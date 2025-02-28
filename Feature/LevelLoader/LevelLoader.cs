using Feature.ResourceManager;

namespace Feature.LevelLoader;

public class LevelLoader : ILevelLoader
{
    private readonly IResourceManager _resourceManager = new ResourceManager.ResourceManager();
    
    public TileMap LoadLevel(string levelName)
    {
        return _resourceManager.LoadTileMap(GetPathToLevel(levelName));
    }

    public TileMap LoadRandomLevel()
    {
        var directoryPath = GetPathToLevel();
        var files = Directory.GetFiles(directoryPath, "*.json");
        if (files.Length == 0) throw new NullReferenceException("No maps found.");
        var randomMap = files[new Random().Next(files.Length)];
        return _resourceManager.LoadTileMap(randomMap);
    }
    
    private string GetPathToLevel(string? levelName = null)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var projectRoot = Directory.GetParent(baseDir)?.Parent?.Parent;
        if (projectRoot == null)
        {
            throw new DirectoryNotFoundException("Не удалось найти корневую директорию проекта.");
        }

        var relativePath = Path.Combine("..", "..", "Levels", "Levels", levelName == null ? "" : $"{levelName}.json");
        return Path.GetFullPath(Path.Combine(projectRoot.FullName, relativePath));
    }
}