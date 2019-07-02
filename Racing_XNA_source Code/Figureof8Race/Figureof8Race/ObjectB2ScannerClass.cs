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
    public class ObjectB2Scanner
    {

        Vector2[] ObjectB2ScannerPositions;
        public Texture2D[] ObjectB2ScannerImg;
        int NumberofObjectB2Scanners;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //double aMove = 1;
        Texture2D mTrack;
        //Texture2D mCar;
        int ObjectB2ScannerCount = 0;
        Texture2D mScan;
        string LastMemoryScan = "" ;
       

       
        
        
        Vector2 ObjectB2ScannerOfset = new Vector2(300, 160);
        //Vector2[] ObjectB2ScannerLocations;
       
        RenderTarget2D [] mTrackRender;

        Vector2 mCarPosition;


        //double mCarScale = 1;

        int mScanHeight;
        int mScanWidth;
        //Rectangle mCarArea;

        int[] ObjectB2ScannerAngle;
        int[] ObjectB2ScannerDistance;
        int[] ObjectB2ScannerWeighting;

        public ObjectB2Scanner(Vector2 pos, Texture2D _mTrack, Texture2D _mScan, int[] _scannerAngle, int[] _ObjectB2ScannerDistance,
                        GraphicsDeviceManager _graphics, SpriteBatch _spritebatch)
        {

            NumberofObjectB2Scanners = _scannerAngle.Count();
            mTrack = _mTrack;

            mScan = _mScan;
            ObjectB2ScannerCount = _scannerAngle.Count();

            graphics = _graphics;
            spriteBatch = _spritebatch;
            mScanHeight = mScan.Height;
            mScanWidth = mScan.Width;
            mCarPosition = pos;
            ObjectB2ScannerAngle = _scannerAngle;
            ObjectB2ScannerDistance = _ObjectB2ScannerDistance;
            ObjectB2ScannerPositions = new Vector2[NumberofObjectB2Scanners];
            ObjectB2ScannerImg = new Texture2D[ObjectB2ScannerAngle.Count()];
            mTrackRender = new RenderTarget2D[ObjectB2ScannerAngle.Count()];
            for (int icount = 0; icount < ObjectB2ScannerAngle.Count(); icount++)
            {
                mTrackRender[icount] = new RenderTarget2D(graphics.GraphicsDevice, mScanWidth,
                   mScanHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            }
            NumberofObjectB2Scanners = ObjectB2ScannerAngle.Count();
            update(pos, 0, 0);
            

        }

        public ObjectB2Scanner(Vector2 pos, Texture2D _mTrack, Texture2D _mScan, int[] _scannerAngle, int[] _ObjectB2ScannerDistance,
                        GraphicsDeviceManager _graphics, SpriteBatch _spritebatch, int [] Weighting)
        {

            NumberofObjectB2Scanners = _scannerAngle.Count();
            mTrack = _mTrack;

            mScan = _mScan;
            ObjectB2ScannerCount = _scannerAngle.Count();

            graphics = _graphics;
            spriteBatch = _spritebatch;
            mScanHeight = mScan.Height;
            mScanWidth = mScan.Width;
            mCarPosition = pos;
            ObjectB2ScannerAngle = _scannerAngle;
            ObjectB2ScannerDistance = _ObjectB2ScannerDistance;
            ObjectB2ScannerPositions = new Vector2[NumberofObjectB2Scanners];
            ObjectB2ScannerImg = new Texture2D[ObjectB2ScannerAngle.Count()];
            mTrackRender = new RenderTarget2D[ObjectB2ScannerAngle.Count()];
            ObjectB2ScannerWeighting = Weighting;

            for (int icount = 0; icount < ObjectB2ScannerAngle.Count(); icount++)
            {
                mTrackRender[icount] = new RenderTarget2D(graphics.GraphicsDevice, mScanWidth,
                   mScanHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            }
            NumberofObjectB2Scanners = ObjectB2ScannerAngle.Count();
            update(pos, 0, 0);

        }


        public void update(Vector2 CurrentLocation, float Rotation, float Speed)
        {
             ObjectB2ScannerPositions = GetObjectB2ScannerPositions(CurrentLocation, Rotation, Speed);
             ObjectB2ScannerImg = getScanImages(ObjectB2ScannerPositions, Rotation);
        }

        Vector2[] GetObjectB2ScannerPositions(Vector2 CurrentPos, float Rotation, float Speed)
        {

            Vector2[] MyRewards = new Vector2[NumberofObjectB2Scanners];
            float aCarRotation;

            for (int icount = 0; icount < NumberofObjectB2Scanners; icount++)
            {
                aCarRotation = Rotation;

                ObjectB2ScannerOfset = CurrentPos;
                //float Angle = 0;
                double OfSet;
                OfSet = ObjectB2ScannerDistance[icount];

                aCarRotation = MathHelper.WrapAngle(Rotation + MathHelper.ToRadians(ObjectB2ScannerAngle[icount]));


                ObjectB2ScannerOfset.Y += (int)(ObjectB2ScannerDistance[icount] * Math.Sin(aCarRotation));
                ObjectB2ScannerOfset.X += (int)(ObjectB2ScannerDistance[icount] * Math.Cos(aCarRotation));

                MyRewards[icount] = ObjectB2ScannerOfset;
            }

            return MyRewards;

        }

        Texture2D[] getScanImages(Vector2[] SensorPos, float aCarRotation)
        {
            
            Texture2D [] aCollisionCheck = new Texture2D[NumberofObjectB2Scanners];
            for (int icount = 0; icount < SensorPos.Count(); icount++)
            {
                int aXPosition = (int)SensorPos[icount].X;
                int aYPosition = (int)SensorPos[icount].Y;
                aCollisionCheck[icount] = CreateCollisionTexture(aXPosition, aYPosition, aCarRotation, mTrackRender, icount);
            }
           
            return aCollisionCheck;
        }

        public void Draw(SpriteBatch spriteBatch, float Rotation, Vector2[] ObjectB2ScannersShowPosition, bool Show, Color MyColour)
        {
            for (int icount = 0; icount < NumberofObjectB2Scanners ; icount++)
            {
                spriteBatch.Draw(mScan, new Rectangle((int)ObjectB2ScannerPositions[icount].X, (int)ObjectB2ScannerPositions[icount].Y, mScanWidth, mScanHeight),
                    new Rectangle(0, 0, mScan.Width, mScan.Height), MyColour, Rotation,
                    new Vector2(mScan.Width / 2, mScan.Height / 2), SpriteEffects.None,1);


                if (Show)
                {
                    Rectangle temp = new Rectangle((int)(ObjectB2ScannersShowPosition[icount].X), (int)(ObjectB2ScannersShowPosition[icount].Y), mScanWidth * 5, mScanHeight * 5);

                    spriteBatch.Draw(ObjectB2ScannerImg[icount], temp,
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
            return ObjectB2ScannerImg;
        }
        public Vector2[] GetObjectB2ScannerLocations()
        {
            return ObjectB2ScannerPositions;
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

        public int GetObjectB2ScannerCount()
        {
            return ObjectB2ScannerCount;
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

        public double[] FindCars( int FoundReward, int initalreward, Rectangle OtherCar)
        {
            double[] MyRewards = GetCreateArray(ObjectB2ScannerCount, initalreward);
         
              
            // find other cars

            for (int icount = 0; icount < ObjectB2ScannerCount; icount++)
            {
                Rectangle temp = new Rectangle((int) ObjectB2ScannerPositions[icount].X,(int)ObjectB2ScannerPositions[icount].Y, (int) ObjectB2ScannerImg[icount].Width,(int) ObjectB2ScannerImg[icount].Height);

                if (temp.Intersects(OtherCar))
                {
                    MyRewards[icount] = FoundReward;
                }
            }
            return MyRewards;
        }

        public ObjectB2Scanner ReturnObjectB2ScannerbyValue(Vector2 Location)
        {
            return new ObjectB2Scanner(Location, mTrack, mScan, ObjectB2ScannerAngle, ObjectB2ScannerDistance, graphics, spriteBatch);
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


