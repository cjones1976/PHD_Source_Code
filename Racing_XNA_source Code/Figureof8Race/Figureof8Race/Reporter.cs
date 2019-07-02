using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Figureof8Race
{
    class Reporter
    {
        string Filename;
        FileStream fs;
        StreamWriter sw;
        public long TrainingCounter = 0;
       
        Stopwatch Watch = new Stopwatch();
  
      
 

        public Reporter()
        {

           
   
            
            Filename = "Report" + ".csv";
            fs = new FileStream(Filename, FileMode.Create);
            sw = new StreamWriter(fs);
          
            
             sw.WriteLine("ErrorRate, LapCount, LearnRate, InvestigationRate, Reward, TrackID, Cache Size");
             Watch.Start();
        }



        //public long WriteBackPropData(double ErrorMSE, string TrainingMode, double _reward, double Distance, long _MoveCount, int _LapCount)
        //{

        //    if (BackPropCounter < TargetWrites)
        //    {
        //        if (WritetoFile)
        //        {
        //            //StreamWriter sw = new StreamWriter(fs);

        //            string temp = TrainingCounter.ToString() + "," + _reward.ToString() + "," + ErrorMSE.ToString() + "," + TrainingMode.ToString() + "," + Watch.Elapsed.TotalSeconds.ToString() + "," + Distance.ToString()+","+ _MoveCount.ToString()+","+_LapCount.ToString();
        //            sw.WriteLine(temp);

        //        }
        //    }
        //    else
        //    {

        //        Close();
        //    }

        //    BackPropCounter++;
        //    TrainingCounter = BackPropCounter;
        //    return BackPropCounter;
        //}


        //public long WriteActionData(double ErrorMSE, string TrainingMode, double _reward, double Distance, long _MoveCount, int _LapCount)
        //{
        //    ActionCounter = _MoveCount;
        //    if (ActionCounter < TargetWrites)
        //    {
        //        if (WritetoFile)
        //        {
        //            //StreamWriter sw = new StreamWriter(fs);

        //            string temp = tempCount.ToString() + "," + _reward.ToString() + "," + ErrorMSE.ToString() + "," + TrainingMode.ToString() + "," + Watch.Elapsed.TotalSeconds.ToString() + "," + Distance.ToString() + "," + _MoveCount.ToString() + "," + _LapCount.ToString();
        //            sw.WriteLine(temp);

        //        }
        //    }
        //    else
        //    {

        //        Close();
        //    }

        //    tempCount++;
        //    TrainingCounter = ActionCounter;
        //    return ActionCounter;
        //}

        public void WriteErrorRate(double ErrorMSE, int LapCount)
        {
            sw.WriteLine(ErrorMSE.ToString() + "," + LapCount.ToString());

        }

        public void WriteErrorRate(double ErrorMSE, int LapCount, double LearnRate)
        {
            sw.WriteLine(ErrorMSE.ToString() + "," + LapCount.ToString() + "," + LearnRate.ToString());

        }

        public void WriteErrorRate(double ErrorMSE, int LapCount, double LearnRate, double InvestigationRate, double _reward, int TrackID, int CachCount)
        {
            sw.WriteLine(ErrorMSE.ToString() + "," + LapCount.ToString() + "," + LearnRate.ToString() + "," + InvestigationRate.ToString() + "," + _reward.ToString() + "," + TrackID.ToString() + "," + CachCount.ToString());

        }



        //public long WriteLapData(double ErrorMSE, string TrainingMode, double _reward, double Distance, long _MoveCount, int _LapCount)
        //{
        //    LapCounter = _LapCount;
        //    if ( LapCounter < TargetWrites)
        //    {
        //        if (WritetoFile)
        //        {
        //            //StreamWriter sw = new StreamWriter(fs);

        //            string temp = TrainingCounter.ToString() + "," + _reward.ToString() + "," + ErrorMSE.ToString() + "," + TrainingMode.ToString() + "," + Watch.Elapsed.TotalSeconds.ToString() + "," + Distance.ToString() + "," + _MoveCount.ToString() + "," + _LapCount.ToString();
        //            sw.WriteLine(temp);

        //        }
        //    }
        //    else
        //    {

        //        Close();
        //    }
        //    TrainingCounter = LapCounter;
        //    return LapCounter;
        //}
        //public long WriteDistanceData(double ErrorMSE, string TrainingMode, double _reward, double Distance, long _MoveCount, int _LapCount)
        //{
        //    DistanceCounter = DistanceCounter + Distance;
        //    if (DistanceCounter < TargetWrites)
        //    {
        //        if (WritetoFile)
        //        {
        //            //StreamWriter sw = new StreamWriter(fs);

        //            string temp = TrainingCounter.ToString() + "," + _reward.ToString() + "," + ErrorMSE.ToString() + "," + TrainingMode.ToString() + "," + Watch.Elapsed.TotalSeconds.ToString() + "," + Distance.ToString() + "," + _MoveCount.ToString() + "," + _LapCount.ToString();
        //            sw.WriteLine(temp);

        //        }
        //    }
        //    else
        //    {

        //        Close();
        //    }
        //    TrainingCounter = Convert.ToInt32(DistanceCounter);
        //    return TrainingCounter;
        //}

        //public long WritePostData(double ErrorMSE, string TrainingMode, double _reward, double Distance, long _MoveCount, int _LapCount)
        //{

        //    if (PostTrainingCounter < PostTargetWrites)
        //    {
        //        if (WritetoFile)
        //        {
        //            //StreamWriter sw = new StreamWriter(fs);

        //            string temp = PostTrainingCounter.ToString() + "," + _reward.ToString() + "," + ErrorMSE.ToString() + "," + TrainingMode.ToString() + "," + Watch.Elapsed.TotalSeconds.ToString() + "," + Distance.ToString() + "," + _MoveCount.ToString() + "," + _LapCount.ToString();
        //            sw.WriteLine(temp);

        //        }
        //    }
        //    else
        //    {

        //        Close();
        //    }

        //    PostTrainingCounter++;
        //    return PostTrainingCounter;
        //}

       

        public void Close()
        {
           // sw.Close();
           // fs.Close();
        }
    }
}

