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
        Texture2D tilesetTexture;

        Texture2D backgroundTexture;

        //--------------------------------------------
        // mario variables
        int                     marioXpos           = 400;
        int                     marioYpos           = 256;

        int                     marioWidth          = 32;
        int                     marioHeight         = 32;

        bool                    marioFlip           = true;

        int                     marioMoveSpeed      = 5;

        int                     marioJumpForce      = 50;
        int                     marioMaxJumpForce   = 23;
        int                     gravity             = 9;


        Matrix camera = Matrix.Identity;

        int[,] map = 
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 941, 942, 943, 0, 0, 0, 540, 540, 540, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 941, 942, 943, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 612, 613, 613, 613, 613, 613, 613, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 941, 942, 943, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 941, 942, 943, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 0, 0, 0, 540, 540, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 540, 540, 540, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 427, 428, 429, 0, 0, 0, 0, 0, 979, 0, 0, 0, 979, 0, 0, 0},
            {0, 0, 0, 634, 634, 634, 634, 0, 0, 0, 634, 634, 634, 634, 459, 460, 461, 634, 634, 634, 634, 634, 634, 0, 0, 0, 634, 634, 634, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 491, 492, 493, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 491, 492, 493, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {676, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677}
        };

        int[,] objectMap =
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 0, 0, 0, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        enum MarioObjectType
        {
            COIN = 2,
            GOOMBA = 4
        };

        class MarioObject
        {
            public float xPos;
            public float yPos;
            public int tileXPos;
            public int tileYPos;
            public MarioObjectType id;

            public Direction dir;
        };

        List<MarioObject> marioObjects = new List<MarioObject>();				  
					  
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

            marioTexture    = Content.Load<Texture2D>("mario");
            tilesetTexture  = Content.Load<Texture2D>("marioTileset");

            backgroundTexture = Content.Load<Texture2D>("background");

            for (int y = 0; y < objectMap.GetLength(0); y++)
            {
                for (int x = 0; x < objectMap.GetLength(1); x++)
                {
                    if (objectMap[y, x] == 0)
                        continue;

                    MarioObject obj = new MarioObject();
                    obj.xPos = x * marioWidth;
                    obj.yPos = y * marioHeight;

                    int numXTiles = tilesetTexture.Width / marioWidth;

                    obj.tileXPos = ((objectMap[y, x] % numXTiles) - 1) * marioWidth;
                    obj.tileYPos = ((objectMap[y, x] / numXTiles)) * marioHeight;

                    obj.id = (MarioObjectType)objectMap[y, x];

                    obj.dir = Direction.LEFT;

                    marioObjects.Add(obj);
                }
            }
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

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                MoveMario(Direction.LEFT, marioMoveSpeed);
                marioFlip = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                MoveMario(Direction.RIGHT, marioMoveSpeed);
                marioFlip = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                MoveMario(Direction.UP, marioJumpForce);
                marioJumpForce -= 1;
            }

            // apply gravity
            if (MoveMario(Direction.DOWN, gravity) == true)
            {
                marioJumpForce = marioMaxJumpForce;
            }

            camera.Translation =
                new Vector3(-marioXpos + (windowWidth) / 2,
                -marioYpos + (windowHeight - (marioHeight * 2)), 0);

            // End Mario Update
            //--------------------------------


            UpdateMarioObjects(gameTime);

            base.Update(gameTime);
        }

        void UpdateMarioObjects(GameTime gameTime)
        {
            for (int i = 0; i < marioObjects.Count; i++)
            {
                if (marioObjects[i].id == MarioObjectType.COIN)
                {
                    UpdateCoin(marioObjects[i], gameTime);
                }

                if (marioObjects[i].id == MarioObjectType.GOOMBA)
                {
                    UpdateGoomba(marioObjects[i], gameTime);
                }
            }
        }

        bool objectCollidesWithMario(MarioObject obj)
        {
            return (marioXpos < obj.xPos + marioWidth && marioXpos + marioWidth > obj.xPos &&
                    marioYpos < obj.yPos + marioHeight && marioYpos + marioHeight > obj.yPos);
        }

        void UpdateCoin(MarioObject obj, GameTime gameTime)
        {
            obj.yPos += (float)
            (Math.Cos(gameTime.TotalGameTime.TotalSeconds * 8.0) * 0.2f);

            if (objectCollidesWithMario(obj))
            {
                marioObjects.Remove(obj);
            }

        }

        void UpdateGoomba(MarioObject obj, GameTime gameTime)
        {

        }


        bool MoveMario(Direction dir, int amount)
        {
            Point p = new Point(marioXpos, marioYpos);


            if (dir == Direction.LEFT || dir == Direction.RIGHT)
            {
                p.Y += marioHeight / 2;

                if (dir == Direction.RIGHT) p.X = marioXpos + marioWidth;
            }
            else
            {
                p.X += marioWidth / 2;
                if (dir == Direction.DOWN) p.Y = marioYpos + marioHeight;
            }

            p = MovePointInDir(dir, amount, p);
            int x = p.X / marioWidth;
            int y = p.Y / marioHeight;

            if (p.X < 0) x = -1;
            if (p.Y < 0) y = -1;

            Point marioPos = new Point(marioXpos, marioYpos);
            marioPos = MovePointInDir(dir, amount, marioPos);

            bool collided = false;


            if (!(y >= map.GetLength(0) || y < 0 || x >= map.GetLength(1) || x < 0))
            {
                if (map[y, x] != 0)
                {
                    // snap into place
                    if (dir == Direction.UP) marioPos.Y = (y + 1) * marioHeight;
                    if (dir == Direction.DOWN) marioPos.Y = (y - 1) * marioHeight;
                    if (dir == Direction.LEFT) marioPos.X = (x + 1) * marioWidth;
                    if (dir == Direction.RIGHT) marioPos.X = (x - 1) * marioWidth;

                    collided = true;
                }
            }

            marioXpos = marioPos.X;
            marioYpos = marioPos.Y;

            return collided;
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, camera);

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            DrawTileMap(tilesetTexture, map, marioWidth, marioHeight);

            // Draw The Level
            //---------------
            DrawTileMap(tilesetTexture, map, marioWidth, marioHeight);

            // draw game objects
            //------------------
            for (int i = 0; i < marioObjects.Count; i++)
            {
                Rectangle dstRect = new Rectangle((int)marioObjects[i].xPos, (int)marioObjects[i].yPos, marioWidth, marioHeight);
                Rectangle srcRect = new Rectangle(marioObjects[i].tileXPos, marioObjects[i].tileYPos, marioWidth, marioHeight);

                spriteBatch.Draw(tilesetTexture, dstRect, srcRect, Color.White);
            }

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

        public void DrawTileMap(Texture2D tileset, int[,] mapData, int TileWidth, int TileHeight)
        {
            int yLen = mapData.GetLength(0);
            int xLen = mapData.GetLength(1);
            int numXTiles = tileset.Width / TileWidth;
            for (int y = 0; y < yLen; y++)
            {
                for (int x = 0; x < xLen; x++)
                {
                    if (mapData[y, x] == 0)
                        continue;

                    int tileIndex = mapData[y, x];
                    int tileXPos = ((tileIndex % numXTiles) - 1) * TileWidth;
                    int tileYPos = ((tileIndex / numXTiles)) * TileHeight;

                    Rectangle dstRect =
                         new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);

                    Rectangle srcRect =
                         new Rectangle(tileXPos, tileYPos, TileWidth, TileHeight);

                    spriteBatch.Draw(tileset, dstRect, srcRect, Color.White);
                }
            }
        }
    }
}
