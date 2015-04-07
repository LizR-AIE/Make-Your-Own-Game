using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameWorkshop
{
    class Goomba
    {
        enum Dir
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        enum Square
        {
            EMPTY,
            BRICK,
            GOOMBA,
            MYSTERY,
            START
        };

        public int goombaXpos;
        public int goombaYpos;
        public bool goombaDead;

        public bool goombaMovingLeft = false;

        public bool update(int[,] map, ref int marioXpos, ref int marioYpos)
        {
            bool marioDied = false;
            if (CheckCollision(marioXpos, marioYpos, goombaXpos, goombaYpos))
            {
                if (goombaYpos + 32 < marioYpos - 16 && goombaDead == false)
                {
                    for (int y = 0; y < map.GetLength(0); y++)
                    {
                        for (int x = 0; x < map.GetLength(1); x++)
                        {
                            if (map[y, x] == 4)
                            {
                                marioXpos = 64 * x;
                                marioYpos = 64 * y;
                                marioDied = true;
                            }
                        }
                    }
                }
                else
                {
                    goombaDead = true;
                }
            }

            if (goombaMovingLeft == true)
            {
                if (CanGoombaMove(map, Dir.LEFT, 3) && goombaDead == false)
                {
                    goombaXpos = goombaXpos - 3;
                }
                else
                {
                    goombaMovingLeft = false;
                }
            }
            else
            {
                if (CanGoombaMove(map, Dir.RIGHT, 3))
                {
                    goombaXpos = goombaXpos + 3;
                }
                else
                {
                    goombaMovingLeft = true;
                }
            }

            if (CanGoombaMove(map, Dir.DOWN, 4) || goombaDead == true)
            {
                goombaYpos = goombaYpos + 4;
            }

            return marioDied;
          
        }

        public void draw(SpriteBatch spriteBatch, Texture2D goomba)
        {
            spriteBatch.Draw(
               goomba,
               new Rectangle(goombaXpos, goombaYpos, 64, 64),
               null,
               Color.White, 0.0f, Vector2.Zero,
               goombaDead ? SpriteEffects.FlipVertically : SpriteEffects.None,
               0.0f);
        }

        bool CheckCollision(int marioXpos, int marioYpos, int otherXpos, int otherYpos)
        {
            if (otherYpos + 64 < marioYpos - 64 ||
            marioYpos < otherYpos)
                return false;

            if (otherXpos + 64 < marioXpos ||
            marioXpos + 64 < otherXpos)
                return false;


            return true;
        }

        bool CanGoombaMove(int[,] map, Dir eDirection, float amount)
        {
            Vector2 point;
            if (eDirection == Dir.LEFT || eDirection == Dir.RIGHT)
            {
                point.Y = goombaYpos + 64 / 2;
                if (eDirection == Dir.LEFT)
                    point.X = goombaXpos;
                else
                    point.X = goombaXpos + 64;
            }
            else
            {
                point.X = goombaXpos + 64 / 2;
                if (eDirection == Dir.UP)
                    point.Y = goombaYpos;
                else
                    point.Y = goombaYpos + 64;
            }

            switch (eDirection)
            {
                case Dir.UP:
                    point.Y -= amount;
                    break;
                case Dir.DOWN:
                    point.Y += amount;
                    break;
                case Dir.LEFT:
                    point.X -= amount;
                    break;
                case Dir.RIGHT:
                    point.X += amount;
                    break;
            }

            //Find the square that point is in
            int x = (int)(point.X / (float)64);
            int y = (int)(point.Y / (float)64);

            //return true if this is not inside a wall
            if (y >= map.GetLength(0) || y < 0 || x >= map.GetLength(1) || x < 0) return true;

            return map[y, x] == (int)Square.EMPTY || map[y, x] == (int)Square.START || map[y, x] == (int)Square.GOOMBA;

        }

    }
    class GoombaCreator
    {
        public void AddGoomba(int x, int y)
        {
            Goomba g = new Goomba();
            g.goombaXpos = x;
            g.goombaYpos = y;
            g.goombaDead = false;

            goombas.Add(g);

        }

        public bool Update(int [,] map, ref int marioXpos, ref int marioYpos)
        {
            bool marioDied = false;
            foreach (Goomba g in goombas)
            {
                if (g.update(map, ref marioXpos, ref marioYpos))
                {
                    marioDied = true;
                }
            }

            return marioDied;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D goomba)
        {
            foreach (Goomba g in goombas)
            {
                g.draw(spriteBatch, goomba);
            }
        }

        List<Goomba> goombas = new List<Goomba>();

    }
}
