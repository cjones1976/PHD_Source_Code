using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace IPD
{



    class Reporter
    {
        string Filename;
        FileStream fs;
        StreamWriter sw;
        public long TrainingCounter = 0;
        Stopwatch Timer;
        long Count = 0;




         public Reporter(int HL, String Letter, string LearnType, int ERun)
        {


            Timer = new Stopwatch();
            Timer.Restart();
            Filename = "Report" + HL.ToString() + Letter + LearnType + ERun.ToString() + ".csv";
            fs = new FileStream(Filename, FileMode.Create);
            sw = new StreamWriter(fs);





            sw.WriteLine("Total Error, action, LearnRate, InvestigationRate, Reward, Cache Size, High Error, Enhanced Count, Position of Low Error, Position of High Error, learn count");

        }

      
        public void WriteErrorRate(double ErrorMSE,int LastAction, double LearnRate, double InvestigationRate, double _reward, int CachCount, double HighCount, int Enhanced, int Low, int High, long lc)
        {
            sw.WriteLine(ErrorMSE.ToString() + "," + LastAction + "," + LearnRate.ToString() + "," + InvestigationRate.ToString() + "," + _reward.ToString() + "," + CachCount.ToString() + "," + HighCount.ToString() + "," + Enhanced.ToString() + "," + Low.ToString() + "," + High.ToString() + "," + lc.ToString());
            Count++;

        }


        public void WriteErrorRate(double _reward, int _action)
        {
            sw.WriteLine( _reward.ToString() + "," + _action.ToString());
            Count++;

        }
        public void WriteTotal(double TotalScore, double zero,double thirty,double sixtysix,double one )
        {
            sw.WriteLine("");
            sw.WriteLine("Total Score, Average");
            double Average = TotalScore / Count;
            sw.WriteLine(TotalScore.ToString() + "," + Average.ToString());
            sw.WriteLine("Average Count");
            sw.WriteLine("0,0.33,0.66,1");
            sw.WriteLine(zero.ToString() + "," + thirty.ToString() + "," + sixtysix.ToString() + "," + one.ToString());

        }
        public void Close()
        {
            sw.Close();
            fs.Close();
        }
    }
}

