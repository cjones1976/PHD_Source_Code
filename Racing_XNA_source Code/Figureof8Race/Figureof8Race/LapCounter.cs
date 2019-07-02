using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Figureof8Race
{
    class LapCounter
    {
        Rectangle[] Splits;
        List<String> SplitTimes = new List<string>();
 
        int checkcount = 0;
        int Lap = 0;
        int LastLapPos = -1;

        int[] CheckPoints;
        public LapCounter(Vector2[] SplitPos)
        {
            checkcount = SplitPos.Count();
            Splits = new Rectangle[checkcount];
            CheckPoints = new int[checkcount];
            Rectangle temp = new Rectangle(0, 0, 87, 101);
            for (int icount = 0; icount < Splits.Count(); icount++)
            {
                
                temp.X = (int) SplitPos[icount].X;
                temp.Y = (int) SplitPos[icount].Y;
                Splits[icount] = temp;
                SplitTimes.Clear();

            }



        }

        public void Updatesplits(Vector2[] SplitPos)
        {
            checkcount = SplitPos.Count();
            Splits = new Rectangle[checkcount];
            CheckPoints = new int[checkcount];
            Rectangle temp = new Rectangle(0, 0, 87, 101);
            for (int icount = 0; icount < Splits.Count(); icount++)
            {

                temp.X = (int)SplitPos[icount].X;
                temp.Y = (int)SplitPos[icount].Y;
                Splits[icount] = temp;
                SplitTimes.Clear();
                LastLapPos = -1;

            }


        }

        public Boolean CheckPoint(Rectangle carpos)
        {
            
            bool returnvalue = false;
            for (int icount = 0; icount < checkcount; icount++)
            {

                Rectangle temp = Rectangle.Intersect(carpos, Splits[icount]);

                if (temp != new Rectangle(0, 0, 0, 0) & LastLapPos !=icount)
                {
                    CheckPoints[icount] = 1;
                    returnvalue = true;
                    LastLapPos = icount;
                }
            }

            if ( CheckPoints.Sum() >=checkcount )
            {
                Lap++;
                CheckPoints = new int[checkcount];
            }

            
            return returnvalue;
        }

        public void AddSplit(string a)
        {
            SplitTimes.Add(a);
        }
        public int GetLapCount()
        {
            return Lap;
        }
        public void ResetLapCount()
        {
            CheckPoints = new int[checkcount];
             Lap = 0;
        }
    }


}
