using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MyFirstGame.Core.Game.resPlayer;
using MyFirstGame.Core.Game.resTile;
using MyFirstGame.HelpDirectory;
using System;
using System.Collections.Generic;

namespace MyFirstGame.Core.Game.World
{
    public class LevelA
    {
        private Dictionary<int, Texture2D> texturesGW;

        Vector2 playerPosition;
        Vector2 playerStartPosition;
        Vector2 position;

        Texture2D floorTexture;
        public LevelA(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
        }

        ContentManager content;
        public ContentManager Content
        {
            get { return content; }
        }

        Player player;
        public Player Player
        {
            get { return player; }
        }

        UploadTileMapFromJson uploader;
        public UploadTileMapFromJson Uploader
        {
            get { return uploader; }
        }

        TileMap tileMap;
        public TileMap TileMap
        {
            get { return tileMap; }
        }
        public int Width
        {
            get { return tileMap.Width; }
        }
        public int Height
        {
            get { return tileMap.Height; }
        }
        private Vector2 FindPlayerStartPosition(int playerIndex)
        {
            for (int y = 0; y < Height; y++)
            {
                var row = tileMap.Tiles[y];
                if (row == null || row.Count != Width)
                {
                    throw new ArgumentException($"Row {y} is invalid or does not match the expected width.");
                }

                for (int x = 0; x < Width; x++)
                {
                    int tileValue = row[x];
                    if (tileValue == playerIndex)
                    {
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }
        public Vector2 GetLevelSize(int textureX, int textureY)
        {
            return new Vector2(tileMap.Width * textureX, tileMap.Height * textureX);
        }
        
        public void Initialize()
        {
            uploader = new UploadTileMapFromJson();
            tileMap = new TileMap();
        }

        /// <summary>
        /// The function parameter must 
        /// specify the full path to the tile map.
        /// </summary>
        /// <param name="filePath"></param>
        public void LoadTileMap(string filePath)
        {
            tileMap = Uploader.LoadFromFile(filePath);
            player = new Player(this, FindPlayerStartPosition(1));
        }

        /// <summary>
        /// In this dictionary -> key = texture assignment on the tile map 
        /// </summary>
        public void LoadTextureMap()
        {
            texturesGW = new Dictionary<int, Texture2D>()
            {
                { 0, Content.Load<Texture2D>("resGW/wallV")},
                { 2, Content.Load<Texture2D>("resGW/floorV")},
                { 3, Content.Load<Texture2D>("resGW/chestV1")}
            };
            floorTexture = texturesGW[2];
        }
        public void LoadEnemyTexture()
        {
            texturesGW.Add(4, Content.Load<Texture2D>("resGW/enemyV1"));
        }
        public void LoadPlayer()
        {
            Player.LoadContent();
            Player.StartPosition = FindPlayerStartPosition(1);
        }
        public void Log()
        {
            foreach (var pair in texturesGW)
            {
                Console.WriteLine($"Key: {pair.Key}, Texture: {pair.Value.Name}");
            }
        }
        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            playerStartPosition = FindPlayerStartPosition(1);

            if (tileMap.Width > 0 && tileMap.Height > 0)
            {
                for (int y = 0; y < tileMap.Height; y++)
                {
                    for (int x = 0; x < tileMap.Width; x++)
                    {
                        position = new Vector2(x * floorTexture.Width, y * floorTexture.Height);

                        // 1. Сначала рисуем пол (глубина 0.9 - он ниже всех)
                        spriteBatch.Draw(floorTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.9f);

                        // 2. Затем проверяем и рисуем тайлы
                        int tileKey = tileMap.Tiles[y][x];
                        if (texturesGW.TryGetValue(tileKey, out Texture2D tileTexture))
                        {
                            spriteBatch.Draw(tileTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("tileMap doesn't have a correct size");
            }
            player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}