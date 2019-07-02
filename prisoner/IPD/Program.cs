using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPD
{
    class Program
    {
        //details of reward

        // P1 & P2 Coop = both get 0.66
        // p1 coop p2 defects p1= 0 p2 =1;
        // p1 defects p2 coop p1= 1 p2 = 0;
        // p1 and p2 defects both get 0.33

        // coop = 0;
        // defect = 1;



        static void Main(string[] args)
        {

            ReporterTotal P1Total = new ReporterTotal("P1Total");
            ReporterTotal P2Total = new ReporterTotal("P2Total");

            Prisoner P1 = new Prisoner();
            Prisoner P2 = new Prisoner();
            for (int tempcount = 0; tempcount < 9; tempcount++)
            {
                string NameP1 = "P1S" + tempcount.ToString();
                string NameP2 = "P2S" + tempcount.ToString();
                P1 = new Prisoner(NameP1, 3, 1, 10, "S", 100, 10);
                //P2 = new Prisoner(NameP2, 3, 1, 10, "S", 100, 10);
                P2 = new Prisoner(NameP2);//tft
                //P1 = new Prisoner(NameP1);//tft

                for (int a = 0; a < 1000000; a++)
                {
                    int ActionP1 = P1.MakeChoice(tempcount);
                    int ActionP2 = P2.MakeChoice(tempcount);


                    if (ActionP1 == 0)
                    {
                        //P1 coop
                        if (ActionP2 == 0)
                        {
                            //P2 coop
                            P1.AddScore(0.66);
                            P2.AddScore(0.66);
                        }
                        else
                        {
                            //P2 Defect
                            P1.AddScore(0);
                            P2.AddScore(1);
                        }
                    }
                    else
                    {
                        // P1 defects
                        if (ActionP2 == 0)
                        {
                            //P2 coop
                            P1.AddScore(1);
                            P2.AddScore(0);
                        }
                        else
                        {
                            //P2 Defect
                            P1.AddScore(0.33);
                            P2.AddScore(0.33);
                        }
                    }


                    P1.OppLastAction(P2.GetAction());
                    P2.OppLastAction(P1.GetAction());

                }

    
                P1.Close(P1Total);
                P2.Close(P2Total);
            }

            P1Total.Close();
            P2Total.Close();
        }

        
    }
}
