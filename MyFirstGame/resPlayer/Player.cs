using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.World;
using System;

namespace MyFirstGame.resPlayer
{
    public class Player
    {
        Texture2D playerTexture;

      

        private const float speed = 2.0f;

        public bool IsAlive
        {
            get { return isAlive; }

        }
        bool isAlive;
        public Texture2D PlayerTexture
        {
            get { return playerTexture; }
            set { playerTexture = value; }
        }
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Level level
        {
            get { return _level; }
        }
        Level _level;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;

        
        public Player(Level level, Vector2 position)
        {
            this._level = level;
            LoadContent();
            Reset(position);
        }
        // Сделано умножение на 32 для установки корректной начальной позиции в соотвествии 
        // с размером текстуры (32х32)
        public void Reset(Vector2 position)
        {
            Position = position * 32;
            isAlive = true;
        }
        void GetMove(float x, float y)
        {
            Position += new Vector2(x, y) * speed;
            Console.WriteLine($"{position.X} {position.Y}");
        }
        public void LoadContent()
        {
            playerTexture = level.Content.Load<Texture2D>("resGW/playerV1");
        }
        public void Update(GameTime gameTime)
        {
            float x = 0;
            float y = 0;

            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up)) { y -= 1; }
            if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down)) { y += 1; }
            if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left)) { x -= 1; }
            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right)) { x += 1; }
            GetMove(x,y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position,Color.White);
        }
    }
}
