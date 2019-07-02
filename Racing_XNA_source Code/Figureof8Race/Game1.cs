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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D mTrack;
        Texture2D mCar;
        Texture2D mTrackOverlay;
        Texture2D[] Scanner = new Texture2D[5];

        RenderTarget2D mTrackRender;
        RenderTarget2D mTrackRenderRotated;

        Vector2 mCarPosition = new Vector2(300, 150);
        int mCarHeight;
        int mCarWidth;
        float mCarRotation = 0;
        double mCarScale = .2;

        KeyboardState mPreviousKeyboard;

        Rectangle mCarArea;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the images from computer into the Texture2D objects
            mTrack = Content.Load<Texture2D>("Track");
            Scanner[0] = Content.Load<Texture2D>("Scan0");
            Scanner[1] = Content.Load<Texture2D>("Scan1");
            Scanner[2] = Content.Load<Texture2D>("Scan2");
            Scanner[3] = Content.Load<Texture2D>("Scan3");
            Scanner[4] = Content.Load<Texture2D>("Scan4");

            mCar = Content.Load<Texture2D>("Car");
            mTrackOverlay = Content.Load<Texture2D>("TrackOverlay");

            //Scale the height and width of the car appropriately
            mCarWidth = (int)(mCar.Width * mCarScale);
            mCarHeight = (int)(mCar.Height * mCarScale);

            mTrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
                mCarHeight + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);

            //mTrackRender = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
            //    mCarHeight + 100, 1, SurfaceFormat.Color);
            mTrackRenderRotated = new RenderTarget2D(graphics.GraphicsDevice, mCarWidth + 100,
                mCarHeight + 100, false, SurfaceFormat.Color, DepthFormat.Depth24);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            //Check to see if the game should be exited
            if (aGamePad.Buttons.Back == ButtonState.Pressed || aKeyboard.IsKeyDown(Keys.Escape) == true)
            {
                this.Exit();
            }

            //Rotate the Car sprite with the Left Thumbstick or the up and down arrows
            mCarRotation = mCarRotation + (float)(aGamePad.ThumbSticks.Left.X * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            if (aKeyboard.IsKeyDown(Keys.Up) == true || aKeyboard.IsKeyDown(Keys.Left) == true)
            {
                mCarRotation -= (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (aKeyboard.IsKeyDown(Keys.Down) == true || aKeyboard.IsKeyDown(Keys.Right) == true)
            {
                mCarRotation += (float)(1 * 3.0f * gameTime.ElapsedGameTime.TotalSeconds);
            }


            //Setup the Movement increment. If a collision doesn't occur with this movement,
            //then this amount will be applied to the car's current position
            int aMove = (int)(200 * gameTime.ElapsedGameTime.TotalSeconds);

            //Check to see if a collision occured. If a collision didn't occur, then move the sprite
            if (CollisionOccurred(aMove, mCarRotation) == false)
            {
                mCarPosition.X += (float)(aMove * Math.Cos(mCarRotation));
                mCarPosition.Y += (float)(aMove * Math.Sin(mCarRotation));
            }

            mPreviousKeyboard = aKeyboard;

            base.Update(gameTime);
        }


        //This method checks to see if the Sprite is going to move into an area that does
        //not contain all Gray pixels. If the move amount would cause a movement into a non-gray
        //pixel, then a collision has occurred.
        private bool CollisionOccurred(int aMove, float aCarRotation)
        {
            //Calculate the Position of the Car and create the collision Texture. This texture will contain
            //all of the pixels that are directly underneath the sprite currently on the Track image.
            float aXPosition = (float)((-mCarWidth / 2) + (mCarPosition.X + (aMove * Math.Cos(aCarRotation))));
            float aYPosition = (float)((-mCarHeight / 2) + (mCarPosition.Y + (aMove * Math.Sin(aCarRotation))));
            Texture2D aCollisionCheck = CreateCollisionTexture(aXPosition, aYPosition, aCarRotation);

            //Use GetData to fill in an array with all of the Colors of the Pixels in the area of the Collision Texture
            int aPixels = (mCarWidth - 10) * (mCarHeight);
            Color[] myColors = new Color[aPixels];
            mCarArea = new Rectangle(aCollisionCheck.Width / 2 - mCarWidth / 2 + 10, aCollisionCheck.Height / 2 - mCarHeight / 2, mCarWidth - 10, mCarHeight);
            aCollisionCheck.GetData<Color>(0, mCarArea, myColors, 0, aPixels);

            //Cycle through all of the colors in the Array and see if any of them
            //are not Gray. If one of them isn't Gray, then the Car is heading off the road
            //and a Collision has occurred
            bool aCollision = false;
            foreach (Color aColor in myColors)
            {
                //If one of the pixels in that area is not Gray, then the sprite is moving
                //off the allowed movement area
                if (aColor != Color.Gray && aColor != Color.Blue && aColor != new Color(102, 102, 102, 255))
                {
                    aCollision = true;
                    break;
                }
            }

            return aCollision;
        }

        //Create the Collision Texture that contains the rotated Track image for determing
        //the pixels beneath the Car srite.
        private Texture2D CreateCollisionTexture(float theXPosition, float theYPosition, float aCarRotation)
        {
            //Grab a square of the Track image that is around the Car
            graphics.GraphicsDevice.SetRenderTarget(mTrackRender);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(mTrack, new Rectangle(0, 0, mCarWidth + 100, mCarHeight + 100),
                new Rectangle((int)(theXPosition - 50),
                (int)(theYPosition - 50), mCarWidth + 100, mCarHeight + 100), Color.White);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            Texture2D aPicture = mTrackRender;

            //Rotate the snapshot of the area Around the car sprite and return that 
            graphics.GraphicsDevice.SetRenderTarget(mTrackRenderRotated);
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(aPicture, new Rectangle((int)(aPicture.Width / 2), (int)(aPicture.Height / 2),
                aPicture.Width, aPicture.Height), new Rectangle(0, 0, aPicture.Width, aPicture.Width),
                Color.White, -aCarRotation, new Vector2((int)(aPicture.Width / 2), (int)(aPicture.Height / 2)),
                SpriteEffects.None, 0);
            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            return mTrackRenderRotated;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            spriteBatch.Draw(mTrackOverlay, new Rectangle(0, 0, mTrackOverlay.Width, mTrackOverlay.Height), Color.White);
            spriteBatch.Draw(mCar, new Rectangle((int)mCarPosition.X, (int)mCarPosition.Y, mCarWidth, mCarHeight),
                new Rectangle(0, 0, mCar.Width, mCar.Height), Color.White, mCarRotation,
                new Vector2(mCar.Width / 2, mCar.Height / 2), SpriteEffects.None, 0);
            //spriteBatch.Draw(mTrackRenderRotated.GetTexture(), new Vector2(0, 0), Color.White);
            //spriteBatch.Draw(mTrackRenderRotated.GetTexture(), mCarArea, new Rectangle(0, 0, 1, 1), Color.Black);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}