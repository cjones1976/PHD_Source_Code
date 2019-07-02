using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iris_classification
{
    class NetworkParse
    {

        public int Input;
        public int output;
        public string LayerType;
        public bool Bias;
        public string ActivationType;


        public NetworkParse() { }

        public NetworkParse(int _Input,int _output, string _Type,bool _Bias, string _ActiviationType)
        {
            Input = _Input;
            output = _output;
            LayerType = _Type;
            Bias = _Bias;
            ActivationType = _ActiviationType;



        }
    }
}
