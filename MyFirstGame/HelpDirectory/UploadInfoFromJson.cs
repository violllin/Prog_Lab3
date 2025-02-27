using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using MyFirstGame.Core.Game.resTile;
using Microsoft.Xna.Framework.Content;
namespace MyFirstGame.HelpDirectory
{
    public class UploadTileMapFromJson
    {
        public TileMap LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден!");
            }

            try
            {
                string json = File.ReadAllText(filePath);
                TileMap map = JsonSerializer.Deserialize<TileMap>(json);

                if (map == null)
                {
                    throw new Exception($"Ошибка десериализации файла {filePath}: получен null");
                }

                return map;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при обработке файла {filePath}: {ex.Message}");
            }
        }
    }
}
