using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Figureof8Race
{
    public class RoadB2Scanner
    {

        Vector2[] RoadB2ScannerPositions;
        public Texture2D[] RoadB2ScannerImg;
        int NumberofRoadB2Scanners;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //double aMove = 1;
        Texture2D mTrack;
        //Texture2D mCar;
        int RoadB2ScannerCount = 0;
        Texture2D mScan;
        string LastMemoryScan = "" ;
       

       
        
        
        Vector2 RoadB2ScannerOfset = new Vector2(300, 160);
        //Vector2[] RoadB2ScannerLocations;
       
        RenderTarget2D [] mTrackRender;

        Vector2 mCarPosition;


        //double mCarScale = 1;

        int mScanHeight;
        int mScanWidth;
        //Rectangle mCarArea;

        int[] RoadB2ScannerAngle;
        int[] RoadB2ScannerDistance;
        int[] RoadB2ScannerWeighting;

        public RoadB2Scanner(Vector2 pos, Texture2D _mTrack, Texture2D _mScan, int[] _scannerAngle, int[] _RoadB2ScannerDistance,
                        GraphicsDeviceManager _graphics, SpriteBatch _spritebatch)
        {

            NumberofRoadB2Scanners = _scannerAngle.Count();
            mTrack = _mTrack;

            mScan = _mScan;
            RoadB2ScannerCount = _scannerAngle.Count();

            graphics = _graphics;
            spriteBatch = _spritebatch;
            mScanHeight = mScan.Height;
            mScanWidth = mScan.Width;
            mCarPosition = pos;
            RoadB2ScannerAngle = _scannerAngle;
            RoadB2ScannerDistance = _RoadB2ScannerDistance;
            RoadB2ScannerPositions = new Vector2[NumberofRoadB2Scanners];
            RoadB2ScannerImg = new Texture2D[RoadB2ScannerAngle.Count()];
            mTrackRender = new RenderTarget2D[RoadB2ScannerAngle.Count()];
            for (int icount = 0; icount < RoadB2ScannerAngle.Count(); icount++)
            {
                mTrackRender[icount] = new RenderTarget2D(graphics.GraphicsDevice, mScanWidth,
                   mScanHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            }
            NumberofRoadB2Scanners = RoadB2ScannerAngle.Count();
            update(pos, 0, 0);
            

        }

        public void UpdateTrack(Texture2D a)
        {
            mTrack = a;
        }

        public RoadB2Scanner(Vector2 pos, Texture2D _mTrack, Texture2D _mScan, int[] _scannerAngle, int[] _RoadB2ScannerDistance,
                        GraphicsDeviceManager _graphics, SpriteBatch _spritebatch, int [] Weighting)
        {

            NumberofRoadB2Scanners = _scannerAngle.Count();
            mTrack = _mTrack;

            mScan = _mScan;
            RoadB2ScannerCount = _scannerAngle.Count();

            graphics = _graphics;
            spriteBatch = _spritebatch;
            mScanHeight = mScan.Height;
            mScanWidth = mScan.Width;
            mCarPosition = pos;
            RoadB2ScannerAngle = _scannerAngle;
            RoadB2ScannerDistance = _RoadB2ScannerDistance;
            RoadB2ScannerPositions = new Vector2[NumberofRoadB2Scanners];
            RoadB2ScannerImg = new Texture2D[RoadB2ScannerAngle.Count()];
            mTrackRender = new RenderTarget2D[RoadB2ScannerAngle.Count()];
            RoadB2ScannerWeighting = Weighting;

            for (int icount = 0; icount < RoadB2ScannerAngle.Count(); icount++)
            {
                mTrackRender[icount] = new RenderTarget2D(graphics.GraphicsDevice, mScanWidth,
                   mScanHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            }
            NumberofRoadB2Scanners = RoadB2ScannerAngle.Count();
            update(pos, 0, 0);

        }


        public void update(Vector2 CurrentLocation, float Rotation, float Speed)
        {
             RoadB2ScannerPositions = GetRoadB2ScannerPositions(CurrentLocation, Rotation, Speed);
             RoadB2ScannerImg = getScanImages(RoadB2ScannerPositions, Rotation);
        }

        Vector2[] GetRoadB2ScannerPositions(Vector2 CurrentPos, float Rotation, float Speed)
        {

            Vector2[] MyRewards = new Vector2[NumberofRoadB2Scanners];
            float aCarRotation;

            for (int icount = 0; icount < NumberofRoadB2Scanners; icount++)
            {
                aCarRotation = Rotation;

                RoadB2ScannerOfset = CurrentPos;
                //float Angle = 0;
                double OfSet;
                OfSet = RoadB2ScannerDistance[icount];

                aCarRotation = MathHelper.WrapAngle(Rotation + MathHelper.ToRadians(RoadB2ScannerAngle[icount]));


                RoadB2ScannerOfset.Y += (int)(RoadB2ScannerDistance[icount] * Math.Sin(aCarRotation));
                RoadB2ScannerOfset.X += (int)(RoadB2ScannerDistance[icount] * Math.Cos(aCarRotation));

                MyRewards[icount] = RoadB2ScannerOfset;
            }

            return MyRewards;

        }

        Texture2D[] getScanImages(Vector2[] SensorPos, float aCarRotation)
        {
            
            Texture2D [] aCollisionCheck = new Texture2D[NumberofRoadB2Scanners];
            for (int icount = 0; icount < SensorPos.Count(); icount++)
            {
                int aXPosition = (int)SensorPos[icount].X;
                int aYPosition = (int)SensorPos[icount].Y;
                aCollisionCheck[icount] = CreateCollisionTexture(aXPosition, aYPosition, aCarRotation, mTrackRender, icount);
            }
           
            return aCollisionCheck;
        }

        public void Draw(SpriteBatch spriteBatch, float Rotation, Vector2[] RoadB2ScannersShowPosition, bool Show, Color MyColour)
        {
            for (int icount = 0; icount < NumberofRoadB2Scanners ; icount++)
            {
                spriteBatch.Draw(mScan, new Rectangle((int)RoadB2ScannerPositions[icount].X, (int)RoadB2ScannerPositions[icount].Y, mScanWidth, mScanHeight),
                    new Rectangle(0, 0, mScan.Width, mScan.Height), MyColour, Rotation,
                    new Vector2(mScan.Width / 2, mScan.Height / 2), SpriteEffects.None,1);


                if (Show)
                {
                    Rectangle temp = new Rectangle((int)(RoadB2ScannersShowPosition[icount].X), (int)(RoadB2ScannersShowPosition[icount].Y), mScanWidth * 5, mScanHeight * 5);

                    spriteBatch.Draw(RoadB2ScannerImg[icount], temp,
                        new Rectangle(0, 0, mScan.Width, mScan.Height), Color.White, Rotation,
                        new Vector2(mScan.Width / 2, mScan.Height / 2), SpriteEffects.None, 0);
                }

            }

        }



        //Create the Collision Texture that contains the rotated Track image for determing
        //the pixels beneath the Car sprite.
        private Texture2D CreateCollisionTexture(double theXPosition, double theYPosition, float aCarRotation, RenderTarget2D[] lTrackRender, int actionNo)
        {
            //Grab a square of the Track image that is around the sensor
            Texture2D temp;
            graphics.GraphicsDevice.SetRenderTarget(lTrackRender[actionNo]);

            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.White, 0, 0);

            spriteBatch.Begin();

                spriteBatch.Draw(mTrack, new Rectangle(0, 0, mScanWidth, mScanHeight),
                   new Rectangle((int)(theXPosition),
                   (int)(theYPosition), mScanWidth, mScanHeight), Color.White);

            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);


            temp = lTrackRender[actionNo];
            return temp;
        }

        public Texture2D[] GetScanImages()
        {
            return RoadB2ScannerImg;
        }
        public Vector2[] GetRoadB2ScannerLocations()
        {
            return RoadB2ScannerPositions;
        }

        public double[] GetCreateArray(int size, double Value)
        {
            double[] temp = new double[size];
            for (int icount = 0; icount < size; icount++)
            {

                temp[icount] = Value;
            }

            return temp;
        }

        public int GetRoadB2ScannerCount()
        {
            return RoadB2ScannerCount;
        }

        public void ChangeTrack(Texture2D img)
        {
            mTrack = img;
        }

        public Boolean checkInWorld(Vector2 Pos, int WorldSizeX, int WorldSizeY)
        {
            Boolean Temp = true;

            if (Pos.X < 0 || Pos.X >= WorldSizeX)
            {
                Temp = false;
            }
            if (Pos.Y < 0 || Pos.Y >= WorldSizeY)
            {
                Temp = false;
            }
            return Temp;
        }

        public double[] FindColour(Color TargetColour, int FoundReward, int initalreward, int WorldSizeY, int WorldSizeX)
        {
            double[] MyRewards = GetCreateArray(RoadB2ScannerCount, initalreward);
            Texture2D aCollisionCheck;
            for (int icount = 0; icount < RoadB2ScannerCount; icount++)
            {
                
                
                if (checkInWorld(RoadB2ScannerPositions[icount], WorldSizeX, WorldSizeY))
                {
                    aCollisionCheck = RoadB2ScannerImg[icount];

                    int aPixels = RoadB2ScannerImg[icount].Width * RoadB2ScannerImg[icount].Height;
                   
                    //int aPixels = 5 * 5;
                    Color[] myColors = new Color[aPixels];
                    // mCarArea = new Rectangle(aCollisionCheck.Width / 2 - mScanWidth / 2 + 10, aCollisionCheck.Height / 2 - mScanHeight / 2, mScanWidth - 10, mScanHeight);
                    //mCarArea = new Rectangle(aCollisionCheck.Width / 2 - mScanWidth / 2, aCollisionCheck.Height / 2 - mScanHeight / 2, mScanWidth, mScanHeight);
                    Rectangle mCarArea = new Rectangle(0, 0, RoadB2ScannerImg[icount].Width, RoadB2ScannerImg[icount].Height);


                    aCollisionCheck.GetData<Color>(0, mCarArea, myColors, 0, aPixels);
                    //RoadB2ScannerImg[icount] = aCollisionCheck;
                    //Cycle through all of the colors in the Array and see if any of them
                    //are not Gray. If one of them isn't Gray, then the Car is heading off the road
                    //and a Collision has occurred
                    foreach (Color aColor in myColors)
                    {

                        if (aColor == TargetColour)
                        {
                            MyRewards[icount] += 1; ;
                           
                        }

                        

                    }
                    MyRewards[icount] = MyRewards[icount] / aPixels;

                }
               
            }
            
     
         
            return MyRewards;
        }

        public RoadB2Scanner ReturnRoadB2ScannerbyValue(Vector2 Location)
        {
            return new RoadB2Scanner(Location, mTrack, mScan, RoadB2ScannerAngle, RoadB2ScannerDistance, graphics, spriteBatch);
        }

        public string GetMemoryMap()
        {
            return LastMemoryScan;
        }
        public string MemoryScan()
        {
           
            return Convert.ToInt32(LastMemoryScan, 2).ToString();
        }
        
    }
}


