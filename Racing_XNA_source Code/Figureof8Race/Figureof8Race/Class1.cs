using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Figureof8Race
{
    public class MathsTools
    {
        double[] data;
        double size;

        public MathsTools(double[] data)
        {
            this.data = data;
            size = data.Length;
        }

        double getMean()
    {
        double sum = 0.0;
        for (int i = 0; i < data.Length; i++ )
            sum += data[i];
            return sum/size;
    }

        double getVariance()
        {
            double mean = getMean();
            double temp = 0;
            double a = 0;
            for (int i = 0; i < data.Length; i++)
                a = data[i];
                temp += (mean-a)*(mean-a);
                return temp/size;
        }

        public double getStdDev()
        {
            return Math.Sqrt(getVariance());
        }


    }
}
