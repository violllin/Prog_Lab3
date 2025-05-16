using Core.Game.Model;
using Feature;
using Feature.LevelLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Core;

public class App : Microsoft.Xna.Framework.Game
{
    private readonly ILevelLoader _levelLoader = new LevelLoader();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Level _level;
    private bool _gameOver = false;

    public App()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.SynchronizeWithVerticalRetrace = true;
        _level = new Level(Services);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        LoadLevel();
        SetupBufferSize();
    }

    private void SetupBufferSize()
    {
        _graphics.PreferredBackBufferWidth = _level.TileMap.Width * GameDefaults.TileSize;
        _graphics.PreferredBackBufferHeight = _level.TileMap.Height * GameDefaults.TileSize;
        _graphics.ApplyChanges();
    }

    private void LoadLevel()
    {
        _level.LoadTileMap(_levelLoader);
        _level.LoadMapTextures();
        _level.LoadEnemies(Services);
        _level.LoadKeys(Services);
        _player = new Player(_level.FindPlayerPosition(1), GameDefaults.PlayerHeathPoints,
            GameDefaults.PlayerAttackStrength, Services, _level);
    }
    
    private void LoadNextLevel()
    {
        Console.WriteLine("Загрузка следующего уровня...");
        _level = new Level(Services);
        _level.LoadTileMap(_levelLoader);
        _level.LoadMapTextures();
        _level.LoadEnemies(Services);
        _level.LoadKeys(Services);
        _player.ResetPositionToSpawn(_level.LevelSpawnPoint);
        _player.UpdateLevelReference(_level);
        SetupBufferSize();
        _gameOver = false;
        Console.WriteLine("Уровень загружен");
    }


    protected override void Update(GameTime gameTime)
    {
        if (_gameOver) return;
        if (_level.IsCompleted)
        {
            Console.WriteLine("Уровень пройден.");
            _gameOver = true;
            LoadNextLevel();
            return;
        }
        if (!_player.IsAlive)
        {
            Console.WriteLine("Игрок погиб. Игра окончена.");
            _gameOver = true;
            return;
        }

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.Update(gameTime);
        _player.CheckKeyCollision(_level.PickUpKey, _level.Keys);
        _player.CheckEnemyCollision();
        _level.Enemies.ForEach(enemy => enemy.Update(gameTime));
        
        
        
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            _player.SetAttacking(true);
            foreach (Enemy enemy in _level.Enemies)
            {
                {
                    if (_player.IsAlive && enemy.IsAlive)
                    {
                        _player.Hit(enemy, gameTime);
                    }
                }
            }
        } 
        else if (Mouse.GetState().LeftButton == ButtonState.Released)
        {
            _player.SetAttacking(false);
        }


        foreach (var enemy in _level.Enemies)
        {
            if (enemy.IsAlive)
            {
                enemy.Hit(_player, gameTime);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

        _level.Draw(_spriteBatch);
        _level.DrawEnemies(_spriteBatch);
        _level.DrawKeys(_spriteBatch);
        _player.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}