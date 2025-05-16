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

        private Dictionary<int, Texture2D> _playerTextures = new();
        private Level _level;

        private Vector2 _position;
        private double _attackStrength;
        private TimeSpan _lastAttackingTime;
        private bool _isAttacking;
        private TimeSpan _attackCooldown;
        
        private (bool, TimeSpan) _isDamaged;
        private TimeSpan _damagedRenderCooldown;

        public Player(Vector2 position, double healthPoints, double attackStrength, IServiceProvider serviceProvider,
            Level level) : base(position, healthPoints)
        {
            _contentManager = new ContentManager(serviceProvider, "Content");
            _level = level;
            _attackStrength = attackStrength;
            _lastAttackingTime = TimeSpan.Zero;
            _isDamaged = (false, TimeSpan.Zero);
            _isAttacking = false;
            _attackCooldown = new TimeSpan(0, 0, 0, 1);
            _damagedRenderCooldown = new TimeSpan(0, 0, 0,0, 250);
            LoadPlayerTextures();
            Reset(position * GameDefaults.TileSize, healthPoints);
        }
        
        public void Update(GameTime gameTime)
        {
            var (x, y) = _movementManager.HandleMovement();
            if (IsAlive)
                MovePlayer(x, y);
            if (gameTime.TotalGameTime - _isDamaged.Item2 > _damagedRenderCooldown)
            {
                _isDamaged = (false, _isDamaged.Item2);
            }
        }

        #region Reset/Setup

        private void Reset(Vector2 position, double healthPoints)
        {
            Position = position;
            _position = Position;
            HealthPoints = healthPoints;
        }
        
        public void ResetPositionToSpawn(Vector2 spawnPosition)
        {
            Reset(spawnPosition * GameDefaults.TileSize, HealthPoints);
        }
        
        public void UpdateLevelReference(Level newLevel)
        {
            _level = newLevel;
        }

        #endregion
        
        #region Load

        private void LoadPlayerTextures()
        {
            _playerTextures = _textureManager.LoadPlayerTextures(_contentManager) ??
                              throw new ContentLoadException("Не удалось загрузить текстуру игрока");
        }

        #endregion
        
        #region Collision

         private void CheckKeyCollision(Action<Key> onKeyPickUp, List<Key> keys)
        {
            var playerRect = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _playerTextures.First().Value.Width,
                _playerTextures.First().Value.Height
            );

            foreach (var key in keys)
            {
                if (key.IsPickedUp) continue;

                var keyRect = new Rectangle(
                    (int)key.Position.X,
                    (int)key.Position.Y,
                    GameDefaults.TileSize,
                    GameDefaults.TileSize
                );

                if (playerRect.Intersects(keyRect))
                {
                    onKeyPickUp(key);
                }
            }
        }
        
        private void CheckHearthCollision(Action<Hearth> onHearthPickUp, List<Hearth> hearths)
        {
            var playerRect = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _playerTextures.First().Value.Width,
                _playerTextures.First().Value.Height
            );

            foreach (var hearth in hearths)
            {
                if (hearth.IsPickedUp) continue;

                var keyRect = new Rectangle(
                    (int)hearth.Position.X,
                    (int)hearth.Position.Y,
                    GameDefaults.TileSize,
                    GameDefaults.TileSize
                );

                if (playerRect.Intersects(keyRect))
                {
                    onHearthPickUp(hearth);
                    HealthPoints += GameDefaults.HearthHealthPoints;
                    Console.WriteLine($"Вы подобрали сердечко. Здоровье увеличено на {GameDefaults.HearthHealthPoints}. Состояние здоровья: ({HealthPoints}/{GameDefaults.PlayerHeathPoints})");
                }
            }
        }

        private void MovePlayer(float x, float y)
        {
            if (_playerTextures.Count == 0) return;

            var movement = new Vector2(x, y) * GameDefaults.PlayerMovementSpeed;
            
            var horizontalRect = new Rectangle(
                (int)(_position.X + movement.X),
                (int)_position.Y,
                _playerTextures.First().Value.Width,
                _playerTextures.First().Value.Height
            );

            CheckKeyCollision(_level.PickUpKey, _level.Keys);
            CheckHearthCollision(_level.PickUpHearth, _level.Hearths);
            
            if (!_movementManager.CheckCollision(horizontalRect, _level.TileMap) &&
                !WillCollideWithEnemy(horizontalRect, _level.Enemies))
            {
                _position.X += movement.X;
            }

            var verticalRect = new Rectangle(
                (int)_position.X,
                (int)(_position.Y + movement.Y),
                _playerTextures.First().Value.Width,
                _playerTextures.First().Value.Height
            );

            if (!_movementManager.CheckCollision(verticalRect, _level.TileMap) &&
                !WillCollideWithEnemy(verticalRect, _level.Enemies))
            {
                _position.Y += movement.Y;
            }

            Position = _position;
        }

        private bool WillCollideWithEnemy(Rectangle futureRect, List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.IsAlive)
                {
                    var enemyRect = new Rectangle(
                        (int)enemy.Position.X,
                        (int)enemy.Position.Y,
                        GameDefaults.TileSize,
                        GameDefaults.TileSize
                    );

                    if (futureRect.Intersects(enemyRect))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
        
        #region Damage

        private bool IsInAttackRange(Vector2 targetPosition)
        {
            return Vector2.Distance(Position, targetPosition) <= GameDefaults.PlayerAttackRange;
        }

        public void TakeDamage(double damage, GameTime gameTime)
        {
            HealthPoints -= damage;
            _isDamaged = (true, gameTime.TotalGameTime);
            Console.WriteLine(
                $"Получен урон: {damage}ед., состояние здоровья: ({HealthPoints}/{GameDefaults.PlayerHeathPoints})");
        }

        public void Hit(IAttacked target, GameTime gameTime)
        {
            if (target is Enemy enemy)
            {
                if (IsInAttackRange(enemy.Position))
                {
                    _lastAttackingTime =
                        DelayedAttack.DelayedHit(target, gameTime, _lastAttackingTime, _attackCooldown,
                            _attackStrength) ??
                        _lastAttackingTime;
                    _isAttacking = true;
                }
            }
        }

        public void SetAttacking(bool isAttacking)
        {
            _isAttacking = isAttacking;
        }

        #endregion

        #region Render

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_playerTextures.Count == 0)
            {
                Console.WriteLine("Player texture is null. Cannot draw player.");
                return;
            }

            _playerTextures.TryGetValue(0, out var playerTexture);
            _playerTextures.TryGetValue(7, out var playerAttackingTexture);
            _playerTextures.TryGetValue(8, out var playerDiedTexture);
            _playerTextures.TryGetValue(9, out var playerDamagedTexture);

            Texture2D? currentTexture = null;

            if (IsAlive && _isDamaged.Item1)
            {
                currentTexture = playerDamagedTexture;
            }
            else if (IsAlive && !_isAttacking)
            {
                currentTexture = playerTexture;
            }
            else if (IsAlive && _isAttacking)
            {
                currentTexture = playerAttackingTexture;
            }
            else if (!IsAlive)
            {
                currentTexture = playerDiedTexture;
            }

            if (currentTexture != null)
            {
                spriteBatch.Draw(
                    currentTexture,
                    _position,
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0.0f);
            }
        }

        #endregion
        
    }
}