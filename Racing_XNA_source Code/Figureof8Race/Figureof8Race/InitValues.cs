using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;


namespace Figureof8Race
{
    public class InitValues
    {

        public long TrainingTargetCount;
        public string TypeName;
        public String CountingSystem;
        public int Count;

        public int PostTrainingCount;
        public double ComparisionRate;
        public int iterations;
        public string FileName;
        public bool Valid;
        public int Row;



        public InitValues(int[] _count)
        {
            int[] iTemp = new int[2];
            string SetupFileName = "ControlData.csv";
            Count = _count[0];
            Row = _count[1] + 1;
            string temp = "";
            FileStream fs;
            StreamReader sr;

            fs = new FileStream(SetupFileName, FileMode.Open);
            sr = new StreamReader(fs);

            temp = sr.ReadLine();
            for (int icount = 0; icount < Row; icount++)
            {
                temp = sr.ReadLine();
            }
            if (temp == null)
            {
                Valid = false;

            }
            else
            {

                string[] StrArray = temp.Split(',');

                if (Row == Convert.ToInt16(StrArray[0]))
                {
                    //Valid Row
                    Valid = true;
                    TypeName = StrArray[1];
                    TrainingTargetCount = Convert.ToInt64(StrArray[2]);
                    CountingSystem = StrArray[3];
                    PostTrainingCount = Convert.ToInt32(StrArray[4]);
                    ComparisionRate = Convert.ToDouble(StrArray[5]);
                    iterations = Convert.ToInt32(StrArray[6]);
                    FileName = TypeName + CountingSystem + TrainingTargetCount;


                }
                else
                {
                    Valid = false;
                }

            }
        }
   
        
    }
}
