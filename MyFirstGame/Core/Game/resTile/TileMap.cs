using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyFirstGame.Core.Game.resTile
{
    public class TileMap
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("tileMap")]
        public List<List<int>> Tiles { get; set; } = new List<List<int>>();
        public int[,] GetTileMap()
        {
            int[,] result = new int[Width, Height];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    result[y, x] = Tiles[y][x];
                }
            }
            return result;
        }
    }
}
