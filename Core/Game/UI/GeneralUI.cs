using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core.Game.UI
{
    public class GeneralUI
    {
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

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;
       
        public GeneralUI(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider);
        }
        public void Initialize()
        {
        }
        public void LoadContent()
        {
            mainMenuButtonTexture = Content.Load<Texture2D>("Content/resUI/mainMenuButton");
            endButtonTexture = Content.Load<Texture2D>("Content/resUI/endButton");
            startButtonTexture = Content.Load<Texture2D>("Content/resUI/startButton");
            mainMenuTexture = Content.Load<Texture2D>("Content/resUI/mainMenu");

            mainMenuButtonRect = new Rectangle(200, 50, mainMenuButtonTexture.Width, mainMenuButtonTexture.Height);
            endMenuButtonRect = new Rectangle(200, 100, endButtonTexture.Width, endButtonTexture.Height);
            startMenuButtonRect = new Rectangle(200, 200, startButtonTexture.Width, startButtonTexture.Height);
            mainMenuRect = new Rectangle(300, 300, mainMenuTexture.Width, mainMenuTexture.Height);

        }
        public void Update(GameTime gameTime)
        {
            var mState = Mouse.GetState();
            var mousePosition = mState.Position;

            if (startMenuButtonRect.Contains(mousePosition) &&
                mState.LeftButton == ButtonState.Pressed &&
                mState.LeftButton == ButtonState.Released)
            {
                IsStartButtonPressed = true;
            }
            else
            {
                IsStartButtonPressed = false;
            }
            if (endMenuButtonRect.Contains(mousePosition) &&
                mState.LeftButton == ButtonState.Pressed &&
                mState.LeftButton == ButtonState.Released)
            {
                IsEndButtonPressed = true;

            }
            else
            {
                IsEndButtonPressed = false;
            }
            if (IsStartButtonPressed)
            {

            }
            //if()
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();
            spriteBatch.Draw(mainMenuTexture, mainMenuButtonRect, Color.White);
            spriteBatch.Draw(startButtonTexture, startMenuButtonRect, Color.White);
            spriteBatch.Draw(endButtonTexture, endMenuButtonRect, Color.White);
            //if (IsStartButtonPressed)
            //{
            //    globalWorld.Draw(gameTime);
            //}
            //if (IsEndButtonPressed)
            //{
            //    _game.Exit();
            //}

            spriteBatch.End();
        }
    }
}
