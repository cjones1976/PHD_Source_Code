using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Figureof8Race
{

    public class Car //: RLPolicy 
    {
        // car must also scan under wheels to find out if on tarmac
        
        Vector2 carLocation;
        float VelocityJump = 0;
        float carHeading;
        float carSpeed;
        Color CarColour;
        double[] OldWheels = { 1, 0, 0, 1 };
        float steerAngle;
        float wheelBase; // the distance between the two axles
        Texture2D mCar;
        Texture2D spot;
        int CarWidth, CarHeight;
        float MaxForwardSpeed = 3;
        float CurrentMaxForwardSpeed = 2;
        float MinForwardSpeed = -1f;
       // float MaxReveseSpeed = -0.5f;
       // float MinReveseSpeed = 0;
        float returnRate;
        float carDriftHeading;
       
        int Gear = 0; // only gear 1 = forward, 0 = neutral. -1 = reverse; 
        int WorldSizeY, WorldSizeX;
        RoadB2Scanner Wheels;
        RoadB2Scanner RoadB2Scanners;
        Boolean TrainingComplete = false;
        Texture2D [] track;
        
        
        double[] PreviousScanInput;
        int ActionCount = 3;//meeds to be linked to Action;
        Vector2 ResetPos;
        long MoveCount = 0;
    
        public long WriteCount;
        LapCounter TrackCounter;

       // int DelayCount = -5;
       // int Delay = -2;
        Vector2 LastPos = Vector2.Zero;
        RLPolicy NewPolicy;

        Color TrackColour;
        int InputCount;
        Random RND;

        //double possibleInputs;

     
        

        double[] RoadB2ScannerOuput;

        public Car(Vector2 _StartPos, Texture2D CarImg, float CarRotation, Texture2D [] _track, Texture2D mScan, Texture2D mTyre, int[] _scannerAngle, int[] _RoadB2ScannerDistance,
                    GraphicsDeviceManager _graphics, SpriteBatch _spritebatch, Color _TrackColour, int _WorldSizeX, int _WorldSizeY, Color _CarColour)
        {
            WorldSizeX = _WorldSizeX;
            CarColour = _CarColour;
            WorldSizeY = _WorldSizeY;
            mCar = CarImg;
            VelocityJump = 0.5f;
            carHeading = 0;
            carSpeed = 0;
            steerAngle = 0F;
            wheelBase = CarImg.Height;
            CarWidth = mCar.Width;

            CarHeight = mCar.Height;
            carLocation = _StartPos;
            LastPos = carLocation;
            returnRate = 0.01F * VelocityJump;
            TrackColour = _TrackColour;
            track = _track;
            //Texture2D wheel = new Texture2D(_graphics.GraphicsDevice, 5,5); // side of wheel scanner
            int[] WheelPosAngle = { 22, 338, 158, 202 };
            int distance = ((int)Math.Sqrt(((CarWidth / 2 * CarWidth / 2) + (CarHeight / 2 * CarHeight / 2)))) - 5;
            ResetPos = carLocation;
            int[] WheelPosDistance = { distance, distance, distance - 2, distance - 2 };
            spot = mScan;
            Wheels = new RoadB2Scanner(carLocation, track[0], mTyre, WheelPosAngle, WheelPosDistance, _graphics, _spritebatch);
            Wheels.update(carLocation, carDriftHeading, carSpeed);

            RoadB2Scanners = new RoadB2Scanner(carLocation, track[0], mScan, _scannerAngle, _RoadB2ScannerDistance, _graphics, _spritebatch);
            
            RoadB2Scanners.update(carLocation, carDriftHeading, carSpeed);
            //ObjScanners.update(carLocation, carDriftHeading, carSpeed);
            //NewPolicy = new RLPolicy(Wheels.GetRoadB2ScannerCount()*2 + RoadB2Scanners.GetRoadB2ScannerCount(), ActionCount);
            InputCount = RoadB2Scanners.GetRoadB2ScannerCount() + 4;
            RoadB2ScannerOuput = new double[InputCount];
           

            RND = new Random();
            Vector2[] lap = new Vector2[8];
            lap[0] = new Vector2(326,459);
            lap[1] = new Vector2(628,448);
            lap[2] = new Vector2(636,282);
            lap[3] = new Vector2(615,91);
            lap[4] = new Vector2(332,72);
            lap[5] = new Vector2(39,75);
            lap[6] = new Vector2(26,243);
            lap[7] = new Vector2(58,452);
            
            NewPolicy = new RLPolicy(InputCount, ActionCount);
            TrackCounter = new LapCounter(lap);
           

        }

       
        public void ChangeTrack(Texture2D img,int TrackID)
        {
            RoadB2Scanners.ChangeTrack(img);
            Wheels.ChangeTrack(img);
            NewPolicy.TrackID = TrackID;
            Vector2[] lap = new Vector2[8];
            lap[0] = new Vector2(326, 459);
            lap[1] = new Vector2(628, 448);
            lap[2] = new Vector2(636, 282);
            lap[3] = new Vector2(615, 91);
            lap[4] = new Vector2(347, 9);
            lap[5] = new Vector2(67, 138);
            lap[6] = new Vector2(441, 264);
            lap[7] = new Vector2(74, 449);

            
            TrackCounter.Updatesplits(lap);
            Reset();
        }

        public void CloseFile()
        {
            NewPolicy.CloseFile();
        }


        //}

        public void GearUp()
        {
            if (Gear < 1)
            {
                Gear++;
            }

        }


        public void Reset()
        {


           
        }

        public void Restart()
        {

            carLocation = ResetPos;
            carHeading = 0;
            carSpeed = 0;
            steerAngle = 0F;
           // TrackCounter.ResetLapCount();
     
        }

        public void GearDown()
        {
            if (Gear > -1)
            {
                Gear--;
            }

        }

        public Rectangle GetRect()
        {
            return new Rectangle((int)carLocation.X, (int)carLocation.Y, CarWidth, CarHeight);
        }


        public void TrackUpdater(int a)
        {
            Wheels.ChangeTrack(track[a]);


            RoadB2Scanners.ChangeTrack(track[a]);
        
        
        }

        public int GetlapCount()
        {
            return TrackCounter.GetLapCount();
        }

        public bool Update(GameTime gameTime)
        {
             // Allows the game to exit

            //add friction
            if (NewPolicy.Reset)
            {
                Restart();
                NewPolicy.Reset = false;
            }
            Rectangle RectCar = new Rectangle((int)carLocation.X, (int)carLocation.Y, CarWidth, CarHeight);
            TrackCounter.CheckPoint(RectCar);

            Wheels.update(carLocation, carDriftHeading, carSpeed);
            RoadB2Scanners.update(carLocation, carDriftHeading, carSpeed);
            CurrentMaxForwardSpeed = FindCurrentMaxSpeed(MaxForwardSpeed);

            double[] mywheels = Wheels.FindColour(Color.Gray, 1, 0, WorldSizeY, WorldSizeX);
            double[] myRoadB2Scanners = RoadB2Scanners.FindColour(Color.Gray, 1, 0, WorldSizeY, WorldSizeX);
           // double[] myObj = ObjScanners.FindCars(0, 1, OtherCarPos);

           
            double[] ID = new double[InputCount];

            //double[] ID = myRoadB2Scanners;
            if (PreviousScanInput == null)
            {
                PreviousScanInput = myRoadB2Scanners;
            }
           
            Array.Copy(myRoadB2Scanners, ID, myRoadB2Scanners.Count());
            Array.Copy(mywheels, 0, ID, myRoadB2Scanners.Count(), mywheels.Count());
           //Array.Copy(mywheels, 0, ID, myRoadB2Scanners.Count()+4, 4);
            //Array.Copy(PreviousScanInput, 0, ID, myRoadB2Scanners.Count()+4, PreviousScanInput.Count());

            PreviousScanInput = myRoadB2Scanners;

            double[] Result = NewPolicy.FeedForward(ID);
            int Action = NewPolicy.GetBestAction(Result);
            //double[] temp = NewPolicy.getActions(RoadB2ScannerOuput);

            // temp action measure
            LastPos = carLocation;
            switch (Action)
            {
                case 0: steerAngle = 0;
                    break;

                case 1: steerAngle = -0.5F;
                    break;
                case 2: steerAngle = 0.5F;
                    break;

            }


            
        
                if (Gear == 1)
                {
                    //carSpeed -= 0.05f;
                
                    carSpeed = MathHelper.Clamp(carSpeed, MinForwardSpeed, CurrentMaxForwardSpeed);
                }

               //make is stear less fast at speed
                if (carSpeed > 1)
                {
                    steerAngle = steerAngle / carSpeed;
                }
                carSpeed = 1;

                DoMovement(ref carHeading, ref wheelBase, ref carLocation, ref steerAngle, ref carDriftHeading, MaxForwardSpeed, ref carSpeed);
                MoveCount++;

                double Distance = Vector2.Distance(carLocation, LastPos);
            
            if (carLocation.X > 0 && carLocation.X < WorldSizeX && carLocation.Y > 0 && carLocation.Y < WorldSizeY)
            {

                Wheels.update(carLocation, carDriftHeading, carSpeed);
                RoadB2Scanners.update(carLocation, carDriftHeading, carSpeed);
                CurrentMaxForwardSpeed = FindCurrentMaxSpeed(MaxForwardSpeed);
                
                double[] mywheels2 = Wheels.FindColour(Color.Gray, 1, 0, WorldSizeY, WorldSizeX );
                // double[] myRoadB2Scanners2 = RoadB2Scanners.FindColour(Color.Gray, 10, -10, WorldSizeY, WorldSizeX);
                double reward = 0;
                reward = (mywheels2[0] + mywheels2[1] + mywheels2[2] + mywheels2[3]) / 4;
           

                double [] Target = (double []) Result.Clone();
                Target[Action] = reward;
                //double ErrorMSE = NewPolicy.LearnError(Target, Result);

                //NewPolicy.DoReport(reward, Result[Action], ErrorMSE, NewPolicy.Training);
                
                //int QNumber = Convert.ToInt32(TempString,2);
                if (NewPolicy.Training == 1)
                {
                    
                    NewPolicy.Learn(Result, reward, Action, ID, Distance, MoveCount, TrackCounter.GetLapCount());
                    WriteCount = NewPolicy.WriteCount;


                }
              

            
          
            }
                else
                {
                    NewPolicy.Learn(Result, 0, Action, ID, Distance, MoveCount, TrackCounter.GetLapCount());
                    Restart();
                }





            return TrainingComplete;
        }

        public void SetTraining(int _Training)
        {
            NewPolicy.Training = _Training;

        }

        public void DoMovement(ref float MoveCarHeading, ref float MoveWheelBase, ref Vector2 MoveCarLocation, ref float MoveSteeringAngle, 
                                ref float MovecarDriftHeading, float MoveMaxForwardSpeed, ref float MoveCarSpeed)
        {
            VelocityJump = 2f;
            Vector2 frontWheel = MoveCarLocation + MoveWheelBase / 2 * new Vector2((float)Math.Cos(MoveCarHeading), (float)Math.Sin(MoveCarHeading));
            Vector2 backWheel = MoveCarLocation - MoveWheelBase / 2 * new Vector2((float)Math.Cos(MoveCarHeading), (float)Math.Sin(MoveCarHeading));

            backWheel += MoveCarSpeed * VelocityJump * new Vector2((float)Math.Cos(MoveCarHeading), (float)Math.Sin(MoveCarHeading));
            frontWheel += MoveCarSpeed * VelocityJump * new Vector2((float)Math.Cos(MoveCarHeading + MoveSteeringAngle), (float)Math.Sin(MoveCarHeading + MoveSteeringAngle));


            MoveCarLocation = (frontWheel + backWheel) / 2;


            double a, b;
            a = frontWheel.Y - backWheel.Y;
            b = frontWheel.X - backWheel.X;

            MoveCarHeading = (float)Math.Atan2(a, b);

            MovecarDriftHeading = MoveCarHeading + (MoveSteeringAngle * (MoveCarSpeed / MoveMaxForwardSpeed) * 0.8F);


            MoveSteeringAngle = 0;
        }

            

        float FindCurrentMaxSpeed(float _MaxSpeed)
        {

            return 5;
        }

        public int GetReinforcementCount()
        {
            return NewPolicy.ReinforcementCount;
        }

      
        public void Turn(float Angle)
        {
            steerAngle = Angle;
        }

        public float getRotation()
        {
            return carDriftHeading;
        }

        public float GetCarSpeed()
        {
            return carSpeed;
        }

        public Vector2 GetCarPos()
        {
            return carLocation;
        }

        public string GetCacheList()
        {
            string temp = NewPolicy.CacheCount().ToString();

            temp = temp + " : LearnRate = ";
            
            temp = temp + Math.Round( NewPolicy.NewLearnRate,3).ToString();

            temp = temp + " : Random Rate = ";
            temp = temp + Math.Round( NewPolicy.InvestigationRate,3).ToString();

            return temp;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont myFont, bool Show)
        {

            spriteBatch.Draw(mCar, new Rectangle((int)carLocation.X, (int)carLocation.Y, CarWidth, CarHeight),
                new Rectangle(0, 0, mCar.Width, mCar.Height), CarColour, carDriftHeading,
                new Vector2(mCar.Width / 2, mCar.Height / 2), SpriteEffects.None, 0);
            Vector2 NewTextPos = new Vector2(10, 10);
            NewTextPos.Y += 20;
            //spriteBatch.DrawString(myFont, " Speed = " + carSpeed.ToString(), NewTextPos, Color.White);


            if (Show)
            {
                Vector2[] ShowRoadB2ScannerPos = { new Vector2(710, 20), new Vector2(710, 80), new Vector2(680, 20), new Vector2(680, 80)};
                //Wheels.Draw(spriteBatch, carDriftHeading, ShowRoadB2ScannerPos, false, Color.Red);
                
                
                Vector2[] ShowRoadB2ScannerPos2 = { new Vector2(710, 20), new Vector2(710, 80), new Vector2(680, 20), new Vector2(680, 80), new Vector2(750, 50), new Vector2(720, 50), new Vector2(680, 50) };
                
                RoadB2Scanners.Draw(spriteBatch, carDriftHeading, ShowRoadB2ScannerPos2, false, Color.White);
               
                // .Draw(spriteBatch, Car1.getRotation(), ShowRoadB2ScannerPosSteering, true);
                //if (GetNextTartgetValid())
                //{
                //    spriteBatch.Draw(spot, GetNextTartgetPos(), GetNextTartgetColour());
                //}

            }
          

        
        }


    
    }

  
}



