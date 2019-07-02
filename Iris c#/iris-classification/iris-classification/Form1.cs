using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;



namespace iris_classification
{

  
    public partial class Form1 : Form
    {
  
   
        List<OutputPoint> Points = new List<OutputPoint>();
        List<OutputPoint> PointsTest = new List<OutputPoint>();
        int linecount = 150;
        double[,] inputarray;
        double  Reward;
        double TrainingCorrect =0;
        string TestingString = "Learning";
        int[] Answer;
        int currentPos = 0;
        int SetwriteCount = 0;
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Points.Clear();
            PointsTest.Clear();
            TestingString = "Learning";
            int input = Convert.ToInt32( txtInput.Text);
            int hidden = Convert.ToInt32(txtHidden.Text);
            int output = Convert.ToInt32(txtOutputs.Text);
            double LearnRate = Convert.ToDouble(txtLearnRate.Text);
            txtCorrect.Text = "";
            txtTestCount.Text = "";
            txtVer.Text = "";
            txtSetPer.Text = "";
            txtVir.Text = "";
            this.Refresh();
            int SetYes = 0,SetNo = 0, VerYes = 0, VerNo= 0, VirYes =0 , VirNo = 0;
            int StartRandom = 0;
            int EndRandom = 121;
            string Enhanced = "S";
            TrainingCorrect = 0;
            if (chkEnhanced.CheckState == CheckState.Checked)
            {
                Enhanced = "E";
            }
            int writecount = 0;
            int repeat = Convert.ToInt32(txtRepeat.Text);
            int epochCount = Convert.ToInt32(txtEpochs.Text);
            RLPolicy learn = new RLPolicy(input,output, hidden, repeat, Enhanced,LearnRate);
            string fileName = "test" + LearnRate.ToString() + Enhanced + epochCount.ToString() + "H=" + hidden.ToString()+ "B=" + Enhanced.ToString() +  ".csv";
            
            if (epochCount > 1000000)
            {
                writecount = epochCount / 1000000;
                SetwriteCount = writecount;
            }
           
            int addcounter = 0;

            using (StreamWriter writer = new StreamWriter(fileName))
            {

                writer.Write("epoch, TargetX, Target y,Target z, type,  Result x, Result y,Result z, Reward,Testing, learnrate, Identified Correctly, learnCount, Total Error, High Error (AverageError), Set, ver, vir, Total, highpos, \n");


                int display = (epochCount / 10);
                int TESTING = epochCount -5000;
                txtTestCount.Text = (epochCount - TESTING).ToString();
                for (int icount = 0; icount < epochCount; icount++)
                {
                    currentPos = rnd.Next(StartRandom, EndRandom);

                    double[] feed = new double[4];
                    feed[0] = inputarray[currentPos, 0];
                    feed[1] = inputarray[currentPos, 1];
                    feed[2] = inputarray[currentPos, 2];
                    feed[3] = inputarray[currentPos, 3];

                    double[] result = learn.FeedForward(feed);

                    OutputPoint x = new OutputPoint();

                    switch (Answer[currentPos])
                    {
                        case 1:
                            x = new OutputPoint(result[0], result[1], 1, Color.Red, 0.48D, 0.24D);
                            Reward = x.GetReward();
                            break;

                        case 2:
                            x = new OutputPoint(result[0], result[1], 2, Color.Lime, 0.8D, 0.75D);
                            Reward = x.GetReward();
                            break;

                        case 3:
                            x = new OutputPoint(result[0], result[1], 3, Color.Blue, 0.2D, 0.77D);
                            Reward = x.GetReward();
                            break;

                    }
             
                    double[] Target = x.GetTarget();

                    Points.Add(x);

                    string Identified = "N";
                    if (Reward > 0.70)
                    {
                        Identified = "Y";
                        TrainingCorrect++;
                    }

                    if (TESTING < 1)
                    {
                        EndRandom = 150;
                        StartRandom = 120;
                        TestingString = "Testing";

                        if (x.GetTypeCode() == 1)
                        {
                            if (Identified == "Y")
                            {
                                SetYes++;
                            }
                            else
                            {
                                SetNo++;
                            }
                        }
                        if (x.GetTypeCode() == 2)
                        {
                            if (Identified == "Y")
                            {
                                VerYes++;
                            }
                            else
                            {
                                VerNo++;
                            }
                        }
                        if (x.GetTypeCode() == 3)
                        {
                            if (Identified == "Y")
                            {
                                VirYes++;
                            }
                            else
                            {
                                VirNo++;
                            }
                        }

                        PointsTest.Add(x);
                      //  Points.Remove(x);
                        writecount = -1;
                    }

                    if (TESTING > 1)
                    {
                        learn.Learn(result, Reward, Target, feed, x.GetTypeCode());
                    }
                    writecount--;
                    if (writecount < 0)
                    {
                        writer.Write(icount.ToString());
                        writer.Write(",");
                        writer.Write(Target[0].ToString());
                        writer.Write(",");
                        writer.Write(Target[1].ToString());
                        writer.Write(",");
                        //writer.Write(Target[2].ToString());
                        writer.Write(",");
                        writer.Write(x.GetTypeCode().ToString());
                        writer.Write(",");
                        writer.Write(result[0].ToString());
                        writer.Write(",");
                        writer.Write(result[1].ToString());
                        writer.Write(",");
                        //writer.Write(result[2].ToString());
                        writer.Write(",");
                        writer.Write(Reward.ToString());
                        writer.Write(",");
                        
                        writer.Write(TestingString.ToString());
                        writer.Write(",");

                        writer.Write(learn.NewLearnRate.ToString());
                        writer.Write(",");
                        writer.Write(Identified);
                        writer.Write(",");
                        writer.Write(learn.GetLearnCount().ToString());
                        writer.Write(",");
                        writer.Write(learn.TotalError.ToString());
                        writer.Write(",");
                        double previousError = learn.GetHighError();
                        writer.Write(previousError.ToString());
                        writer.Write(",");
                        writer.Write(learn.Getmemory());
                        writer.Write(",");
                        writer.Write(learn.getHighPos());
                        writer.Write(",");
                        writer.Write(feed[0]);
                        writer.Write(",");
                        writer.Write(feed[1]);
                        writer.Write(",");
                        writer.Write(feed[2]);
                        writer.Write(",");
                        writer.Write(feed[3]);
                        writer.Write("\n");

                        writecount = SetwriteCount;
                    }
                    
                    display--;
                    TESTING--;
                    addcounter++;
                    if (display == 0)
                    {
                        //this.Refresh();
                        display = epochCount / 10;
                       
                        StartRandom = 0;
                    }
                    if (TESTING == 0)
                    {
                     

                    }

                    if (addcounter > 100)
                    {
                        addcounter = 0;
                    }




                }



               

                double percorrect = (TrainingCorrect / epochCount) * 100;
                txtCorrect.Text = percorrect.ToString() + "%";

                double temp = SetNo + SetYes;
                temp = SetYes / temp;
                temp = temp * 100;

                txtSetPer.Text = temp.ToString() + " %";
                temp = VerNo + VerYes;
                temp = VerYes / temp;
                temp = temp * 100;
                txtVer.Text = temp.ToString() + " %";
                temp = VirNo + VirYes;
                temp = VirYes / temp;
                temp = temp * 100;
                txtVir.Text = temp.ToString() + " %";
                writer.Write("\n");
                writer.Write("\n");
                writer.Write("\n");
                writer.Write(txtCorrect.Text);
                writer.Write("\n");
                writer.Write(txtSetPer.Text);
                writer.Write("\n");
                writer.Write(txtVer.Text);
                writer.Write("\n");
                writer.Write(txtVir.Text);
                writer.Write("\n");
            }
            this.Refresh();
        }
        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            Pen ThinkblackPen = new Pen(Color.Black, 3);
            Pen ThinblackPen = new Pen(Color.Black, 1);

            e.Graphics.DrawRectangle(ThinkblackPen, 497, 47, 506, 506);
            //e.Graphics.DrawRectangle(ThinblackPen, 500, 50, 100, 100);
            //e.Graphics.DrawRectangle(ThinblackPen, 600, 150, 100, 100);

            e.Graphics.DrawRectangle(ThinkblackPen, 1047, 47, 506, 506);

            //e.Graphics.DrawRectangle(ThinblackPen, 750, 50, 100, 100);
            //e.Graphics.DrawRectangle(ThinblackPen, 850, 150, 100, 100);


            float RedX = 0.48F;
            float RedY = 0.24F;

            float BlueX = 0.2F;
            float BlueY = 0.77F;

            float LimeX = 0.8F;
            float LimeY = 0.75F;

            float RSize = 300F;
            float HRSize = RSize / 2;


            // pos 1
            float Gridx = (RedX * 500) + 500- HRSize;//500;
            float Gridy = 550 - (RedY * 500)- HRSize;
            float TGridx = (RedX * 500) + 500;//500;
            float TGridy = 550 - (RedY * 500);
            float  GridTestx = (RedX * 500) + 1050- HRSize;//500;
            float  GridTesty = 550 - (RedY * 500)- HRSize;

            Pen mypen2 = new Pen(Color.Red, 1);
            e.Graphics.DrawEllipse(mypen2, Gridx, Gridy, RSize, RSize);
            e.Graphics.DrawEllipse(mypen2, GridTestx, GridTesty, RSize, RSize);
            e.Graphics.DrawRectangle(mypen2, TGridx, TGridy, 1, 1);

            // pos 2
            Gridx = (LimeX * 500) + 500 - HRSize;//500;
            Gridy = 550 - (LimeY * 500) - HRSize;
            TGridx = (LimeX * 500) + 500;//500;
            TGridy = 550 - (LimeY * 500);
            GridTestx = (LimeX * 500) + 1050 - HRSize;//500;
            GridTesty = 550 - (LimeY * 500) - HRSize;

            mypen2 = new Pen(Color.Lime, 1);
            e.Graphics.DrawEllipse(mypen2, Gridx, Gridy, RSize, RSize);
            e.Graphics.DrawEllipse(mypen2, GridTestx, GridTesty, RSize, RSize);
            e.Graphics.DrawRectangle(mypen2, TGridx, TGridy, 1, 1);


            // pos 3
            Gridx = (BlueX * 500) + 500 - HRSize;//500;
            Gridy = 550 - (BlueY * 500) - HRSize;
            TGridx = (BlueX * 500) + 500;//500;
            TGridy = 550 - (BlueY * 500);
            GridTestx = (BlueX * 500) + 1050 - HRSize;//500;
            GridTesty = 550 - (BlueY * 500) - HRSize;

            mypen2 = new Pen(Color.Blue, 1);
            e.Graphics.DrawEllipse(mypen2, Gridx, Gridy, RSize, RSize);
            e.Graphics.DrawEllipse(mypen2, GridTestx, GridTesty, RSize, RSize);
            e.Graphics.DrawRectangle(mypen2, TGridx, TGridy, 1, 1);


            //Targets
            if (Points != null)
            {
                for (int icount = 0; icount < Points.Count; icount++)
                {
                    Pen mypen = new Pen(Points[icount].DotColour, 1);
                    e.Graphics.DrawRectangle(mypen, (float) Points[icount].Gridx, (float)Points[icount].Gridy, 1, 1);
                    mypen.Dispose();

                }
            }

            //Targets
            if (PointsTest != null)
            {
                for (int icount = 0; icount < PointsTest.Count; icount++)
                {
                    Pen mypen = new Pen(PointsTest[icount].DotColour, 1);
                    e.Graphics.DrawRectangle(mypen, (float)PointsTest[icount].GridTestx, (float)PointsTest[icount].GridTesty, 1, 1);
                    mypen.Dispose();

                }
            }


            ThinkblackPen.Dispose();
            ThinblackPen.Dispose();

            lbltrainingCount.Text = Points.Count().ToString();
            lblTestingCount.Text = PointsTest.Count().ToString();
        }


        private void LoadArray_Click(object sender, EventArgs e)
        {
            Points.Clear();
            Learn.Enabled = true;
           // Points.Add(new OutputPoint(10, 10, "test", 1, Color.Red));

            //this.Refresh();
            //this is where I am attempting to read from the .csv
            StreamReader LineData = new StreamReader(File.OpenRead("irisdatanormalised.csv"));
            inputarray = new double[linecount, 4];
            Answer = new int[linecount];
            try
            {

                
                

                for (int loop = 0; loop < linecount; loop++)
                {
                    string input = LineData.ReadLine();
                    string[] split = input.Split(',');
                    inputarray[loop, 0] = Convert.ToDouble(split[0]);
                    inputarray[loop, 1] = Convert.ToDouble(split[1]);
                    inputarray[loop, 2] = Convert.ToDouble(split[2]);
                    inputarray[loop, 3] = Convert.ToDouble(split[3]);

                    switch (split[4])
                    {
                        case "Iris-virginica":
                            Answer[loop] = 3;
                            break;

                        case "Iris-setosa":
                            Answer[loop] = 1;
                            break;

                        case "Iris-versicolor":
                            Answer[loop] = 2;
                            break;

                    }

                }

                      
     


        }
            catch (System.Exception)
        {
            MessageBox.Show("An error occured during the file read...","File Read Error");
        }
        finally
        {
                LineData.Close();
        }

        }

      

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void txtEpochs_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
