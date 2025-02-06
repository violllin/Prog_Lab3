using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MyFirstGame.World
{
    public class GlobalWorld : Game
    {
        private int[,] tileMap;
        private Dictionary<int, Texture2D> texturesGW;

        private GraphicsDeviceManager _graphicGW;
        private SpriteBatch _spriteBatchGW;
        public GlobalWorld()
        {
            _graphicGW = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphicGW.PreferredBackBufferWidth = 1920;
            _graphicGW.PreferredBackBufferHeight = 1080;
            _graphicGW.ApplyChanges();
            tileMap = new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,2,2,2,0,2,2,2,2,0,2,0,2,2,2,0},
                {0,2,1,2,0,2,2,2,2,2,2,0,2,2,3,0},
                {0,2,2,2,0,0,0,0,0,0,2,0,2,2,0,0},
                {0,0,2,0,0,0,2,2,2,2,2,0,0,2,0,0},
                {0,2,2,2,2,0,2,2,2,2,2,0,2,2,2,0},
                {0,2,2,2,2,0,2,2,2,4,2,2,2,2,2,0},
                {0,2,2,2,2,0,2,4,2,2,2,2,2,2,2,0},
                {0,2,2,2,3,0,0,2,2,2,2,4,2,2,2,0},
                {0,2,2,2,2,0,0,0,0,0,0,0,2,2,2,0},
                {0,2,2,2,2,2,3,2,2,2,3,0,2,2,2,0},
                {0,2,2,2,2,2,2,2,2,2,2,0,2,2,2,0},
                {0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
                {0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
                {0,3,2,2,2,2,2,2,2,2,2,2,2,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
            };
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatchGW = new SpriteBatch(GraphicsDevice);
            texturesGW = new Dictionary<int, Texture2D>
            {
                { 0, Content.Load<Texture2D>("resGW/wall") },
                { 1, Content.Load<Texture2D>("resGW/player") },
                { 2, Content.Load<Texture2D>("resGW/floor")},
                { 3, Content.Load<Texture2D>("resGW/enemy") },
                { 4, Content.Load<Texture2D>("resGW/chest") }
            };
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            { Exit(); }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            _spriteBatchGW.Begin();
            if (tileMap.GetLength(0) > 0 && tileMap.GetLength(1) > 00)
            {
                for (int y = 0; y < tileMap.GetLength(0); y++)
                {
                    for (int x = 0; x < tileMap.GetLength(1); x++)
                    {
                        int tileKey = tileMap[y, x];
                        if (texturesGW.TryGetValue(tileKey, out Texture2D tileTexture))
                        {
                            Vector2 position = new Vector2(x * tileTexture.Width, y * tileTexture.Height);
                            _spriteBatchGW.Draw(tileTexture, position, Color.White);
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
            _spriteBatchGW.End();
            base.Draw(gameTime);
        }
    }
}
