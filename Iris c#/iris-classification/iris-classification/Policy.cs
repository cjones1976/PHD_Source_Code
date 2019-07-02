using System;
//using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;



namespace iris_classification
{

    public class RLPolicy
    {

        ANN Net;
        ELCache EnhancedLearning = new ELCache();
        int inputCount = 0;
        int OutputCount = 0;
    
        public int Training = 1;
        public int ReinforcementCount = 0;

        NetworkParse[] NetWorkInit;
        //Reporter ReportTrainingCSV;
     
        public long WriteCount;
        public double TotalError = 0;
        
        public double PreviousAverageValue;
        bool Learning = true;
        public int TrackID = 0;
        Boolean DoEnhancedLearning = false;

 
        public bool Reset = false;
        

        Random RND = new Random();


        public double NewLearnRate = 0.3D;
        public double InvestigationRate = 90;



        public RLPolicy(int _Input, int _Output, int _HL, int ERun, string LearnType, double _LearnRate)
        {

         
                 NetWorkInit = new NetworkParse[3];
           // ReportTrainingCSV = new Reporter(_HL, Letter, LearnType, ERun);

           // int HiddenLayerSize = _Input / 3 * 2 + _Output;
            int HiddenLayerSize = _HL;
            NewLearnRate = _LearnRate;
            NetWorkInit[0] = new NetworkParse(_Input, HiddenLayerSize, "INPUT", true, "None");
            NetWorkInit[1] = new NetworkParse(HiddenLayerSize, _Output, "HIDDEN", true, "Sigmoid");
            NetWorkInit[2] = new NetworkParse(_Output, _Output, "OUTPUT", false, "Sigmoid");
       
            
            double [] temp = new double[_Output];
            if (LearnType == "E") { DoEnhancedLearning = true;}
         

           
            inputCount = _Input;
            OutputCount = _Output;

            Net = new ANN(NetWorkInit, NewLearnRate);
        
            double [] inputCode = new double[inputCount];
            double [] result = new double[OutputCount];

            EnhancedLearning = new ELCache(ERun, ERun, NewLearnRate, InvestigationRate);
        
            
        }


        public int CacheCount()
        {
            return EnhancedLearning.CacheCount();
        }





        public void Learn(double[] MainResult, double Reward, double[] Target, double[] inputs, int Type)
        {





            TotalError = 0;







            TotalError = Net.LearnError(Target, MainResult);
            //Learning = false;
            if (Learning)
            {

                // new Learn System
                if (DoEnhancedLearning)
                {

                    
                    Net = EnhancedLearning.CacheLearn(new PastData(Target, MainResult, RND, TotalError, inputs, Type), Net);
                    Net = EnhancedLearning.UpdateCache(Net);
                    
                }
                else
                {


                    // normal Learn
                    Net.Learn(Target, MainResult, NewLearnRate);
                }
            }

            if (DoEnhancedLearning)
            {
                NewLearnRate = EnhancedLearning.GetLearnRate();
                InvestigationRate = EnhancedLearning.GetInvestigationRate();
                Learning = EnhancedLearning.UpdateTraining(Learning, TotalError);

            }







        }





        public string Getmemory()
        {
           

             return  EnhancedLearning.GetMemory();
            

        }

        
        public double[] FeedForward(double[] ID)
        {

            double[] temp;
            

            temp = Net.feedForward(ID);

            return temp;

        }

      

        public long GetLearnCount()
        {
            return Net.LearnCount;
        }

        public double GetHighError()
        {
            return EnhancedLearning.GetPreviousAverage();
        }

        public int getHighPos()
        {
            return EnhancedLearning.GetHighPos();
        }

    }
}



