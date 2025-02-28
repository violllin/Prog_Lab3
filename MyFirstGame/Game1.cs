using System;
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
        private static readonly Random random = new Random();
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
        private string LoadRandomMap(string directoryPath, string baseName, string extension)
        {
            string[] files = Directory.GetFiles(directoryPath, $"{baseName}*.{extension}");
     
            if (files.Length == 0)
                throw new FileNotFoundException($"В папке '{directoryPath}' нет карт формата {extension}!");

            string randomFile = files[random.Next(files.Length)];

            return Path.GetFileName(randomFile);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var relativePath = Path.Combine("Core", "Game", "Levels");
            var fullPath = Path.Combine(baseDir, relativePath, LoadRandomMap(relativePath,"TileMap", "json"));
            levelA.LoadTileMap(Path.GetFullPath(fullPath));

            levelA.LoadTileMap(Path.GetFullPath(fullPath));
            levelA.LoadTextureMap();
            levelA.LoadEnemyTexture();
            levelA.LoadPlayer();

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
