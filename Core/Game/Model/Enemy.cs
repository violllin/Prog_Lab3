using Core.Game.Interface;
using Feature;
using Feature.TextureManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Game.Model;

public class Enemy : Entity, IAttacked, IAttacking
{
    private readonly ITextureManager _textureManager = new TextureManager();
    private readonly ContentManager _contentManager;
    private Texture2D? _enemyTexture;
    
    private readonly double _attackStrength;
    
    public Enemy(Vector2 position, double healthPoints, double attackStrength, IServiceProvider serviceProvider)
        : base(position, healthPoints)
    {
        _contentManager = new ContentManager(serviceProvider, "Content");
        _attackStrength = attackStrength;
        LoadEnemyTextures();
        Reset(position, healthPoints);
    }
    
    private void Reset(Vector2 position, double healthPoints)
    {
        Position = position;
        HealthPoints = healthPoints;
        Console.WriteLine($"Enemy position initialized to: {Position}");
    }
    
    private void LoadEnemyTextures()
    {
        _enemyTexture = _textureManager.LoadEnemyTextures(_contentManager).Item2;
    }
    
    private bool IsInAttackRange(Vector2 targetPosition)
    {
        var distance = Vector2.Distance(Position, targetPosition);
        return distance <= GameDefaults.EyeEnemyAttackRange;
    }

    public void Hit(IAttacked target, GameTime gameTime)
    {
        if (target is Player entity)
        {
            if (IsInAttackRange(entity.Position))
            {
                target.TakeDamage(_attackStrength, gameTime);
            }
        }
        
    }
    
    public void TakeDamage(double damage, GameTime gameTime)
    {
        HealthPoints -= damage;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_enemyTexture == null)
        {
            Console.WriteLine("Enemy texture is null. Cannot draw player.");
            return;
        }

        spriteBatch.Draw(
            _enemyTexture,
            Position, 
            null, 
            Color.White, 
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            0.0f);
    }
}