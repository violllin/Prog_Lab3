using System.Text.Json;

namespace Feature.ResourceManager;

public class ResourceManager : IResourceManager
{
    public TileMap LoadTileMap(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Файл {path} не найден!");
        }

        try
        {
            var json = File.ReadAllText(path);
            var map = JsonSerializer.Deserialize<TileMap>(json);
            if (map == null)
            {
                throw new Exception($"Ошибка десериализации файла {path}: получен null");
            }
            return map;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при обработке файла {path}: {ex.Message}");
        }
    }
}