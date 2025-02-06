using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.World;

namespace MyFirstGame.UI
{
    public class GeneralUI : IGame
    {
        public Game _game;

        Texture2D mainMenuButtonTexture;
        Texture2D endButtonTexture;
        Texture2D startButtonTexture;
        Texture2D mainMenuTexture;

        Vector2 mainMenuButtonPosition;
        Vector2 endButtonPosition;
        Vector2 startButtonPosition;

        Rectangle mainMenuButtonRect;
        Rectangle endMenuButtonRect;
        Rectangle startMenuButtonRect;
        Rectangle mainMenuRect;

        private bool IsButtonPressed = false;
        private bool IsWindowOpen = false;
        private bool IsStartButtonPressed = false;
        private bool IsEndButtonPressed = false;
        private bool IsMainMenuButtonPressed = false;

        private GraphicsDeviceManager _graphicUI;
        private SpriteBatch _spriteBatchUI;
        private GlobalWorld globalWorld;

        public GeneralUI(Game game)
        {
            _game = game;
        }
        public void Initialize()
        {
            globalWorld = new GlobalWorld(_game);
        }
        public void LoadContent()
        {
            _spriteBatchUI = new SpriteBatch(_game.GraphicsDevice);

            mainMenuButtonTexture = _game.Content.Load<Texture2D>("resUI/mainMenuButton");
            endButtonTexture = _game.Content.Load<Texture2D>("resUI/endButton");
            startButtonTexture = _game.Content.Load<Texture2D>("resUI/startButton");
            mainMenuTexture = _game.Content.Load<Texture2D>("resUI/mainMenu");

            mainMenuButtonRect = new Rectangle(200, 50, mainMenuButtonTexture.Width, mainMenuButtonTexture.Height);
            endMenuButtonRect = new Rectangle(200, 100, endButtonTexture.Width, endButtonTexture.Height);
            startMenuButtonRect = new Rectangle(200, 200, startButtonTexture.Width, startButtonTexture.Height);
            mainMenuRect = new Rectangle(300, 300, mainMenuTexture.Width, mainMenuTexture.Height);



        }
        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            { _game.Exit(); }

            var mState = Mouse.GetState();
            var mousePosition = mState.Position;

            if (mainMenuButtonRect.Contains(mousePosition))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    IsMainMenuButtonPressed = true;
                }
                else if (mState.LeftButton == ButtonState.Released && IsButtonPressed)
                {
                    IsMainMenuButtonPressed = false;
                }
            }
            if (endMenuButtonRect.Contains(mousePosition))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    IsEndButtonPressed = true;
                }
                else if (mState.LeftButton == ButtonState.Released && IsButtonPressed)
                {
                    IsEndButtonPressed = false;
                }
            }
            if (startMenuButtonRect.Contains(mousePosition))
            {
                if (mState.LeftButton == ButtonState.Pressed)
                {
                    IsStartButtonPressed = true;
                }
                else if (mState.LeftButton == ButtonState.Released && IsButtonPressed)
                {
                    IsStartButtonPressed = false;
                }
            }
        }
        public void Draw(GameTime gameTime)
        {
            _game.GraphicsDevice.Clear(Color.White);

            _spriteBatchUI.Begin();
            _spriteBatchUI.Draw(mainMenuTexture, mainMenuButtonRect, Color.White);
            _spriteBatchUI.Draw(startButtonTexture, startMenuButtonRect, Color.White);
            _spriteBatchUI.Draw(endButtonTexture, endMenuButtonRect, Color.White);
            if (IsStartButtonPressed)
            {
                globalWorld._game.Run();
            }
            _spriteBatchUI.End();
        }
    }
}
