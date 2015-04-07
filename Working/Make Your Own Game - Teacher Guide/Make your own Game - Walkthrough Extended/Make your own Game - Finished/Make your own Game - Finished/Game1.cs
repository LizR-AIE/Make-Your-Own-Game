using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;

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
        SpriteFont hudFont;

        //--------------------------------------------
        // mario variables
        int                     marioXpos;
        int                     marioYpos;
        int                     marioWidth          = 32;
        int                     marioHeight         = 32;
        int                     marioMoveSpeed      = 5;
        bool                    marioFlip           = true;

        int                     marioJumpForce      = 0;
        int                     marioMaxJumpForce   = 20;

        int                     gravityForce        = 5;

        int gameScore   = 0;
        int gameLives   = 5;
        int gameTime    = 0;

        Matrix camera = Matrix.Identity;

        enum BlockType
        {
            PASS_THROUGH = 1,
            SOLID,
            JUMP_THROUGH
        };

        enum SpecialTiles
        {
            START = 1,
        };

        Dictionary<int, BlockType> blockTable;

        int tileWidth = 32;
        int tileHeight = 32;


        int[,] map =
        {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 941, 942, 943, 0, 0, 0, 540, 540, 540, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 634, 634, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 941, 942, 943},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 941, 942, 943, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 634, 634, 634, 0, 0, 0, 0, 0, 0, 634, 634, 0, 0, 0, 0, 0, 0, 634, 634, 634, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 609, 610, 610, 610, 610, 610, 611, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 634, 634, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 941, 942, 943, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 915, 0, 818, 0, 915, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 634, 634, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 850, 0, 947, 0, 0, 0, 0, 0, 0, 676, 677, 677, 677, 677, 677, 678, 0, 0, 0, 634, 634, 0, 0, 0, 676, 677, 677, 677, 677, 677, 678, 0, 0, 0, 0, 0},
        {0, 941, 942, 943, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 882, 0, 947, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 634, 634, 634, 634, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 0, 0, 947, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 689, 690},
        {0, 0, 0, 0, 540, 540, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 540, 540, 540, 0, 947, 0, 0, 0, 947, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 676, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 677, 0, 0, 0, 0, 0, 689, 690, 721, 722},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 689, 690, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 947, 0, 540, 0, 947, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 721, 722, 721, 722},
        {0, 0, 0, 0, 0, 0, 0, 689, 690, 721, 722, 0, 0, 0, 427, 428, 429, 0, 0, 0, 0, 0, 979, 0, 0, 0, 979, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 689, 690, 721, 827, 828, 828},
        {0, 0, 0, 634, 634, 634, 634, 721, 722, 721, 634, 634, 634, 634, 459, 460, 461, 634, 634, 634, 634, 634, 634, 690, 689, 690, 634, 634, 634, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 721, 722, 721, 859, 860, 860},
        {0, 0, 0, 0, 0, 689, 690, 721, 722, 721, 722, 0, 0, 0, 491, 492, 493, 0, 0, 0, 689, 690, 721, 722, 721, 722, 721, 722, 689, 690, 0, 411, 412, 412, 413, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 411, 412, 412, 412, 412, 412, 413},
        {0, 0, 0, 0, 0, 721, 722, 721, 722, 721, 722, 631, 632, 633, 491, 492, 493, 663, 664, 665, 721, 722, 721, 722, 721, 722, 721, 722, 721, 722, 0, 443, 444, 444, 445, 685, 686, 687, 688, 685, 686, 687, 688, 685, 686, 687, 688, 685, 686, 687, 688, 685, 686, 443, 444, 444, 444, 444, 444, 445},
        {411, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 412, 443, 444, 444, 445, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 656, 443, 444, 444, 444, 444, 444, 445}
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
            public int xPos;
            public int yPos;
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

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == (int)SpecialTiles.START)
                    {
                        marioXpos = x * tileWidth;
                        marioYpos = y * tileHeight;
                        map[y, x] = 0;
                    }
                }
            }

            blockTable = new Dictionary<int, BlockType>();

            blockTable[0]   = BlockType.PASS_THROUGH;
            blockTable[689] = BlockType.PASS_THROUGH;
            blockTable[690] = BlockType.PASS_THROUGH;
            blockTable[721] = BlockType.PASS_THROUGH;
            blockTable[722] = BlockType.PASS_THROUGH;
            blockTable[663] = BlockType.PASS_THROUGH;
            blockTable[664] = BlockType.PASS_THROUGH;
            blockTable[665] = BlockType.PASS_THROUGH;
            blockTable[631] = BlockType.PASS_THROUGH;
            blockTable[632] = BlockType.PASS_THROUGH;
            blockTable[633] = BlockType.PASS_THROUGH;
            

            blockTable[941] = BlockType.JUMP_THROUGH;
            blockTable[942] = BlockType.JUMP_THROUGH;
            blockTable[943] = BlockType.JUMP_THROUGH;

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
            tilesetTexture = Content.Load<Texture2D>("marioTileset");
            hudFont = Content.Load<SpriteFont>("Impact");

            for (int y = 0; y < objectMap.GetLength(0); y++)
            {
                for (int x = 0; x < objectMap.GetLength(1); x++)
                {
                    if (objectMap[y, x] == 0)
                        continue;

                    MarioObject obj = new MarioObject();
                    obj.xPos = x * tileWidth;
                    obj.yPos = y * tileHeight;

                    int numXTiles = tilesetTexture.Width / tileWidth;

                    obj.tileXPos = ((objectMap[y, x] % numXTiles) - 1) * tileWidth;
                    obj.tileYPos = ((objectMap[y, x] / numXTiles)) * tileHeight;

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
                //marioXpos -= marioMoveSpeed;
                MoveMario(Direction.LEFT, marioMoveSpeed);
                marioFlip = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //marioXpos += marioMoveSpeed;
                MoveMario(Direction.RIGHT, marioMoveSpeed);
                marioFlip = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                //marioYpos -= marioJumpForce;
                MoveMario(Direction.UP,marioJumpForce);
                marioJumpForce -= 1;
                if (marioJumpForce < 0) marioJumpForce = 0;
            }

            // Apply Gravity
            //marioYpos += gravityForce;
            if (MoveMario(Direction.DOWN, gravityForce) == true)
            {
                marioJumpForce = marioMaxJumpForce;
            }

            // make camera follow mario
            camera.Translation = 
                new Vector3(-marioXpos + (GraphicsDevice.Viewport.Width) / 2,
                            -marioYpos + (GraphicsDevice.Viewport.Height - (marioHeight * 2)), 0);

            // End Mario Update
            //--------------------------------
         

            // update mario objects
            for (int i = 0; i < marioObjects.Count; i++)
            {
                if (marioObjects[i].id == MarioObjectType.COIN)
                {
                    UpdateCoin(marioObjects[i]);
                }

                if (marioObjects[i].id == MarioObjectType.GOOMBA)
                {
                    UpdateGoomba(marioObjects[i]);
                }
            }
            base.Update(gameTime);
        }

        void UpdateGoomba(MarioObject goombaObj)
        {
            int xid = (goombaObj.xPos + 16) / tileWidth;
            int yid = goombaObj.yPos / tileWidth;
            if (xid < 0 || yid < 0) return;

            bool switchDir = false;


            if(GetTileType( xid, yid + 1) == BlockType.PASS_THROUGH)
                switchDir = true;

            if (GetTileType(xid - 1, yid) == BlockType.SOLID)
                switchDir = true;

            if (GetTileType(xid + 1, yid) == BlockType.SOLID)
                switchDir = true;


            if (switchDir)
            {
                if (goombaObj.dir == Direction.LEFT) goombaObj.dir = Direction.RIGHT;
                else if (goombaObj.dir == Direction.RIGHT) goombaObj.dir = Direction.LEFT;
            }

            if (goombaObj.dir == Direction.LEFT)
                goombaObj.xPos -= 4;
            if (goombaObj.dir == Direction.RIGHT)
                goombaObj.xPos += 4;
        }

        void UpdateCoin(MarioObject coinObj)
        {
            if (objectCollidesWithMario(coinObj))
            {
                marioObjects.Remove(coinObj);
                gameScore += 10;
            }
        }

        bool objectCollidesWithMario(MarioObject obj)
        {
            return (marioXpos < obj.xPos + tileWidth && marioXpos + marioWidth > obj.xPos &&
                    marioYpos < obj.yPos + tileHeight && marioYpos + marioHeight > obj.yPos);
        }

        //Direction objectCollidesWithMario(MarioObject obj)
        //{
        //    if (marioYpos < obj.yPos + tileHeight)
        //        return Direction.DOWN;

        //    if (marioYpos + marioHeight > obj.yPos)
        //        return Direction.UP;
        //    if(marioXpos < 


        //    return Direction.NONE;
        //}

        BlockType GetTileType(int idx, int idy)
        {
            if (idx < 0 || idx >= map.GetLength(1) || idy < 0 || idy > map.GetLength(0))
                return BlockType.SOLID;

            BlockType type = BlockType.SOLID;
            if (blockTable.Keys.Contains(map[idy, idx]))
                type = blockTable[map[idy, idx]];

            return type;
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
            int x = p.X / tileWidth;
            int y = p.Y / tileHeight;

            if (p.X < 0) x = -1;
            if (p.Y < 0) y = -1;

            Point marioPos = new Point(marioXpos, marioYpos);
            marioPos = MovePointInDir(dir, amount, marioPos);

            bool collided = false;

            
            if (!(y >= map.GetLength(0) || y < 0 || x >= map.GetLength(1) || x < 0))
            {
                BlockType type = BlockType.SOLID;
                if(blockTable.Keys.Contains( map[y, x] ))
                    type = blockTable[map[y, x]];

                if (type == BlockType.SOLID)
                {
                    // snap into place
                    if (dir == Direction.UP)    marioPos.Y = (y + 1) * tileHeight;
                    if (dir == Direction.DOWN)  marioPos.Y = (y - 1) * tileHeight;
                    if (dir == Direction.LEFT)  marioPos.X = (x + 1) * tileHeight;
                    if (dir == Direction.RIGHT) marioPos.X = (x - 1) * tileHeight;

                    collided = true;
                }
                else if (type == BlockType.JUMP_THROUGH)
                {
                    // snap into place
                    //if (dir == Direction.UP) marioPos.Y = (y + 1) * tileHeight;
                    if (dir == Direction.DOWN) marioPos.Y = (y - 1) * tileHeight;
                    //if (dir == Direction.LEFT) marioPos.X = (x + 1) * tileHeight;
                    //if (dir == Direction.RIGHT) marioPos.X = (x - 1) * tileHeight;

                    if(dir != Direction.UP)
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

            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, camera);
            
            // Draw The Level
            //---------------
            DrawTileMap(tilesetTexture, map, tileWidth, tileHeight);



            // draw game objects
            //------------------
            for (int i = 0; i < marioObjects.Count; i++)
            {

                Rectangle dstRect = new Rectangle(marioObjects[i].xPos, marioObjects[i].yPos, tileWidth, tileHeight);
                Rectangle srcRect = new Rectangle(marioObjects[i].tileXPos, marioObjects[i].tileYPos, tileWidth, tileHeight);

                spriteBatch.Draw(tilesetTexture, dstRect, srcRect, Color.White);
            }

            // draw mario
            //---------------
            spriteBatch.Draw(
                marioTexture,
                new Rectangle(marioXpos, marioYpos, marioWidth, marioHeight),
                null,
                Color.White, 0.0f, Vector2.Zero,
                marioFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            //---------------
            spriteBatch.End();


            


            // draw the hud
            spriteBatch.Begin();

            spriteBatch.DrawString(hudFont, "SCORE: " + gameScore.ToString(), new Vector2(32, 16), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
