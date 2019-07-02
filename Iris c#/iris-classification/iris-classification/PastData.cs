using System;
using System.Linq;


namespace iris_classification
{
    class PastData
    {
         double[] Target;
         double[] Result;
         public double errorlevel;
       
         double [] Input;
         int iAge;
         int Arraysize;
         int Type;
    
         

        public PastData() { }

        public PastData(double [] _Target, double[] _Result, Random _RND,  double _Error, double [] _inputs, int _Type)
        {
            Target = _Target;
            Result = _Result;
            Input = _inputs;
            Arraysize = 5;
            iAge = _RND.Next(1, 5000);
            Type = _Type;
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
            iAge--;
            return Input;
        }

        
        public double[] GetTarget()
        {
            return Target;
        }

        public Boolean DeleteMe()
        {
            Boolean DeleteMe = false;

            if (iAge < 0)
            {
                DeleteMe = true;
            }

            return DeleteMe;
        }

        
        public void SetTarget(double[] _Target)
        {
            Target = _Target;
        }

        public int GetTypeCode()
        {
            return Type;
        }

    }
}
