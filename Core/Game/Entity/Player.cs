using Feature;
using Feature.MovementManager;
using Feature.TextureManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Game.Entity
{
    public class Player
    {
        private readonly ITextureManager _textureManager = new TextureManager();
        private readonly IMovementManager _movementManager = new MovementManager();
        private readonly ContentManager _contentManager;
        
        private Texture2D? _playerTexture;
        private Level _level;

        public int HealthPoints { get; set; }
        public bool IsPlayerAlive => HealthPoints > 0;
        
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        private Vector2 _position;

        public Player(Vector2 position, IServiceProvider serviceProvider, Level level)
        {
            _contentManager = new ContentManager(serviceProvider, "Content");
            _level = level;
            LoadPlayerTextures();
            Reset(position);
        }

        private void Reset(Vector2 position)
        {
            Position = position * GameDefaults.TileSize;
            HealthPoints = 10;
        }

        private void LoadPlayerTextures()
        {
            _playerTexture = _textureManager.LoadPlayerTextures(_contentManager);
        }
        
        public void Update(GameTime gameTime)
        {
            var (x, y) = _movementManager.HandleMovement();
            MovePlayer(x, y);
        }
        
        private void MovePlayer(float x, float y)
        {
            if (_playerTexture == null) return;
            
            var movement = new Vector2(x, y) * GameDefaults.PlayerMovementSpeed;

            var horizontalRect = new Rectangle(
                (int)(_position.X + movement.X),
                (int)_position.Y,
                _playerTexture.Width,
                _playerTexture.Height
            );
            
            if (!_movementManager.CheckCollision(horizontalRect, _level.TileMap))
            {
                _position.X += movement.X;
            }

            var verticalRect = new Rectangle(
                (int)_position.X,
                (int)(_position.Y + movement.Y),
                _playerTexture.Width,
                _playerTexture.Height
            );

            if (!_movementManager.CheckCollision(verticalRect, _level.TileMap))
            {
                _position.Y += movement.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_playerTexture, Position, Color.White);
        }
    }
}