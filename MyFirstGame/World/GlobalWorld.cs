using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MyFirstGame.World
{
    public class GlobalWorld : IGame
    {
        public Game _game;
        private int[,] tileMap;
        private Dictionary<int, Texture2D> texturesGW;

        private SpriteBatch _spriteBatchGW;
        public GlobalWorld(Game game)
        {
            _game = game;
        }
        public void Initialize()
        {
            
            
            tileMap = new int[,]
            {
                
{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
{0, 2, 1, 2, 2, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 0},
{0, 2, 2, 2, 2, 0, 0, 2, 2, 4, 2, 0, 0, 0, 2, 0},
{0, 2, 2, 2, 2, 0, 0, 3, 2, 0, 0, 0, 0, 0, 2, 0},
{0, 2, 2, 2, 2, 2, 0, 0, 0, 0, 2, 2, 0, 0, 2, 0},
{0, 2, 2, 0, 2, 2, 2, 4, 2, 2, 2, 3, 0, 2, 2, 0},
{0, 2, 2, 0, 0, 0, 0, 0, 0, 2, 2, 2, 0, 2, 2, 0},
{0, 2, 2, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0},
{0, 2, 2, 2, 2, 2, 3, 0, 0, 2, 2, 2, 2, 2, 0, 0},
{0, 0, 2, 2, 2, 2, 2, 0, 0, 2, 2, 3, 2, 2, 0, 0},
{0, 0, 4, 2, 0, 2, 2, 0, 0, 2, 2, 0, 0, 0, 0, 0},
{0, 0, 2, 2, 0, 0, 2, 0, 0, 2, 2, 0, 0, 0, 0, 0},
{0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 2, 2, 2, 2, 2, 0},
{0, 0, 0, 0, 0, 0, 2, 0, 0, 2, 2, 0, 0, 0, 2, 0},
{0, 0, 0, 0, 0, 0, 2, 4, 2, 2, 2, 0, 0, 0, 3, 0},
{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };
        }
        public void LoadContent()
        {
            _spriteBatchGW = new SpriteBatch(_game.GraphicsDevice);
            texturesGW = new Dictionary<int, Texture2D>
            {
                { 0, _game.Content.Load<Texture2D>("resGW/wall") },
                { 1, _game.Content.Load<Texture2D>("resGW/player") },
                { 2, _game.Content.Load<Texture2D>("resGW/floor")},
                { 3, _game.Content.Load<Texture2D>("resGW/chest") },
                { 4, _game.Content.Load<Texture2D>("resGW/enemy")}
            };
        }
        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            { _game.Exit(); }
        }
        public void Draw(GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.White);
            
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
        }
    }
}
