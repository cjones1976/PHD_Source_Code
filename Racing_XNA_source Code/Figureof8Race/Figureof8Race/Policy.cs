using System;
//using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;


namespace Figureof8Race
{

    public class RLPolicy
    {

        ANN Net;
        int inputCount = 6;
        int OutputCount = 3;
        int[] NumberofHiddenLayersDetails = { 3, 10, 8, 5 };
        double ComparisonRate;
 
        //LinkedList<int> ReinforcementTarget = new LinkedList<int>();
        public int Training = 1;
        public int ReinforcementCount = 0;
       // int ReinforcementPos = 0;
        NetworkParse[] NetWorkInit;
        Reporter ReportTrainingCSV;
        //Reporter ReportPostTrainingCSV;
        public long WriteCount;
        public double NewLearnRate = 0.3D;
        public double NewLearnRateLimit = 0.3D;
        public double PreviousErrorRate = 0;
        public double InvestigationRate = 80;
        public double InvestigationRateReset = 80;
        public bool DecisionWasRandom = false;
        PastData Removeat = null;
        double TotalError = double.MaxValue;
        bool Learning = true;
        public int TrackID = 0;
 
        List<PastData> PreviousData = new List<PastData>();
  
        public bool Reset = false;
        

        Random RND = new Random();




  


        public RLPolicy(int _Input, int _Output)
        {

       
            NetWorkInit = new NetworkParse[3];
            ReportTrainingCSV = new Reporter();
           // ReportPostTrainingCSV = new Reporter(RunData, "PostTraining" );
            //ReinforcementTarget.Clear();
            int HiddenLayerSize = _Input / 3 * 2 + _Output;
            //int HiddenLayerSize = 12;

            NetWorkInit[0] = new NetworkParse(_Input, HiddenLayerSize, "INPUT", true, "None");
            NetWorkInit[1] = new NetworkParse(HiddenLayerSize, _Output, "HIDDEN", true, "Sigmoid");
            NetWorkInit[2] = new NetworkParse(_Output, _Output, "OUTPUT", false, "Sigmoid");
            


            PreviousData.Clear();
        
            
            double [] temp = new double[_Output];

         

           
            inputCount = _Input;
            OutputCount = _Output;

            Net = new ANN(NetWorkInit, NewLearnRate);
        
            double [] inputCode = new double[inputCount];
            double [] result = new double[OutputCount];

          

        }

        public long DoReport(double _ErrorMSE, string train, double _reward,double Distance, long _MoveCount, int _LapCount)
        {

            //WriteCount = ReportPostTrainingCSV.WritePostData(_ErrorMSE, train, _reward, Distance, _MoveCount,_LapCount);
            return WriteCount;

        }

        public int CacheCount()
        {
            return PreviousData.Count;
        }



        public Int64 ConvertArrayToInt(double [] Code)
        {
            string Data;

            Data = String.Join("",Code);

            Int64 returndata = Convert.ToInt64(Data, 2);
            return returndata;
        }

        public double[] ConverttoDoublearray(Int64 Code)
        {
            double[] Data = new double[inputCount];
       
            string binary = Convert.ToString(Code, 2);
            int a = Data.Count() - binary.Count();
            for (int i = 0; i < binary.Count(); i++)
            {
                
                Data[a] = Convert.ToInt16(binary[i].ToString());
                a++; 
            }


            return Data;
        }
     
        public void Learn(double [] MainResult, double _reward, int Action, double [] ID, double Distance, long _MoveCount, int _LapCount)
        {
            
            double[] Target = (double[])MainResult.Clone();
            double ErrorLevel = 0 ;
            double[] result;
            
            String TypeName; 

            Target[Action] = _reward;
            //TypeName = "Standard";

           TypeName = "Cached";
            if (TypeName == "Cached")
            {
                //PreviousData.Clear();

               

                for (int a = 0; a < PreviousData.Count(); a++)
                {
                    double temp = 0;
                    temp = Aver(PreviousData[a].GetInput());

                    //if (Removeat != null & PreviousData.Count > inputCount)
                    //{
                    //    PreviousData.Remove(Removeat);
                    //}
                    for (int b = a + 1; b < PreviousData.Count - 1; b++)
                    {
                        double CompTemp = Aver(PreviousData[b].GetInput());
                        if (Math.Abs(temp - CompTemp) < (ComparisonRate * a))
                        {
                            PreviousData.RemoveAt(a);

                        }
                    }
                }


                                    PreviousData.Add(new PastData(Target, MainResult, Action, ID));

                                    
                                    double LowError = double.MaxValue;
                                    double HighError = double.MinValue;
                    foreach (PastData a in PreviousData)
                    {
                        ErrorLevel = 0;
                        
                        double[] NewInput = a.GetInput();
                        double[] NewTarget = a.GetTarget();
                        result = Net.feedForward(NewInput);
                        int iAction = a.GetAction();

                        if (Learning)
                        {
       
                                Net.Learn(NewTarget, result, _reward, NewLearnRate);
                        }
                        ErrorLevel = Math.Abs( Net.LearnError(NewTarget, result));
                        if (ErrorLevel < LowError)
                        {
                            LowError = ErrorLevel;
                            Removeat = a;
                        }
                        if (ErrorLevel > HighError)
                        {
                            HighError = ErrorLevel;
                        }
                        
                        

                    }

                   
                   
                    if (Learning)
                    {
                        Net.Learn(Target, MainResult, _reward, NewLearnRate);

                    }
                    ErrorLevel = Math.Abs(Net.LearnError(Target, MainResult));
                    SetMinMax(ID);

                    TotalError = (HighError + LowError) /2;
                    //TotalError -= LowError;
                  


                    

                    if (DecisionWasRandom)
                    {
                        DecisionWasRandom = false;
                    }

                else
                    {
                        DecisionWasRandom = true;
                        if (ErrorLevel > TotalError)
                        {
                            //Current Error More Need to Learn and be More Random
                            if (Learning & InvestigationRate > InvestigationRateReset)
                            {
                                NewLearnRate = Math.Abs(NewLearnRate + NewLearnRate * 0.001);
                                InvestigationRate = Math.Abs(InvestigationRate - 0.01);
                            }
                            else
                            {


                                NewLearnRate = NewLearnRateLimit;
                                InvestigationRate = InvestigationRateReset;
                            }

                        }
                        else
                        {

                            NewLearnRate = Math.Abs(NewLearnRate - NewLearnRate * 0.001);
                            InvestigationRate = Math.Abs(InvestigationRate + 0.01);
                        }

                    }       
                    

                if (InvestigationRate > 99)
                {
                    InvestigationRate = 100;
                    NewLearnRate = 0;
                    Learning = false;
                }
                else
                {
                    Learning = true;
                }

                   
              
            }

            if (TypeName == "Standard")
            {
                result = Net.feedForward(ID);

                Net.Learn(Target, result,_reward, NewLearnRateLimit);
                TotalError = Math.Abs(Net.LearnError(Target, result, Action));
                Distance = 0;
            }



            //if (ReportTrainingCSV.TrainingCounter >= RunData.TrainingTargetCount)
            //{

            //    //Training = 0;
            //    //Reset = true;
            //}

            //WriteCount = ReportTrainingCSV.TrainingCounter;
            ReportTrainingCSV.WriteErrorRate(TotalError, _LapCount, NewLearnRate, InvestigationRate, _reward, TrackID, PreviousData.Count());
        }

        public double LearnError(double[] _Target, double[] _result)
        {
            return Net.LearnError(_Target, _result);
        }

     
        public double[] getActions(double[] ID)
        {
            double[] temp = new double[OutputCount];
            // temp = NetManager.Evaluate((BasicNetwork)method, dataSet, ID);
            temp = Net.feedForward(ID);
            return temp;

        }

         public void SetMinMax(double [] Input)
    {
         
       ComparisonRate = GetStandardDev();
       ComparisonRate = ComparisonRate / (PreviousData.Count * 2 );
         
   }
        
        
    public double Aver(double [] temp)
    {
        double a = 0;
       for(int i = 0; i < temp.Length ; i++)
       { 
           a += temp[i];
       
       }
       a = a/temp.Length;
       return a;
    }
    
    
     public double GetStandardDev()
    {
        double [] temp = new double[PreviousData.Count];
         for(int j=1; j < PreviousData.Count; j++)
            {
                
                temp[j] = Aver( PreviousData[j].GetInput());
            }
            MathsTools a = new MathsTools(temp);
            return a.getStdDev();
                   
    }
    

        public int GetBestAction(double[] temp)
        {

           
            int TopActionID = 0;
            double TopActionVAlue = temp[0];
            for (int icount = 1; icount < temp.Count(); icount++)
            {
               
                if (temp[icount] > TopActionVAlue)
                {
                    TopActionID = icount;
                    TopActionVAlue = temp[icount];
                }


            }

         

            if (Training == 1)
            {
                if (RND.Next(0, 100) > InvestigationRate)
                {
                    TopActionID = RND.Next(0, OutputCount);
                    DecisionWasRandom = true;
                }

            }
            return TopActionID;

        }

        public double [] FeedForward(double[] ID)
        {

            double[] temp;
            //temp = NetManager.GetSpecificActions(method, dataSet, ID);


            temp = Net.feedForward(ID);
           
            return temp;

        }

        public void CloseFile()
        {
           // ReportTrainingCSV.Close();
        }
        public void ClosePostTrainingFile()
        {
            //ReportPostTrainingCSV.Close();
        }
    
    

    }
}



