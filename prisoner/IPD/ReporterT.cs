using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace IPD
{



    class ReporterTotal
    {
        string Filename;
        FileStream fs;
        StreamWriter sw;
        public long TrainingCounter = 0;

        long Count = 0;

       


 

        public ReporterTotal(String Letter)

        {
       

            Filename = "Report" + Letter + ".csv";
            fs = new FileStream(Filename, FileMode.Create);
            sw = new StreamWriter(fs);

            sw.WriteLine("TotalScore,Average, ZeroScoreCount, thirdScoreCount, CoopScoreCount, MaxScoreCount");

        }

      
        
        public void WriteTotal(double TotalScore, double zero,double thirty,double sixtysix,double one )
        {
            sw.WriteLine(TotalScore.ToString() + "," + "," +zero.ToString() + "," + thirty.ToString() + "," + sixtysix.ToString() + "," + one.ToString());

        }
        public void Close()
        {
            sw.Close();
            fs.Close();
        }
    }
}

