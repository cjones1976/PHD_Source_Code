using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Figureof8Race
{
    class PastData
    {
         double[] Target;
         double[] Result;
         int Action;
         double [] Input;

         int Arraysize;

        public PastData() { }

        public PastData(double [] _Target, double[] _Result, int _Action, double [] _Input)
        {
            Target = _Target;
            Result = _Result;
            Action = _Action;
            Input = _Input;
            Arraysize = Input.Count();
       
        }

        public bool CompareInputs(double [] OtherInput)
        {
            bool tempFlag = true;
            double temp;
            
            for (int icount = 0; icount < Arraysize; icount++)
            {
                temp = Math.Abs( OtherInput[icount] - Input[icount]);


            }

            return tempFlag;
        }

        public double[] GetInput()
        {
            return Input;
        }

        public int GetAction()
        {
            return Action;
        }
        public double[] GetTarget()
        {
            return Target;
        }
    }
}
