using Core.Game.Interface;
using Feature;
using Feature.MovementManager;
using Feature.TextureManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Game.Model
{
    public class Player : Entity, IAttacked, IAttacking
    {
        private readonly ITextureManager _textureManager = new TextureManager();
        private readonly IMovementManager _movementManager = new MovementManager();
        private readonly ContentManager _contentManager;
        
        private Texture2D? _playerTexture;
        private Level _level;

        private Vector2 _position;
        private double _attackStrength;
        private TimeSpan _lastAttackingTime;
        private TimeSpan _lastAttackedTime;
        private readonly TimeSpan _attackCooldown; 

        public Player(Vector2 position, double healthPoints, double attackStrength, IServiceProvider serviceProvider, Level level) : base(position, healthPoints)
        {
            _contentManager = new ContentManager(serviceProvider, "Content");
            _level = level;
            _attackStrength = attackStrength;
            _attackCooldown = new TimeSpan(0,0,0, 0, 500);
            Console.WriteLine(_lastAttackingTime.ToString());
            LoadPlayerTextures();
            Reset(position, healthPoints);
        }

        private void Reset(Vector2 position, double healthPoints)
        {
            Position = position * GameDefaults.TileSize;
            _position = Position;
            HealthPoints = healthPoints;
            Console.WriteLine($"Player position initialized to: {_position}");
        }

        private void LoadPlayerTextures()
        {
            _playerTexture = _textureManager.LoadPlayerTextures(_contentManager) ??
                             throw new ContentLoadException("Не удалось загрузить текстуру игрока");
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
            Position = _position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_playerTexture == null)
            {
                Console.WriteLine("Player texture is null. Cannot draw player.");
                return;
            }
            spriteBatch.Draw(
                _playerTexture,
                _position, 
                null, 
                Color.White, 
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0.0f);
        }
        
        private bool IsInAttackRange(Vector2 targetPosition)
        {
            return Vector2.Distance(Position, targetPosition) <= GameDefaults.TileSize;
        }

        public void TakeDamage(double damage, GameTime gameTime)
        {
            var currentTime = gameTime.TotalGameTime;
            if (_lastAttackedTime == TimeSpan.Zero || currentTime - _lastAttackedTime > _attackCooldown)
            {
                HealthPoints -= damage;
                _lastAttackedTime = currentTime;
            }
            else
            {
                return;
            }
            Console.WriteLine($"Получен урон: {damage}ед., состояние здоровья: ({HealthPoints}/{GameDefaults.PlayerHeathPoints})");
        }

        public void Hit(IAttacked target, GameTime gameTime)
        {
            if (target is Enemy enemy && IsInAttackRange(enemy.Position))
            {
                target.TakeDamage(_attackStrength, gameTime);
            }
        }
    }
}