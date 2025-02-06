using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.World;
using System;
using System.Collections.Generic;

namespace MyFirstGame
{
    public class Game1 : Game
    {
        Vector2 centerPositionOnScreen;
        Vector2 ballPosition;
        Vector2 mousePosition;
        Vector2 mapPosition;
        Vector2 centerPositionOnMap;

        Texture2D ballTexture;
        Texture2D firstWorldMapTexture;

        private GlobalWorld world;

        float ballSpeed;
        Dictionary<string, Texture2D> dict = new Dictionary<string, Texture2D>();
        private int[,] tile_map;
        private Dictionary<int, Texture2D> texturesGW;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.ApplyChanges();

            centerPositionOnScreen = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                                 _graphics.PreferredBackBufferHeight / 2);

            ballPosition = centerPositionOnScreen;
            mapPosition = centerPositionOnScreen;
            ballSpeed = 100f;
            
            world = new GlobalWorld();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
            firstWorldMapTexture = Content.Load<Texture2D>("FirstMapC");

            centerPositionOnMap = new Vector2(firstWorldMapTexture.Width / 2, firstWorldMapTexture.Height / 2);
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            float updateBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * 2;

            // Keyboard
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                //ballPosition.Y += updateBallSpeed;
            }
            if (kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W))
            {
                //ballPosition.Y -= updateBallSpeed;
            }
            if (kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A))
            {
                //ballPosition.X -= updateBallSpeed;
            }
            if (kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D))
            {
                //ballPosition.X += updateBallSpeed;
            }
            if (kstate.IsKeyDown(Keys.R))
            {
                //ballPosition = centerPositionOnScreen;
            }

            // Mouse
            var mstate = Mouse.GetState();
            if (mstate.LeftButton == ButtonState.Pressed)
            {
                //ballPosition.X = mstate.X - ballTexture.Width / 4;
                //ballPosition.Y = mstate.Y - ballTexture.Height / 4;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

           


            
            base.Draw(gameTime);
        }
    }
}
