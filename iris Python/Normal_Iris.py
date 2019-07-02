"""
    	A simple neural network written in Keras (TensorFlow backend) to classify the IRIS data
	Chris Jones	
"""
import math
import numpy as np
import os
import csv
import time
import keras
from sklearn.datasets import load_iris
from sklearn import preprocessing
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import OneHotEncoder

from keras.models import Sequential
from keras.layers import Dense
from keras.optimizers import Adam
import keras.backend as K


batch_size = 120
num_classes = 10
epochs = 500
learncount = 0
learnrate = 0.001

score = [0,0,0]
Matched = [0,0,0]
Correct = 0
correctly_Found = False

iris_data = load_iris() # load the iris dataset



# Normalize the data attributes for the Iris dataset.

# load the iris dataset
iris = load_iris()
print(iris.data.shape)
y_ = iris_data.target.reshape(-1, 1) # Convert data to a single column
# separate the data from the target attributes
x = iris.data
# normalize the data attributes
#x = preprocessing.normalize(x)
x = preprocessing.scale(x)

print('Example data: ')
print(iris_data.data[:5])
print('Example labels: ')
print(iris_data.target[:5])



# One Hot encode the class labels
encoder = OneHotEncoder(sparse=False)
y = encoder.fit_transform(y_)
#print(y)

# Split the data for training and testing
x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=0.20)

# Build the model

model = Sequential()

model.add(Dense(20, input_shape=(4,), activation='relu', name='fc1'))
model.add(Dense(20, activation='relu', name='fc2'))
model.add(Dense(3, activation='softmax', name='output'))

# Adam optimizer with learning rate of 0.001
#optimizer = rmpop(lr=learnrate)
#model.compile(optimizer, loss='categorical_crossentropy', metrics=['accuracy'])
opt_rms = keras.optimizers.rmsprop(lr=learnrate,decay=1e-6)
model.compile(loss='categorical_crossentropy', optimizer=opt_rms, metrics=['accuracy'])

print('Neural Network Model Summary: ')
print(model.summary())

# # Train the model
# model.fit(train_x, train_y, verbose=2, batch_size=5, epochs=200)

# # Test on unseen data
 # later...
training_Count = len(y_train)
with open('TrainingNormalIRISRL.csv', 'w') as csvFile:
    writer = csv.writer(csvFile)
    writer.writerow(["error","correct","learncount","type","correctly_Found","learnrate","Learning","InvestigationRate", "Correct Count", "NameOfSpecies","epoch"])
    writer.writerow([time.strftime("%Y-%m-%d %H:%M")])
 
    for e in range( 0, epochs):
            

        BatchList = list(range(0,training_Count))
            

    
        for a in BatchList[::batch_size]:
                
                    

            myList = list(range(a,a+batch_size))  # list(range(1, 101)) for Python 3 if you need a list
         
            model.fit(x_train, y_train, verbose=0, epochs=1)
        
                    

            for i in myList[::1]:

                prediction = model.predict(x_train[i:i+1],verbose=0)
                error = model.evaluate(x_train[i:i+1], y_train[i:i+1], verbose = 0)

                learncount+=1
                        
                mytarget = y_train[i:i+1]
                MyInput = x_train[i:i+1]
            
                
                    
                NameofObject = 'none'
                correctly_Found = False
                    
                if (1==error[1]):
                    Correct+=1
                    correctly_Found = True
                        
                                     
                writer.writerow([mytarget,format(error[0], 'f'),error[1],learncount,'RL',correctly_Found,  Correct, e])      

               
                        

    writer.writerow([time.strftime("%Y-%m-%d %H:%M")])
csvFile.close()

results = model.evaluate(x_test, y_test)

print('Final test set loss: {:4f}'.format(results[0]))
print('Final test set accuracy: {:4f}'.format(results[1]))


with open('TestingNormalIRISRL.csv', 'w') as csvFile:
    writer = csv.writer(csvFile)
    writer.writerow(["prediction","mytarget","error","Accuracy","type","correctly_Found","Correct Count", "NameOfSpecies"])
    TotalError = 0;
    score = [0,0,0]
    Matched = [0,0,0]
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

        if np.array_equal(mytarget[0],[1,0,0]):
            NameOfSpecies = 'Setosa'
            Matched[0] = Matched[0] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[0] = score[0] + 1

        if np.array_equal(mytarget[0],[0,1,0]):
            NameOfSpecies = 'Viginica'
            Matched[1] = Matched[1] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[1] = score[1] + 1

        if np.array_equal(mytarget[0],[0,0,1]):
            NameOfSpecies = 'Versacolour'
            Matched[2] = Matched[2] + 1
            if np.array_equal(mytarget[0],np.round(prediction[0],0)):
                Correct+=1
                correctly_Found = True
                score[2] = score[2] + 1

     

        #writer.writerow(["prediction","mytarget","error","Accuracy","type","correctly_Found","Correct Count", "NameOfSpecies"])
        writer.writerow([np.round(prediction[0],0),mytarget[0],error[0],error[1],'NORL',correctly_Found,Correct, NameOfSpecies])

