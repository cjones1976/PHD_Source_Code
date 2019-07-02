using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPD
{
    class Prisoner
    {

        RLPolicy NewPolicy;
        double[] Input = new double [3];
        int LastAction = 0;
        int OpponentsLastOption = 0;
        int ZeroScoreCount;
        int thirdScoreCount;
        int CoopScoreCount;
        int MaxScoreCount;
        double Score;
        double TotalScore;
        int Action = 0;
  
        Random rnd = new Random();
        bool MLPUSed = false;
        public Prisoner(string _Name, int _input, int _output, int _HiddenLayer, string LearnType, int ELCount, int CacheSize)
        {

            LastAction = rnd.Next(0,2);
            OpponentsLastOption = rnd.Next(0, 2);
            
            NewPolicy = new RLPolicy(_input, _output, _HiddenLayer, _Name, LearnType, CacheSize);
            MLPUSed = true;
            

        }

        public Prisoner(string _Name)
        {
            //titforTate kind
            LastAction = 0;
            OpponentsLastOption = 0;
            MLPUSed = false;
            NewPolicy = new RLPolicy(0, 0, 0, _Name, "TIT", 0);

        }

        public Prisoner()
        {
          
        }


        // P1 & P2 Coop = both get 0.66
        // p1 coop p2 defects p1= 0 p2 =1;
        // p1 defects p2 coop p1= 1 p2 = 0;
        // p1 and p2 defects both get 0.33

        // coop = 0;
        // defect = 1;

        public int MakeChoice(int runno)
        {
            Input[0] = LastAction;
            
            Input[1] = OpponentsLastOption*-1;


            Input[2] = 0;
            
            if (MLPUSed)
            {
                
                Action = NewPolicy.GetBestAction(Input,runno);
                //if (rnd.Next(0, 100) > 90)
                //{
                //    Action = rnd.Next(0, 2);

               // }
            }
            else
            {

               
              
                    Action = 1;
                    if (OpponentsLastOption == 0)
                    {
                        Action = 0;
                    }
              


            }

            Input[2] = Action;
            return Action;
            
        }

        public void AddScore(double r)
        {
            Score = r;
            TotalScore += Score;

            if (r == 0)
            {
                ZeroScoreCount++;
            }

            if (r == 0.33)
            {
                thirdScoreCount++;
            }
            
            if (r == 0.66)
            {
                CoopScoreCount++;
            }

             if (r == 1)
            {
                MaxScoreCount++;
            }

            


            if (MLPUSed)
            {
                NewPolicy.Learn( r, Input, Action);
            }
            else
            {
                NewPolicy.WriteData(r, Action);
            }
        }

        public double GetScore()
        {
            return TotalScore;
        }

        public int GetAction()
        {
            
            LastAction = Action;
            return Action;
        }

        public void OppLastAction(int OppLast)
        {
            OpponentsLastOption = OppLast;
        }

        public void Close(ReporterTotal P1Total)
        {      

            NewPolicy.CloseFile();
            P1Total.WriteTotal(TotalScore, ZeroScoreCount, thirdScoreCount, CoopScoreCount, MaxScoreCount);
        }
    }
}
