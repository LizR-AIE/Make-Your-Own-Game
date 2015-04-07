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
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {

        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        public Point MovePointInDir(Direction dir, int amount, Point p)
        {
            if (dir == Direction.UP)   p.Y -= amount;
            if (dir == Direction.DOWN) p.Y += amount;
            if (dir == Direction.LEFT) p.X -= amount;
            if (dir == Direction.RIGHT)p.X += amount;

            return p;
        }

        /// <summary>
        /// Draws The Tiles from the tileset texture
        /// </summary>
        /// <param name="tileset">Texture containing tiles</param>
        /// <param name="mapData">Data Exported from editor</param>
        /// <param name="TileWidth">Whith of each individual tile</param>
        /// <param name="TileHeight">Height of each individual tile</param>
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

                    Rectangle dstRect = new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
                    Rectangle srcRect = new Rectangle(tileXPos, tileYPos, TileWidth, TileHeight);

                    spriteBatch.Draw(tileset, dstRect, srcRect, Color.White);
                }
            }
        }
    }
}
