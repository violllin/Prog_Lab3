using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using MyFirstGame.resPlayer;

namespace MyFirstGame.World
{
    public class Level
    {
        private int[,] tileMap;
        private Dictionary<int, Texture2D> texturesGW;
        private Texture2D wallTexture;
        Vector2 position;
        public Level(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider);
        }
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;
        public Player player
        {
            get { return _player; }
        }
        Player _player;
        public Vector2 GetLevelSize(int textureX, int textureY)
        {
            Initialize();
            int height = tileMap.GetLength(0);
            int width = tileMap.GetLength(1);
            return new Vector2(height * textureX, width * textureY);
        }
        public static Vector2 FindPosition<T>(T[,] array, T index)
        {
            for (int y = 0; y < array.GetLength(0); y++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    if (EqualityComparer<T>.Default.Equals(array[y, x], index))
                    {
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }
        public void Initialize()
        {
            tileMap = new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                {0, 2, 3, 2, 2, 0, 0, 0, 0, 0, 3, 2, 2, 2, 0, 0},
                {0, 2, 2, 2, 2, 4, 2, 2, 2, 0, 0, 0, 2, 2, 0, 0},
                {0, 2, 2, 2, 2, 0, 0, 2, 2, 2, 3, 0, 2, 2, 0, 0},
                {0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0},
                {0, 0, 2, 2, 0, 0, 2, 2, 2, 0, 2, 2, 0, 2, 2, 0},
                {0, 0, 2, 2, 4, 2, 2, 3, 2, 2, 2, 4, 0, 2, 2, 0},
                {0, 2, 2, 2, 0, 0, 2, 2, 2, 0, 2, 2, 0, 0, 4, 0},
                {0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 2, 2, 2, 0},
                {0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 2, 2, 0},
                {0, 2, 2, 0, 0, 0, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0},
                {0, 3, 2, 2, 4, 2, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0},
                {0, 0, 2, 2, 2, 2, 2, 0, 2, 0, 2, 0, 0, 2, 2, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 2, 4, 2, 0, 0, 2, 1, 0},
                {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
            
        }
        public void LoadContent()
        {
            this.Content.RootDirectory = "Content";
            texturesGW = new Dictionary<int, Texture2D>
            {
                { 0, Content.Load<Texture2D>("resGW/wallV") },
                { 1, Content.Load<Texture2D>("resGW/playerV1") },
                { 2, Content.Load<Texture2D>("resGW/floorV")},
                { 3, Content.Load<Texture2D>("resGW/chestV1") },
                { 4, Content.Load<Texture2D>("resGW/enemyV1")}
            };
            _player = new Player(this, FindPosition(tileMap, 1));
        }
        public void UploadContent()
        {
            texturesGW.Clear();
        }
        public void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
        }
        public void Dispose()
        {
            Content.Dispose();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            if (tileMap.GetLength(0) > 0 && tileMap.GetLength(1) > 00)
            {
                for (int y = 0; y < tileMap.GetLength(0); y++)
                {
                    for (int x = 0; x < tileMap.GetLength(1); x++)
                    {
                        int tileKey = tileMap[y, x];
                        if (texturesGW.TryGetValue(tileKey, out Texture2D tileTexture))
                        {
                            position = new Vector2(x * tileTexture.Width, y * tileTexture.Height);
                            spriteBatch.Draw(tileTexture, position, Color.White);
                        }
                        if (texturesGW.TryGetValue(2, out Texture2D floorTexture))
                        {
                            position = new Vector2(x * tileTexture.Width, y * tileTexture.Height);
                            spriteBatch.Draw(floorTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.8f);
                        }
                        else
                        {
                            throw new ArgumentException("No key found in texturesGW");
                        }

                    }
                }
            }
            else
            {
                throw new ArgumentException("tileMap don't have corrent size");
            }
            _player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
