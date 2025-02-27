using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyFirstGame.Core.Game.World;

namespace MyFirstGame.Core.Game.resPlayer
{
    public class Player
    {
        Texture2D playerTexture;
        private Rectangle playerBounds;

        private const float speed = 2.0f;
        private const int Tile_Size = 32;
        private bool isMoving;

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

        public LevelA levelA
        {
            get { return _levelA; }
        }

        LevelA _levelA;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        Vector2 position;

        public Player(LevelA levelA, Vector2 position)
        {
            _levelA = levelA;
            LoadContent();
            Reset(position);
        }

        public void Reset(Vector2 position)
        {
            Position = position * Tile_Size;
            isAlive = true;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public void LoadContent()
        {
            playerTexture = levelA.Content.Load<Texture2D>("resGW/playerV1");
            UpdatePlayerBounds();
        }

        public void Update(GameTime gameTime)
        {
            float x = 0;
            float y = 0;

            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W) || kstate.IsKeyDown(Keys.Up))
            {
                y -= 1;
            }

            if (kstate.IsKeyDown(Keys.S) || kstate.IsKeyDown(Keys.Down))
            {
                y += 1;
            }

            if (kstate.IsKeyDown(Keys.A) || kstate.IsKeyDown(Keys.Left))
            {
                x -= 1;
            }

            if (kstate.IsKeyDown(Keys.D) || kstate.IsKeyDown(Keys.Right))
            {
                x += 1;
            }

            GetMove(x, y);
        }

        private void UpdatePlayerBounds()
        {
            if (playerTexture != null)
            {
                playerBounds = new Rectangle((int)position.X, (int)position.Y,
                    playerTexture.Width, playerTexture.Height);
            }
        }

        private bool IsTileObstacle(int tileKey)
        {
            Console.WriteLine($"TileKey: {tileKey}");
            return tileKey switch
            {
                1 or 2 => false,
                _ => true
            };
        }

        private bool CheckCollision(Rectangle playerRect)
        {
            int tileSize = 32;
            
            int startX = Math.Max(0, playerRect.Left / tileSize);
            int startY = Math.Max(0, playerRect.Top / tileSize);
            int endX = Math.Min(levelA.Width - 1, playerRect.Right / tileSize);
            int endY = Math.Min(levelA.Height - 1, playerRect.Bottom / tileSize);

            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    int tileKey = levelA.TileMap.Tiles[y][x];

                    if (!IsTileWalkable(tileKey))
                    {
                        Rectangle tileRect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);

                        if (playerRect.Intersects(tileRect))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool IsTileWalkable(int tileKey)
        {
            return tileKey == 1 || tileKey == 2;
        }

        private void GetMove(float x, float y)
        {
            Vector2 movement = new Vector2(x, y) * speed;

            Rectangle horizontalRect = new Rectangle(
                (int)(position.X + movement.X),
                (int)position.Y,
                playerTexture.Width,
                playerTexture.Height
            );

            if (!CheckCollision(horizontalRect))
            {
                position.X += movement.X;
            }

            Rectangle verticalRect = new Rectangle(
                (int)position.X,
                (int)(position.Y + movement.Y),
                playerTexture.Width,
                playerTexture.Height
            );

            if (!CheckCollision(verticalRect))
            {
                position.Y += movement.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, position, Color.White);
        }
    }
}