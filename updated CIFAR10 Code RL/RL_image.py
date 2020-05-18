#!/usr/bin/env python3
# -*- coding: utf-8 -*-



import keras
from keras.models import Sequential
from keras.utils import np_utils
from keras.preprocessing.image import ImageDataGenerator
from keras.layers import Dense, Activation, Flatten, Dropout, BatchNormalization
from keras.layers import Conv2D, MaxPooling2D
from keras.datasets import cifar10
from keras import regularizers
from keras.callbacks import LearningRateScheduler
from keras.models import load_model
from sklearn.model_selection import train_test_split
import keras.backend as K
from sklearn.utils import shuffle



import math
import os
import csv
import numpy as np
import time

os.environ['CUDA_VISIBLE_DEVICES'] = '-1'


###########################################################
class RL_Record(object):
    def __init__(data):
        data.error = []
        data.count = 0
        data.Target = []
        data.Input = []
        data.deleteme=[]
        
    def Delete( data, deletepos):
        del data.Target[deletepos]
        del data.Input[deletepos]
        del data.error[deletepos]
        data.count = data.count - 1

    def AddEntry( data, mytarget, MyInput, error):
        data.Target.append(mytarget)
        data.Input.append(MyInput)
        data.error.append(error)
        data.count = data.count+1
        data.deleteme.append(False)

    
    def MarkForDel(data,deletepos):
        data.deleteme[deletepos]=True
 

####################################################################

test = RL_Record()


batch_size = 1000
num_classes = 10
epochs = 1
learncount = 0
learnrate = 0.0001

       
learnrateReset = learnrate
Accelerated = 25
AcceleratedLearnRate = 0.0001
score = [0,0,0,0,0,0,0,0,0,0]
Matched = [0,0,0,0,0,0,0,0,0,0]
Correct = 0
Learning = True
BatchLearning = True
InvestigationRate = 90
DecayRate = 0.0025
Show_Progres = 1000
Lookevery = 4
writetofile = 4
GlobalLoss = 0


# Operations
def Newlearnrate (learnrate):
    return  math.fabs(learnrate - (learnrate * DecayRate));

def InvestRate(InvestigationRate):
    return math.fabs(InvestigationRate + (InvestigationRate/10 * DecayRate));



save_dir = os.path.join(os.getcwd(), 'saved_models')
model_name = 'keras_cifar10_trained_model.h5'

(x_train, y_train), (x_test, y_test) = cifar10.load_data()
#x_train, x_test, y_train, y_test = train_test_split(x_test, y_test, test_size=0.4)
#x_train, x_test, y_train, y_test = train_test_split(x_test, y_test, test_size=0.5)
print( x_train.shape, y_train.shape)
print( x_test.shape, y_test.shape)

x_train = x_train.astype('float32')
x_test = x_test.astype('float32')

#z-score
mean = np.mean(x_train,axis=(0,1,2,3))
std = np.std(x_train,axis=(0,1,2,3))
x_train = (x_train-mean)/(std+1e-7)
x_test = (x_test-mean)/(std+1e-7)

num_classes = 10
y_train = np_utils.to_categorical(y_train,num_classes)
y_test = np_utils.to_categorical(y_test,num_classes)

weight_decay = 1e-4
model = Sequential()
model.add(Conv2D(32, (3,3), padding='same', kernel_regularizer=regularizers.l2(weight_decay), input_shape=x_train.shape[1:]))
model.add(Activation('elu'))
model.add(BatchNormalization())
model.add(Conv2D(32, (3,3), padding='same', kernel_regularizer=regularizers.l2(weight_decay)))
model.add(Activation('elu'))
model.add(BatchNormalization())
model.add(MaxPooling2D(pool_size=(2,2)))
model.add(Dropout(0.2))

model.add(Conv2D(64, (3,3), padding='same', kernel_regularizer=regularizers.l2(weight_decay)))
model.add(Activation('elu'))
model.add(BatchNormalization())
model.add(Conv2D(64, (3,3), padding='same', kernel_regularizer=regularizers.l2(weight_decay)))
model.add(Activation('elu'))
model.add(BatchNormalization())
model.add(MaxPooling2D(pool_size=(2,2)))
model.add(Dropout(0.3))

model.add(Conv2D(128, (3,3), padding='same', kernel_regularizer=regularizers.l2(weight_decay)))
model.add(Activation('elu'))
model.add(BatchNormalization())
model.add(Conv2D(128, (3,3), padding='same', kernel_regularizer=regularizers.l2(weight_decay)))
model.add(Activation('elu'))
model.add(BatchNormalization())
model.add(MaxPooling2D(pool_size=(2,2)))
model.add(Dropout(0.4))

model.add(Flatten())
model.add(Dense(num_classes, activation='softmax'))

model.summary()

# Load model and weights
if os.path.isdir(save_dir):
    model_path = os.path.join(save_dir, model_name)
    model = load_model(model_path)
    print('Loaded trained model at %s ' % model_path)


#data augmentation
datagen = ImageDataGenerator(
    rotation_range=15,
    width_shift_range=0.1,
    height_shift_range=0.1,
    horizontal_flip=True,
    )
datagen.fit(x_train)

#training


opt_rms = keras.optimizers.rmsprop(lr=learnrate,decay=1e-6)
model.compile(loss='categorical_crossentropy', optimizer=opt_rms, metrics=['accuracy'])


#model.fit_generator(datagen.flow(x_train, y_train, batch_size=batch_size),\
#                    steps_per_epoch=x_train.shape[0] // batch_size,epochs=epochs,\
#                    verbose=1,validation_data=(x_test,y_test),callbacks=[LearningRateScheduler(lr_schedule)])

with open('Training.csv', 'w', newline='') as csvFile:
    writer = csv.writer(csvFile)

    writer.writerow(["learn rate","EPS", "accuracy","val_accuracy","loss","val_loss"])
    TotalError = 0
    
    print('Training the model...')
    

    # later...
    training_Count = len(y_train)
 
    for e in range( 0, epochs):
        

        x_train, y_train = shuffle(x_train,y_train)
 

        BatchList = list(range(0,training_Count))
        

   
        for a in BatchList[::batch_size]:
            
                   

            myList = list(range(a,a+batch_size))  # list(range(1, 101)) for Python 3 if you need a list


            BatchLearning = Learning
            if (Learning):

                print(learnrate)
                print(InvestigationRate)
                print (test.count)
                #K.set_value(model.optimizer.lr, learnrate)
                if (Accelerated > 0 or test.count>100 ):
                    Accelerated = Accelerated -1
                    K.set_value(model.optimizer.lr, AcceleratedLearnRate)
                else:
                    K.set_value(model.optimizer.lr, 0)

                response = model.fit_generator(datagen.flow(x_train[a:a+batch_size], y_train[a:a+batch_size]),steps_per_epoch=batch_size, validation_data=(x_test, y_test))



            for i in myList[::1]:

               # prediction = model.predict(x_train[i:i+1],verbose=0)
                error = model.evaluate(x_train[i:i+1], y_train[i:i+1], verbose = 1)

                if (BatchLearning):
                    learncount+=1
                    
                mytarget = y_train[i:i+1]
                MyInput = x_train[i:i+1]
          
                  
                
                NameofObject = 'none'
                correctly_Found = False

                if (1==error[1]):
                    Correct+=1
                    correctly_Found = True
                else:
                    test.AddEntry( mytarget[0], MyInput[0], error[0])
                    if (GlobalLoss == 0):
                        GlobalLoss = error[0]
                    else:
                        GlobalLoss = GlobalLoss * 0.95
                        GlobalLoss = (error[0] + GlobalLoss)/2


                if (error[0] < GlobalLoss or  test.count < 2 ):
                    learnrate = Newlearnrate(learnrate)
                    InvestigationRate = InvestRate(InvestigationRate)
                else:
                    learnrate = learnrateReset
                    InvestigationRate = 90
                    Learning = True


                if (InvestigationRate > 98):
                    Learning = False
                    InvestigationRate = 100
                    learnrate = 0
                       
    


            ####################################################################
            #Reluctant System
            if(test.count>2):

                K.set_value(model.optimizer.lr, learnrate)
                y = np.array(test.Target)
                x = np.array(test.Input)



                if(Learning and test.count < 100):
                    model.fit(x, y,epochs=1, verbose=1)
                    learncount+=test.count

                deletelist = []
                deletecounter = 0
                for a in range(test.count) :
                    myerror = model.evaluate(x[a:a+1], y[a:a+1], verbose = 1)
                    if (1==myerror[1]):
                        deletelist.append(a)
                        deletecounter+=1
            
                deletelist.reverse()
                while deletecounter > 0:
                    test.Delete(deletelist[0])
                    deletelist.pop(0)
                    deletecounter = deletecounter -1



            writer.writerow([learnrate,InvestigationRate, test.count, response.history['accuracy'], response.history['val_accuracy'], response.history['loss'], response.history['val_loss']])
                    
         
    writer.writerow([time.strftime("%Y-%m-%d %H:%M")])




    # Save model and weights
    if not os.path.isdir(save_dir):
        os.makedirs(save_dir)
    model_path = os.path.join(save_dir, model_name)
    model.save(model_path)
    print('Saved trained model at %s ' % model_path)

    # Score trained model.
    scores = model.evaluate(x_test, y_test, verbose=1)
    print('Test loss:', scores[0])
    print('Test accuracy:', scores[1])
    writer.writerow(["Test loss:", scores[0]])
    writer.writerow(["Test accuracy:", scores[1]])
    csvFile.close()
