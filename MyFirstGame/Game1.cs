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
        private Level world;
        private GeneralUI UI;
        private Vector2 levelSize;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic 
            _graphics.SynchronizeWithVerticalRetrace = true;
            
            
            world = new Level(Content.ServiceProvider);
            world.Initialize();

            UI = new GeneralUI(Content.ServiceProvider);

            _graphics.PreferredBackBufferWidth = (int)world.GetLevelSize(32, 32).X;
            _graphics.PreferredBackBufferHeight = (int)world.GetLevelSize(32, 32).Y;

            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            world.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            world.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // World "Run
            
           
            
            world.Draw(gameTime, _spriteBatch);
            //UI.Initialize();
            //UI.LoadContent();
            //UI.Update(gameTime);
            //UI.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}
