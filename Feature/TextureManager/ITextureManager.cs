using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Feature.TextureManager;

public interface ITextureManager
{
    Dictionary<int, Texture2D> LoadMapTextures(ContentManager contentManager);
    (int, Texture2D) LoadEnemyTextures(ContentManager contentManager);
    Texture2D LoadPlayerTextures(ContentManager contentManager);
}