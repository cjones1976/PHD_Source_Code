using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IPD
{
    class PastData
    {
         double[] Target;
         double[] Result;
         public double errorlevel;
  
         double [] Input;
         int iAge;
         int Arraysize;
     
        public bool DeleteMe = false;

        public PastData() { }

        public PastData(double [] _Target, double[] _Result, double [] _Input, Random _RND, double _Error)
        {
            Target = _Target;
            Result = _Result;
   
            Input = _Input;
            Arraysize = Input.Count();
            iAge = _RND.Next(1, 4000);
    
            errorlevel = _Error;
       
        }

        public bool CompareInputs(double [] OtherInput)
        {
            bool tempFlag = true;
            double temp;
            
            for (int icount = 0; icount < Arraysize; icount++)
            {
                temp = Math.Abs( OtherInput[icount] - Input[icount]);

                if (temp > 0.1f)
                {
                    tempFlag = false;
                    break;
                }


            }

            return tempFlag;
        }

        public double[] GetInput()
        {
            return Input;
        }

     
        public double GetTarget(int a)
        {
            return Target[a];
        }

        public void Age(int a)
        {
            iAge -= a;
            if (iAge < 0)
            {
                DeleteMe = true;
            }
        }

      
        public void SetTarget(double[] _Target)
        {
            Target = _Target;
        }

    }
}
