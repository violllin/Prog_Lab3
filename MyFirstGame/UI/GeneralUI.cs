using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.World;

namespace MyFirstGame.UI
{
    public class GeneralUI : Game
    {
        Texture2D mainMenuButtonTexture;
        Texture2D endButtonTexture;
        Texture2D startButtonTexture;

        Vector2 mainMenuButtonPosition;
        Vector2 endButtonPosition;
        Vector2 startButtonPosition;

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            mainMenuButtonTexture = Content.Load<Texture2D>("resUI/mainMenuButton");
            endButtonTexture = Content.Load<Texture2D>("resUI/endButton");
            startButtonTexture = Content.Load<Texture2D>("resUI/startButton");
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

            base.Draw(gameTime);
        }
    }
}
