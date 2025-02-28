using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core.Game.Entity;
using Feature;
using Feature.LevelLoader;

namespace Core;

public class App : Microsoft.Xna.Framework.Game
{
    private readonly ILevelLoader _levelLoader = new LevelLoader();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private Level _level;

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
        _level.LoadEnemyTexture();
        _player = new Player(_level.FindPlayerPosition(1), Services, _level);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _player.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

        _level.Draw(_spriteBatch);
        _player.Draw(_spriteBatch);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}