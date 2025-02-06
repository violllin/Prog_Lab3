using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.UI;
using MyFirstGame.World;
using System;
using System.Collections.Generic;

namespace MyFirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GlobalWorld world;
        private GeneralUI UI;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic 

            world = new GlobalWorld(this);
            UI = new GeneralUI(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            //float updateBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 2;

            // Keyboard
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
            }
            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
            }
            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
            }
            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
            }
            if (kstate.IsKeyDown(Keys.R))
            {
                
            }

            // Mouse
            var mstate = Mouse.GetState();
            if (mstate.LeftButton == ButtonState.Pressed)
            {
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // World "Run()"
            //world.Initialize();
            //world.LoadContent();
            //world.Update(gameTime);
            //world.Draw(gameTime);

            UI.Initialize();
            UI.LoadContent();
            UI.Update(gameTime);
            UI.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
