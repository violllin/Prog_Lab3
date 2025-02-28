using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Feature.TextureManager;

public class TextureManager : ITextureManager
{
    public Dictionary<int, Texture2D> LoadMapTextures(ContentManager contentManager)
    {
        return new Dictionary<int, Texture2D>
        {
            { 0, contentManager.Load<Texture2D>("resGW/wallV")},
            { 2, contentManager.Load<Texture2D>("resGW/floorV")},
            { 3, contentManager.Load<Texture2D>("resGW/chestV1")}
        };
    }

    public (int, Texture2D) LoadEnemyTextures(ContentManager contentManager)
    {
        return (4, contentManager.Load<Texture2D>("resGW/enemyV1"));
    }

    public Texture2D LoadPlayerTextures(ContentManager contentManager)
    {
        return contentManager.Load<Texture2D>("resGW/playerV1");
    }
}