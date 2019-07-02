using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace iris_classification
{
    public class OutputPoint 
    {
        double x;
        double y;
       
        public int TypeCode;
        public Color DotColour;
        double Targetx;
        double Targety;
       // float Targetz;
        public double Gridx;
        public double Gridy;
        public double GridTestx;
        public double GridTesty;

        public OutputPoint()
        { }

        public OutputPoint (double _x, double _y, int _TypeCode, Color _DotColour, double _TargetX, double _TargetY)  
        {
            x = _x;
            y = _y;
            //z = (float)_z;

            Gridx = (x * 500) + 500;//500;
            Gridy = 550 - (y * 500) ;
            GridTestx = (x * 500) + 1050;//500;
            GridTesty = 550 - (y * 500);
      
            TypeCode = _TypeCode;
            DotColour = _DotColour;
            Targetx = _TargetX;
            Targety = _TargetY;
           // Targetz = _TargetZ;
        }

        public double GetReward ()
        {
           
            double Reward = Math.Pow(Targetx - x, 2) + Math.Pow(Targety - y, 2);
            //Reward = Reward / 2;
            Reward = Math.Sqrt(Reward);
            Reward = 1 - Reward;
            return Reward;
        }

        public double [] GetTarget()
        {
            double[] temp = new double[2];
            temp[0] = Targetx;
            temp[1] = Targety;
            

            return temp;

        }

        public int GetTypeCode()
        {
            return TypeCode;
        }
        
    }
}
