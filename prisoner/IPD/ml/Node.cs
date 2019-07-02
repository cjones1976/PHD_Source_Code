using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPD
{
    
    
    class Node
    {
        double [] weights;
        double[] NewWeights;
        double LastOutput;
        double LastInput;
        //double[] BiasWeights;

        double LearnRate;
        double [] WeightError;
        double HiddenNodeError;
        string ActiviationType = "";

        bool Bias;
        Random RND = new Random();


        public Node(double _LearnRate, string _ActiviationType, int Output, bool BiasNode)
        {
            
            LearnRate = _LearnRate;
            ActiviationType = _ActiviationType;
            Bias = BiasNode;
            weights = new double[Output];
            WeightError = new double[Output];
            NewWeights = new double[Output];
            if (BiasNode)
            {
                LastOutput = 1;
            }
            initNode(RND);
        }

        private void initNode(Random _rnd)
        {
            for (int icount = 0; icount < weights.Count(); icount++)
            {
                weights[icount] = _rnd.NextDouble() - .5;
                //weights[icount] = 0.5;
                
            }
        }

      
        public void initNode(double [] _rnd)
        {

            weights = _rnd;

        }

      

        public double GetWeight(int a)
        {
            return weights[a];
        }

        public double GetLastOutPut()
        {
            return LastOutput;
        }
        private double Sigmoid(double value)
        {
            LastOutput =  (double)(1.0 / (1.0 + Math.Pow(Math.E, -value)));
            return LastOutput;
        }
        public double UpdateLastOutput(double value)
        {
            LastOutput = value;
            LastInput = value;

            if (ActiviationType == "Sigmoid")
            {
                LastOutput = Sigmoid(LastOutput);
            }


            return LastOutput;
        }




        public void UpdateWeight(double Input, double [] OutPutError, double _LearnRate)
        {
            LearnRate = _LearnRate;

            for (int icount = 0; icount < weights.Count(); icount++)
            {
                
               // WeightError[icount] =  OutPutError[icount] * Input;
                WeightError[icount] = LearnRate * OutPutError[icount] * Input;
                NewWeights[icount] = weights[icount] + WeightError[icount];
            }

           
        }

       public double UpdateHiddenError (double []OutPutError)
       {
           double temp = 0;

    
               for (int icount = 0; icount < weights.Count(); icount++)
               {
                   temp = temp + OutPutError[icount] * weights[icount];

               }
        

           temp = LastOutput * (1 - LastOutput) * temp;

           return temp;
       }

       public void UpdateWeight(double[] OutPutError, double IN, double _LearnRate)
       {

           LearnRate = _LearnRate;

           for (int icount = 0; icount < weights.Count(); icount++)
           {
               WeightError[icount] = LearnRate * OutPutError[icount] * (IN);
               NewWeights[icount] = weights[icount] + WeightError[icount];
           }
       }
        public void UpdateWeight(double [] OutPutError, double _LearnRate)
        {
            LearnRate = _LearnRate;

            for (int icount = 0; icount < OutPutError.Count(); icount++)
            {
                WeightError[icount] = LearnRate * LastOutput * OutPutError[icount];
                NewWeights[icount] = weights[icount] + WeightError[icount];

            }

        }


        public double CalcHiddenNodeError(double [] OutPutError)
        {

            HiddenNodeError = 0;

            for (int inodeCount = 0; inodeCount < OutPutError.Count(); inodeCount++)
            {

                for (int icount = 0; icount < weights.Count(); icount++)
                {

                    HiddenNodeError = HiddenNodeError + (weights[icount] * OutPutError[inodeCount]);
                }

                

            }

            return HiddenNodeError;

        }

        public void ConfirmWeights()
        {
            weights = NewWeights;


        }
    }
}
