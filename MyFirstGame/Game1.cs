using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.Core.Game.UI;
using MyFirstGame.Core.Game.World;
using System.IO;

namespace MyFirstGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private GeneralUI UI;
        private Vector2 levelSize;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public LevelA LevelA
        {
            get { return levelA; }
        }
        LevelA levelA;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic 
            _graphics.SynchronizeWithVerticalRetrace = true;

            levelA = new LevelA(Services);
            levelA.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            levelA.LoadTileMap(Path.GetFullPath("C:\\Users\\fit43\\source\\repos\\MyFirstGame\\MyFirstGame\\Core\\Game\\Levels\\TileMap2.json"));
            levelA.LoadTextureMap();
            levelA.LoadEnemyTexture();

            _graphics.PreferredBackBufferWidth = levelA.Width * 32;
            _graphics.PreferredBackBufferHeight = levelA.Height * 32;
            _graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            levelA.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            levelA.Draw(_spriteBatch);


            base.Draw(gameTime);
        }
    }
}
