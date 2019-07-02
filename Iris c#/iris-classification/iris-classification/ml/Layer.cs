using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iris_classification
{
    class Layer
    {
        Node[] IndividualNode;
        double[] LastStoredOutputs;
        public double[] LastInputs;
        string LayerType;
        double [] Target;
        bool Bias;
        double LearnRate;
        string ActiviatinType;


        int InputCount;
        int OutPutCount;

        public Layer(int _numberOfInputs, int _NumberofOutPuts, bool _bias, double _LearnRate, string _layerType, string _activiationType)
        {
            Bias = _bias;
            LearnRate = _LearnRate;
            LayerType = _layerType;
            InputCount = _numberOfInputs;
            OutPutCount = _NumberofOutPuts;
            ActiviatinType = _activiationType;

            if (Bias)
            {
                InputCount = InputCount + 1;
            }
            IndividualNode = new Node[InputCount];
            

            for (int icount = 0; icount < IndividualNode.Count(); icount++)
            {
                IndividualNode[icount] = new Node(LearnRate, ActiviatinType, OutPutCount,false);

            }

            if (Bias)
            {
                IndividualNode[IndividualNode.Count() - 1] = new Node(LearnRate, ActiviatinType, OutPutCount, true);
            }

        }

        public double [] FeedForward(double[] _Inputs)
        {
            int NodeInputCount = _Inputs.Count();
            int NodeCount = NodeInputCount;
            LastInputs = new double[NodeInputCount];

            for (int icount = 0; icount < NodeInputCount; icount++)
            {
                LastInputs[icount] = _Inputs[icount];
            }

            double[] ForwardInput = _Inputs;

 
                double[] returndata = new double[OutPutCount];

                for (int icount = 0; icount < NodeInputCount; icount++)
                {
                    ForwardInput[icount] = IndividualNode[icount].UpdateLastOutput(ForwardInput[icount]);
                }
               

                if (Bias)
                {
                    NodeCount++;
                    double[] temp = new double[NodeCount];
                    double[] tempInputLog = new double[NodeCount];
                    for (int i = 0; i < NodeCount - 1; i++)
                    {
                        temp[i] = ForwardInput[i];
                        LastInputs[i] = _Inputs[i];
                    }

                    temp[NodeInputCount] = 1;
                    LastInputs = temp;
                    ForwardInput = temp;
                }

               

            double tempWeight = 0;
            double TempTotal = 0;
            double Temp = 0;

            LastInputs = ForwardInput;
            if (LayerType != "OUTPUT")
            {

                for (int icount = 0; icount < returndata.Count(); icount++)
                {
                    // output node at a time


                    for (int i = 0; i < IndividualNode.Count(); i++)
                    {

                        tempWeight = IndividualNode[i].GetWeight(icount);
                        Temp = (tempWeight * ForwardInput[i]);
                        TempTotal = TempTotal + Temp;
                    }

              
                    returndata[icount] = TempTotal;


                    TempTotal = 0;



                }

               
            }
            else
            {
                returndata = ForwardInput;
            }


           

            LastStoredOutputs = returndata;

          
           
            return returndata;
        }

        public double[] GetLastStoredoutputs()
        {

            return LastStoredOutputs;
        }
       
             

       
        public void SetTarget(double [] _Target)
        {
            Target = _Target;
            
        }

        //public int getNodeCount()
        //{
        //        return NodeCount;
           
        //}

        //public string GetLayerPos()
        //{
        //    return LayerPos;
        //}

        //public string GetNextLayerPos()
        //{
        //    return NextLayerPos;
        //}

        public void BackPropagation(double[] OutPutError, double _LearnRate)
        {

         
                for (int icount = 0; icount < IndividualNode.Count(); icount++)
                {

                    IndividualNode[icount].UpdateWeight(OutPutError, _LearnRate);

                }
           

     

        }

        public void BackPropagation(double[] OutPutError, double[] PreviousOutPut)
        {

   

            for (int icount = 0; icount < IndividualNode.Count(); icount++)
            {
                //IndividualNode[icount].UpdateHiddenError(OutPutError);

                IndividualNode[icount].UpdateWeight(PreviousOutPut, LastInputs[icount]);
               
                

            }




        }

        public double[] getHiddenError(double [] errorCode)
        {
            int nodeCount = IndividualNode.Count();
            if (Bias)
            {
                nodeCount--;
            }
            double[] temp = new double[nodeCount];

            for (int icount = 0; icount < nodeCount; icount++)
            {

                temp[icount] =  IndividualNode[icount].UpdateHiddenError(errorCode);

            }


            return temp;


        }
        //public void CalcOutputError(double[] Target, double[] ValueFromNextNode)
        //{
        //    OutPutError = new double[Target.Count()];
        //    for (int icount = 0; icount < OutPutError.Count(); icount++)
        //    {
        //        OutPutError[icount] = ValueFromNextNode[icount] * (1 - ValueFromNextNode[icount]) * (Target[icount] - ValueFromNextNode[icount]);


        //    }
        //}

        //public double[] GetOutPutError()
        //{



        //    return OutPutError;



        //}

        //public void SetOutputError(double[] a)
        //{
        //        OutPutError = a; 


        //}
        ////test system
        public void SetNodes(double[] NodeWeights, int NodeID)
        {

            IndividualNode[NodeID].initNode(NodeWeights);

        }

        public void UpdateChanges()
        {
            for (int icount = 0; icount < IndividualNode.Count(); icount++)
            {

                IndividualNode[icount].ConfirmWeights();

            }

        }
    }
}
