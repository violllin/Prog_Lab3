using System.Text.Json.Serialization;

namespace Feature
{
    public class TileMap
    {
        private List<List<int>> _tiles = [];

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("tileMap")]
        public List<List<int>> Tiles
        {
            get => _tiles;
            set => _tiles = value;
        }

        public int[,] GetTileMap()
        {
            var result = new int[Width, Height];
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    result[y, x] = Tiles[y][x];
                }
            }
            return result;
        }
    }
}
