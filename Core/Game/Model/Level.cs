using Feature;
using Feature.LevelLoader;
using Feature.TextureManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace Core.Game.Model;

public class Level
{
    private readonly ITextureManager _textureManager = new TextureManager();
    private Dictionary<int, Texture2D> _texturesGw = [];
    private readonly ContentManager _contentManager;
    private Microsoft.Xna.Framework.Vector2 _position;

    public Vector2 LevelSpawnPoint;

    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    public List<Key> Keys { get; private set; } = new List<Key>();
    public List<Hearth> Hearths { get; private set; } = new List<Hearth>();
    public int PickedKeys { get; set; }
    public int EnemiesDefeated { get; set; }
    public bool IsCompleted => PickedKeys == Keys.Count && EnemiesDefeated == Enemies.Count;

    public TileMap TileMap { get; set; }

    public Vector2 FindPlayerPosition(int playerIndex)
    {
        for (var y = 0; y < TileMap.Height; y++)
        {
            var row = TileMap.Tiles[y];
            for (var x = 0; x < TileMap.Width; x++)
            {
                if (row[x] == playerIndex)
                {
                    return new Vector2(x, y);
                }
            }
        }
        return Vector2.Zero;
    }

    public Level(IServiceProvider serviceProvider)
    {
        _contentManager = new ContentManager(serviceProvider, "Content");
        InitTileMap();
    }

    #region Handlers

    public void OnEnemyDefeat()
    {
        EnemiesDefeated++;
        Console.WriteLine($"{EnemiesDefeated}/{Enemies.Count} enemies defeated."); 
    }

    public void PickUpKey(Key key)
    {
        key.PickUp();
        PickedKeys++;
    }
    
    public void PickUpHearth(Hearth hearth)
    {
        hearth.PickUp();
    }

    #endregion

    #region LoadRegion

    public void LoadTileMap(ILevelLoader levelLoader)
    {
        TileMap = levelLoader.LoadRandomLevel();
        LevelSpawnPoint = LoadSpawnPoint();
    }

    public void LoadMapTextures()
    {
        var mapTextures = _textureManager.LoadMapTextures(_contentManager);
        foreach (var texture in mapTextures)
        {
            _texturesGw.Add(texture.Key, texture.Value);
        }
    }

    private Vector2 LoadSpawnPoint()
    {
        for (var y = 0; y < TileMap.Height; y++)
        {
            for (var x = 0; x < TileMap.Width; x++)
            {
                if (TileMap.Tiles[y][x] == (int) TileMatching.SpawnPoint)
                {
                    return new Vector2(x, y);
                }
            }
        }
        throw new NullReferenceException("Spawn point not found");
    }

    public void LoadEnemies(GameServiceContainer services)
    {
        for (var y = 0; y < TileMap.Height; y++)
        {
            for (var x = 0; x < TileMap.Width; x++)
            {
                if (TileMap.Tiles[y][x] == (int)TileMatching.Enemy)
                {
                    var position = new Vector2(x, y) * GameDefaults.TileSize;
                    Enemies.Add(new Enemy(position, GameDefaults.EyeEnemyHealthPoints,
                        GameDefaults.EyeEnemyAttackStrength, services));
                }
            }
        }
    }

    public void LoadKeys(GameServiceContainer services)
    {
        for (var y = 0; y < TileMap.Height; y++)
        {
            for (var x = 0; x < TileMap.Width; x++)
            {
                if (TileMap.Tiles[y][x] == (int)TileMatching.Key)
                {
                    var position = new Vector2(x, y) * GameDefaults.TileSize;
                    Keys.Add(new Key(position, services));
                }
            }
        }
    }
    
    public void LoadHearths(GameServiceContainer services)
    {
        for (var y = 0; y < TileMap.Height; y++)
        {
            for (var x = 0; x < TileMap.Width; x++)
            {
                if (TileMap.Tiles[y][x] == (int)TileMatching.Hearth)
                {
                    var position = new Vector2(x, y) * GameDefaults.TileSize;
                    Hearths.Add(new Hearth(position, services));
                }
            }
        }
    }

    #endregion

    #region InitializeRegion

    private void InitTileMap()
    {
        TileMap = new TileMap();
    }

    #endregion

    #region RenderRegion

    public void Draw(SpriteBatch spriteBatch)
    {
        if (TileMap.Width > 0 && TileMap.Height > 0)
        {
            for (var y = 0; y < TileMap.Height; y++)
            {
                for (var x = 0; x < TileMap.Width; x++)
                {
                    var tileKey = TileMap.Tiles[y][x];
                    if (tileKey is 1 or 3 or 11 or 12) tileKey = 2;

                    if (!_texturesGw.TryGetValue(tileKey, out var texture))
                    {
                        Console.WriteLine($"Текстура для ключа {tileKey} не найдена.");
                        continue;
                    }
                    _position = new Microsoft.Xna.Framework.Vector2(x * GameDefaults.TileSize, y * GameDefaults.TileSize);
                    spriteBatch.Draw(texture, _position, null, Color.White, 0f, Vector2.Zero, 1f,
                        SpriteEffects.None, 0.8f);
   
                }
            }
        }
        else
        {
            throw new ArgumentException("tileMap doesn't have a correct size");
        }
    }

    public void DrawEnemies(SpriteBatch spriteBatch)
    {
        foreach (var enemy in Enemies)
        {
            enemy.Draw(spriteBatch);
        }
    }
    
    public void DrawKeys(SpriteBatch spriteBatch)
    {
        foreach (var key in Keys)
        {
            key.Draw(spriteBatch);
        }
    }
    
    public void DrawHearths(SpriteBatch spriteBatch)
    {
        foreach (var hearth in Hearths)
        {
            hearth.Draw(spriteBatch);
        }
    }

    #endregion
}