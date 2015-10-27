using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TiledSharp_MonoGame_Example
{
    class TileMap
    {
        public TmxMap map;
        public Texture2D tileset;
        Game1 parent;
        public int tileWidth
        {
            get
            {
                return map.Tilesets[0].TileWidth;
            }
        }

        public int tileHeight
        {
            get
            {
                return map.Tilesets[0].TileHeight;
            }
        }

        public int tilesetTilesWide 
        {
            get
            {
                return tileset.Width / tileWidth;
            }
        }

        public int tilesetTilesHigh
        {
            get
            {
                return tileset.Height / tileHeight;
            }
        }

        public TileMap(String path,  Microsoft.Xna.Framework.Content.ContentManager content)
        {
            map = new TmxMap(path);
            tileset = content.Load<Texture2D>(map.Tilesets[0].Name.ToString());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (tileFrame + 1 > tilesetTilesWide) ? tileFrame - column * tilesetTilesWide : 0;

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }
        }

    }
}
