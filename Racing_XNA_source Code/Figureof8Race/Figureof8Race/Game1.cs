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
using System.IO;


namespace Figureof8Race
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int ScreenSizeX = 800, ScreenSizeY = 600;
        
        SpriteFont myFont;
        Texture2D mTrack;
        Texture2D mCar;
        Texture2D[] mTrack2;
        Texture2D mScan, mTyre;
        Vector2 RoadB2ScannerOfset = new Vector2(300, 160);
       // Boolean ForceRotation = false;
        //int TrainingCount = 5000;
       // double aMove = 200;
        Vector2 FollowPos;
        RenderTarget2D[] mTrackRender = new RenderTarget2D[5];
        //RenderTarget2D mTrackRenderRotated;

        //int Training = 0;
        Boolean DisplayMemory = false;
        //Boolean DisplayTargets = false;
        Vector2 mCarPosition1 = new Vector2(150, 500);
        Vector2 mCarPosition2 = new Vector2(250, 250);
        int mCarHeight;
        int mCarWidth;
        float mCarRotation = 0;
        double mCarScale = 1;
        //int UpdateCount = 0;
        int mScanHeight;
        int mScanWidth;
       // int Training = 1;

        // data
        long runcount = 1000000;
        long ImageChange = 15000 / 2;
        long Counter;
        Boolean ChangeImage = false;
        int[] Count;
 
        // 0 = new
        // 1 = repeated
        // 2 = standard
        Random rnd = new Random();
        

        Car Car1;


        int[] RoadB2ScannerAngle = { 00, 45, 315, 25, 336, 25, 335, 00, 45, 315, 25, 336, 300, 60,135,225,180,100,260 };
        int[] RoadB2ScannerDistance = { 25, 30, 30, 30, 30, 45, 45, 55, 60, 60, 70, 70, 40, 40,30,30,30,30,30 };



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = ScreenSizeX;
            graphics.PreferredBackBufferHeight = ScreenSizeY;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mTrack2 = new Texture2D[2];
            base.Initialize();

            
            ImageChange = runcount / 2;
            Counter = runcount;
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Count = GetNextCarCount();

          
            //Load the images from computer into the Texture2D objects
            mTrack = Content.Load<Texture2D>("Track3");
            mScan = Content.Load<Texture2D>("Scan");
            mTyre = Content.Load<Texture2D>("Tyre");
            mCar = Content.Load<Texture2D>("Car");
            mTrack2[0] = mTrack;
            mTrack2[1] = Content.Load<Texture2D>("Track2");
       
        

            //Scale the height and width of the car appropriately
            mCarWidth = (int)(mCar.Width * mCarScale);
            mCarHeight = (int)(mCar.Height * mCarScale);
            mScanWidth = (int)(mScan.Width * mCarScale );
            mScanHeight = (int)(mScan.Height * mCarScale);
            myFont = Content.Load<SpriteFont>("SpriteFont1");
            
     
            Car1 = new Car(mCarPosition1, mCar, mCarRotation, mTrack2, mScan, mTyre, RoadB2ScannerAngle, RoadB2ScannerDistance, graphics, spriteBatch, Color.Gray, ScreenSizeX, ScreenSizeY, Color.Red);
            
            //Car1.initValues(RunData);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
           
            //Car1.CloseFile();


        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            GamePadState aGamePad = GamePad.GetState(PlayerIndex.One);
            KeyboardState aKeyboard = Keyboard.GetState();
            MouseState aMouse = Mouse.GetState();

        
            FollowPos.X = aMouse.X;
            FollowPos.Y = aMouse.Y;

            //SteeringPolicy.MouseLocation = FollowPos;




            if (runcount > 0)
            {

                Car1.Update(gameTime);
                runcount--;
                if (runcount < ImageChange && ChangeImage == false)
                {
                    mTrack = mTrack2[1];
                    Car1.ChangeTrack(mTrack,1);
                    ChangeImage = true;
                }
            }
            else
            {
                Exit();
            }

           

           
            base.Update(gameTime);
        }

       
        //This method checks to see if the Sprite is going to move into an area that does
        //not contain all Gray pixels. If the move amount would cause a movement into a non-gray
        //pixel, then a collision has occurred.
      

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (!DisplayMemory)
            {
                spriteBatch.Draw(mTrack, new Rectangle(0, 0, mTrack.Width, mTrack.Height), Color.White);

                // draw cars

               // 
                Car1.Draw(spriteBatch,myFont,true);

                 
              
      

                spriteBatch.Draw(mScan, new Rectangle((int)FollowPos.X,(int)FollowPos.Y, 10, 10), Color.Yellow);
              
            }

            Window.Title = "Cache Count " + Car1.GetCacheList() + " , Epoch " + (Counter - runcount) + " , Lap Count " + Car1.GetlapCount().ToString(); 
            

            spriteBatch.End();

            base.Draw(gameTime);
        }


        public int[] GetNextCarCount()
        {

            int[] iTemp = new int[2];
            string FileName = "Count.txt";



            FileStream fs;
            StreamReader sr;

            fs = new FileStream(FileName, FileMode.Open);
            sr = new StreamReader(fs);
            string temp = sr.ReadLine();
            iTemp[0] = Convert.ToInt32(temp);

            temp = sr.ReadLine();
            iTemp[1] = Convert.ToInt32(temp);

            fs.Close();

            return iTemp;
        }

        public void IncrementNextCarCount(int a)
        {

            string FileName = "count.txt";


            if (Count[0] > a)
            {
                Count[0] = 0;
                Count[1] = Count[1] + 1;
            }
            else
            {
                Count[0]++;
            }

            
            
             using (StreamWriter writer =
                new StreamWriter(FileName ))
                {
                 
                     
                        writer.WriteLine( Count[0].ToString());
                        writer.WriteLine( Count[1].ToString());
                        writer.Close();
                }

           
           
            
        }
    }
}
