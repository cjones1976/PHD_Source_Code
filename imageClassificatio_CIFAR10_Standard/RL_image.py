#!/usr/bin/env python2
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
import keras.backend as K



import math
import os
import csv
import numpy as np
import time



num_classes = 10
epochs = 10
learncount = 0
learnrate = 0.00001
score = [0,0,0,0,0,0,0,0,0,0]
Matched = [0,0,0,0,0,0,0,0,0,0]
Correct = 0
DecayRate = 0.05


# Operations
def Newlearnrate (learnrate):
    return  math.fabs(learnrate - (learnrate * DecayRate));

def InvestRate(InvestigationRate):
    return math.fabs(InvestigationRate + (InvestigationRate/10 * DecayRate));



save_dir = os.path.join(os.getcwd(), 'saved_models')
model_name = 'keras_cifar10_trained_model.h5'

(x_train, y_train), (x_test, y_test) = cifar10.load_data()
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


model.fit_generator(datagen.flow(x_train, y_train), steps_per_epoch=x_train.shape[0],epochs=epochs, verbose=1)



# Save model and weights
if not os.path.isdir(save_dir):
    os.makedirs(save_dir)
model_path = os.path.join(save_dir, model_name)
model.save(model_path)
print('Saved trained model at %s ' % model_path)

# Score trained model.
#scores = model.evaluate(x_test, y_test, verbose=1)
#print('Test loss:', scores[0])
#print('Test accuracy:', scores[1])

with open('Testing.csv', 'w', newline='') as csvFile:
    writer = csv.writer(csvFile)
    writer.writerow(["prediction","mytarget","error","Accuracy","type","correctly_Found","Correct Count", "NameOfSpecies"])
    TotalError = 0;
    score = [0,0,0,0,0,0,0,0,0,0]
    Matched = [0,0,0,0,0,0,0,0,0,0]
    Correct = 0

    # later...
    
    print ("Testing now")
   
    for i in range( 0, len(y_test)):
    
        
        prediction = model.predict(x_test[i:i+1])
            
        mytarget = y_test[i:i+1]
                   
        error = model.evaluate(x_test[i:i+1], y_test[i:i+1],verbose=1)
        NameofObject = 'none'
        correctly_Found = False
        #  return ['airplane', 'automobile', 'bird', 'cat', 'deer', 'dog', 'frog', 'horse', 'ship', 'truck']

        if np.array_equal(mytarget[0],[1,0,0,0,0,0,0,0,0,0]):
            NameOfSpecies = 'Airplane'
            Matched[0] = Matched[0] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[0] = score[0] + 1

        if np.array_equal(mytarget[0],[0,1,0,0,0,0,0,0,0,0]):
            NameOfSpecies = 'automobile'
            Matched[1] = Matched[1] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[1] = score[1] + 1

        if np.array_equal(mytarget[0],[0,0,1,0,0,0,0,0,0,0]):
            NameOfSpecies = 'Bird'
            Matched[2] = Matched[2] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[2] = score[2] + 1

        if np.array_equal(mytarget[0],[0,0,0,1,0,0,0,0,0,0]):
            NameOfSpecies = 'Cat'
            Matched[3] = Matched[3] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[3] = score[3] + 1

        if np.array_equal(mytarget[0],[0,0,0,0,1,0,0,0,0,0]):
            NameOfSpecies = 'Deer'
            Matched[4] = Matched[4] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[4] = score[4] + 1

        if np.array_equal(mytarget[0],[0,0,0,0,0,1,0,0,0,0]):
            NameOfSpecies = 'Dog'
            Matched[5] = Matched[5] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[5] = score[5] + 1

        if np.array_equal(mytarget[0],[0,0,0,0,0,0,1,0,0,0]):
            NameOfSpecies = 'Frog'
            Matched[6] = Matched[6] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[6] = score[6] + 1

        if np.array_equal(mytarget[0],[0,0,0,0,0,0,0,1,0,0]):
            NameOfSpecies = 'Horse'
            Matched[7] = Matched[7] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[7] = score[7] + 1

        if np.array_equal(mytarget[0],[0,0,0,0,0,0,0,0,1,0]):
            NameOfSpecies = 'Ship'
            Matched[8] = Matched[8] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[8] = score[8] + 1
           
        if np.array_equal(mytarget[0],[0,0,0,0,0,0,0,0,0,1]):
            NameOfSpecies = 'Truck'
            Matched[9] = Matched[9] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[9] = score[9] + 1
     

        #writer.writerow(["prediction","mytarget","error","Accuracy","type","correctly_Found","Correct Count", "NameOfSpecies"])
        writer.writerow([np.round(prediction[0],0),mytarget[0],error[0],error[1],'NORL',correctly_Found,Correct, NameOfSpecies])

