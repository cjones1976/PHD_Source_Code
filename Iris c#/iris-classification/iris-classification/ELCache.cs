using System;
//using System.Collections.Generic;
using System.Linq;

using System.Diagnostics;

using System.Collections.Generic;


namespace iris_classification
{
    class ELCache
    {

        public double DecayRate = 0.25;
        int CacheSize;
        int Postcachesize;
        public int EnhancedLearning = 0;

        double NewLearnRate;
        double NewLearnRateLimit;
        double InvestigationRate;
        double InvestigationRateReset;
        // added new
        public double LowError = double.MaxValue;
        public double HighError = double.MinValue;
        public int LowPos;
        int HighPos;
        int ELResetRate;
        private PastData LastEntry;
     
        string  TypeData;
        private Random RND = new Random();

        List<PastData> PreviousData = new List<PastData>();

      

        public ELCache()
        { }

        public ELCache(int _CachSize, int _EnhancedLearning, double _LearnRate, double _InvestigationRate)
        {
            CacheSize = _CachSize;
            Postcachesize = CacheSize ;
            PreviousData.Clear();
            EnhancedLearning = _EnhancedLearning;
            NewLearnRate = _LearnRate;
            NewLearnRateLimit = _LearnRate;
            InvestigationRate = _InvestigationRate;
            InvestigationRateReset = _InvestigationRate;
            ELResetRate = _EnhancedLearning;

        }

        public double GetPreviousAverage()
        {
            return HighError;
        }

       public bool UpdateTraining(bool  Learning, double TotalError)
       {
            //PreviousAverageValue = EnhancedLearning.GetPreviousAverage();
           if (EnhancedLearning < 1)
           {
               CacheSize = Postcachesize; 
               Learning = UpdateLearnRate(Learning, TotalError);
           }
                
           return Learning;
       }

        public bool UpdateLearnRate(Boolean Learning, double TotalError)
        {


            if (0.30 < TotalError)
            {
                
                    NewLearnRate = NewLearnRateLimit;
                    InvestigationRate = InvestigationRateReset;
        

            }
            else
            {
                NewLearnRate = Math.Abs(NewLearnRate - (NewLearnRate * DecayRate));
                InvestigationRate = Math.Abs(InvestigationRate + (InvestigationRate/10 * DecayRate));

            }

            if (InvestigationRate > 99.5)
            {
                InvestigationRate = 100;
                NewLearnRate = 0;
                Learning = false;
             //   PreviousData.Clear();
               
            }
            else
            {
                Learning = true;
            }

          

            return Learning;
        }

        public ANN CacheLearn(PastData NewEntry, ANN Net)
        {

            int OneCount = 0;
            int TwoCount = 0;
            int ThreeCount = 0;


            // LowPos = -1;
            // HighPos = -1;
            LowError = double.MaxValue;
            HighError = double.MinValue;

            LastEntry = NewEntry;


            double ErrorLevel = 0;

            double[] NewInput = NewEntry.GetInput();



            double[] SingleTarget = NewEntry.GetTarget();
            double[] result = Net.feedForward(NewInput);
            Net.Learn(SingleTarget, result, NewLearnRate);



         //   ErrorLevel = Net.LearnError(SingleTarget, result);

            // add a bit that only addes bad ones!
           // if (ErrorLevel > 0.25)
           // {
                PreviousData.Add(NewEntry);
           //     Console.WriteLine("hello");
           // }
                // Console.WriteLine("ADDING ENTRY : " + PreviousData.Count);






                if (PreviousData.Count > CacheSize)
                {
                    PreviousData.RemoveAt(LowPos);
                    LowError = double.MaxValue;
                    LowPos = -1;
                }

                for (int a = 0; a < PreviousData.Count(); a++)
                {
                    ErrorLevel = 0;

                    NewInput = PreviousData[a].GetInput();


                    SingleTarget = PreviousData[a].GetTarget();

                    if (EnhancedLearning > 0)
                    {
                        EnhancedLearn(Net, NewInput, SingleTarget);
                    }

                    /////
                    // Store Type Makeup
                    if (PreviousData[a].GetTypeCode() == 1)
                    {
                        OneCount++;
                    }
                    if (PreviousData[a].GetTypeCode() == 2)
                    {
                        TwoCount++;
                    }
                    if (PreviousData[a].GetTypeCode() == 3)
                    {
                        ThreeCount++;
                    }




                    result = Net.feedForward(NewInput);
                    TypeData = "" + OneCount + " ," + TwoCount + "," + ThreeCount + "," + (OneCount + TwoCount + ThreeCount);

                    ErrorLevel = Net.LearnError(SingleTarget, result);


                    if (ErrorLevel < LowError)
                    {

                        LowError = ErrorLevel;
                        LowPos = a;

                    }
                    if (ErrorLevel > HighError)
                    {
                        HighError = ErrorLevel;
                        HighPos = a;
                    }

              //  }





            }
           
            if (EnhancedLearning > 0) { EnhancedLearning--; }

            return Net;
        }

        public ANN UpdateCache(ANN Net)
        {
            int count = PreviousData.Count;


            if (count > 2 && HighPos > -1)
            {

                
                int Pos = HighPos;
                double[] NewInput = PreviousData[Pos].GetInput();

        
                double[] SingleTarget = PreviousData[Pos].GetTarget();
                double[] result = Net.feedForward(NewInput);
                HighError = Net.LearnError(SingleTarget, result); 


                
                
                double ErrorLevel;
                int CheckCount = 1;
                do
                { 
                    
                   
                  


                    Net.Learn(SingleTarget, result, NewLearnRate);
                    result = Net.feedForward(NewInput);
                    CheckCount--;
                    ErrorLevel = Net.LearnError(SingleTarget, result);


                } while (ErrorLevel > LowError && CheckCount > 0);

                
            }
            

            return Net;
        }

        public void EnhancedLearn(ANN Net,double[] NewInput, double [] SingleTarget)
        {

            
            double ErrorLevel;
            double[] result = Net.feedForward(NewInput);
            ErrorLevel = Net.LearnError(SingleTarget, result);
            double SetLevel = ErrorLevel * 0.9;
            //if (SetLevel < 0.05)
            //{
                //SetLevel = 0.05;
            //}
            int CheckCount = 10;
            do
            {
                result = Net.feedForward(NewInput);
                
                


                Net.Learn(SingleTarget, result, NewLearnRate);
                CheckCount--;
                ErrorLevel = Net.LearnError(SingleTarget, result);


            } while (ErrorLevel > SetLevel || CheckCount > 0);

        }

        public int CacheCount()
        {
            return PreviousData.Count;
        }

        public string GetMemory()
        {
            return TypeData;
        }

        public double GetLearnRate()
        {
            return NewLearnRate;
        }
        public double GetInvestigationRate()
        {
            return InvestigationRate;
        }

        public int GetHighPos()
        {
            return HighPos;
        }
     
    }
}
