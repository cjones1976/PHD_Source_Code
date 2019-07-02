using System;
using System.Collections.Generic;
using System.Linq;


namespace Figureof8Race
{
    public class Action
    {
        float Turn, Throttle;

  
        public Action(float _Turn, float _Throttle)
        {

            Turn = _Turn;
            Throttle = _Throttle;
        }

        public float GetTurn()
        {
            return Turn;
        }
        public float GetThrottle()
        {
            //return Throttle;
            return 2;
        }

    }
}
