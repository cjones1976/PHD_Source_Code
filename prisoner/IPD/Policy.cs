using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;



namespace IPD
{

    public class RLPolicy
    {

        ANN Net;
        ELCache EnhancedLearning = new ELCache();
        int inputCount = 3;
        int OutputCount = 1;
    
        public int Training = 1;
        public int ReinforcementCount = 0;

        NetworkParse[] NetWorkInit;
        Reporter ReportTrainingCSV;
     
        public long WriteCount;

        double[] Result;
        public double PreviousAverageValue;
        bool Learning = true;
        public int TrackID = 0;
        Boolean DoEnhancedLearning = false;

 
        public bool Reset = false;
        

        Random RND = new Random();


        public double NewLearnRate = 0.01D;
        public double InvestigationRate = 90;



        public RLPolicy(int _Input, int _Output, int _HL, String Letter, String LearnType, int ERun)
        {

       
            NetWorkInit = new NetworkParse[3];
            
            ReportTrainingCSV = new Reporter(_HL, Letter, LearnType, ERun);

           // int HiddenLayerSize = _Input / 3 * 2 + _Output;
            int HiddenLayerSize = _HL;

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
        
            EnhancedLearning = new ELCache(20, ERun, NewLearnRate, InvestigationRate);
        
            
        }


        public int CacheCount()
        {
            return EnhancedLearning.CacheCount();
        }
      

       


        public  void Learn(double _reward, double[] ID, int LastAction)
        {

            double[] Target = new double[1];

       
  

      
            double TotalError = 0;
      

            Target[0] = _reward;
           // TypeName = "Standard";


     

                TotalError = Net.LearnError(Target, Result);

                if (Learning)
                {   
                    
                    // new Learn System
                    if (DoEnhancedLearning)
                    {

                        Net = EnhancedLearning.CacheLearn(new PastData(Target, Result, ID, RND, TotalError), Net);
                        Net = EnhancedLearning.UpdateCache(Net);
                    }
                    else
                    {


                        // normal Learn
                        Net.Learn(Target, Result, NewLearnRate);
                    }
                }

                if (DoEnhancedLearning)
                {
                    NewLearnRate = EnhancedLearning.GetLearnRate();
                    InvestigationRate = EnhancedLearning.GetInvestigationRate();
                    Learning = EnhancedLearning.UpdateTraining(Learning, TotalError);

                }       
                

              
           



            ReportTrainingCSV.WriteErrorRate(TotalError,LastAction, NewLearnRate, InvestigationRate, _reward, EnhancedLearning.CacheCount(), EnhancedLearning.GetPreviousAverage(), EnhancedLearning.EnhancedLearning, EnhancedLearning.HighPos, EnhancedLearning.LowPos, Net.LearnCount);
        }

       public void WriteData(double _reward,int  _action)
       {
       
            ReportTrainingCSV.WriteErrorRate(_reward , _action );
   
    
       }
        public int GetBestAction(double[] temp, int runno)
        {


            int TopActionID = 0;
            temp[2] = 0;
            double[] Result1 = FeedForward(temp);
            temp[2] = 1;
            double[] Result2 = FeedForward(temp);

            if (Result2[0] > Result1[0])
            {
                TopActionID = 1;
                Result = Result2;
            }
            else
            {
                Result = Result1;
            }





            if (Learning)
            {
                if (runno < 4)
                { 
                    if (RND.Next(0, 100) > InvestigationRate)
                    {
                        TopActionID = RND.Next(0, 2);

                    }
                }

            }
            return TopActionID;

        }

        public double[] FeedForward(double[] ID)
        {

            double[] temp;
            //temp = NetManager.GetSpecificActions(method, dataSet, ID);


            temp = Net.feedForward(ID);

            return temp;

        }

        public void CloseFile()
        {
            
            ReportTrainingCSV.Close();
        }
 

    }
}



