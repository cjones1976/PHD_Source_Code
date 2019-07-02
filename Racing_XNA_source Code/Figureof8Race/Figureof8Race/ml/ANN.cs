using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Figureof8Race
{
    class ANN
    {

       int NumberofInputs;
       int NumberofOutPuts;
       int NumberofHiddenLayers;
       double[][] allOutPuts;
       public long LearnCount = 0;
      // public long WriteCount;

        Layer[] AllLayers;

       Random rnd = new Random();
       NetworkParse[] NetworkConstruct;
       double LearnRate;
       public ANN(NetworkParse [] _NetworkConstruct, double _LearnRate)
       {
            NetworkConstruct = _NetworkConstruct;
            NumberofInputs = NetworkConstruct[0].Input;
            NumberofOutPuts = NetworkConstruct[NetworkConstruct.Count() - 1].output;
            NumberofHiddenLayers = NetworkConstruct.Count()-2;
            LearnRate = _LearnRate;
            
           AllLayers = new Layer[NetworkConstruct.Count()];

            // create layers

            //input layer first


           for (int icount = 0; icount < NetworkConstruct.Count(); icount++)
           {
               AllLayers[icount] = new Layer(NetworkConstruct[icount].Input, NetworkConstruct[icount].output, NetworkConstruct[icount].Bias, LearnRate, NetworkConstruct[icount].LayerType, NetworkConstruct[icount].ActivationType);
           }
           allOutPuts = new double[AllLayers.Count()][];
           
           
           

           
       }

        public double [] feedForward(double [] _inputs)
        {
            double[] Inputs = _inputs;
            double[] results;

            for (int icount = 0; icount < AllLayers.Count(); icount++)
            {
              
                results = AllLayers[icount].FeedForward(Inputs);
                allOutPuts[icount] = results;
                Inputs = results;
            }

            return Inputs;
        }


        public double LearnError(double[] Target, double[] result)
        {
            double MSE = 0;

            for (int Icount = 0; Icount < Target.Count(); Icount++)
            {
                double STotal;
                STotal = Target[Icount] - result[Icount];
                STotal = Math.Pow(STotal, 2);
                MSE = MSE + STotal;
            }

            MSE = MSE / Target.Count();
            return MSE;
        }
        
        public double LearnError(double[] Target, double[] result, int Action)
        {
            double MSE = 0;

           
                double STotal;
                STotal = Target[Action] - result[Action];
    
                MSE =  STotal;
          

            
            return MSE;
        }

        public void Learn(double[] _target, double[] _Results, double _reward,double _LearnRate)
        {
            //for now set target!
            LearnCount++;
            double[] Target = _target;
            double[] OutputError = new double[_Results.Count()];

          

            for (int a = 0; a < OutputError.Count(); a++)
            {
                OutputError[a] = _Results[a] * (1 - _Results[a]) * (Target[a] - _Results[a]);
            }

            // do ouput error first
            AllLayers[AllLayers.Count() - 2].BackPropagation(OutputError, _LearnRate);

            for (int a = AllLayers.Count() - 3; a >= 0; a--)
            {
                double[] tempHiddenNodeError = AllLayers[a + 1].getHiddenError(OutputError);

                AllLayers[a].BackPropagation(OutputError, tempHiddenNodeError);

            }

            for (int a = 0; a < AllLayers.Count(); a++)
            {
                AllLayers[a].UpdateChanges();
            }
        }

    }
}
