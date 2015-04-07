using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
    /// This is the main type for your gavices;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace GameWorkshop
{
    /// <summary>me
    /// </summary>
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager   graphics;
        SpriteBatch             spriteBatch;

        int                     windowWidth = 800;
        int                     windowHeight = 512;

        //--------------------------------------------
        // Textures:

        Texture2D marioTexture;

        //--------------------------------------------
        // mario variables
        int                     marioXpos           = 400;
        int                     marioYpos           = 256;
        int                     marioWidth          = 32;
        int                     marioHeight         = 32;
        bool                    marioFlip           = true;


        Matrix camera = Matrix.Identity;


					  
					  
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.ApplyChanges();

            //-----------------------------------
            // Begin Level Loading

            // End Level Loading
            //-----------------------------------
          

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

            marioTexture = Content.Load<Texture2D>("mario");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //--------------------------------
            // Mario Update

            // End Mario Update
            //--------------------------------
         
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            

            //-------------------------------------------------
            // draw mario

            spriteBatch.Draw(
                marioTexture,
                new Rectangle(marioXpos, marioYpos, marioWidth, marioHeight),
                null,
                Color.White, 0.0f, Vector2.Zero,
                marioFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            
            // End draw mario
            //-------------------------------------------------

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
