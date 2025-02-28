namespace Feature.LevelLoader;

public interface ILevelLoader
{
    TileMap LoadLevel(string levelName);
    TileMap LoadRandomLevel();
}