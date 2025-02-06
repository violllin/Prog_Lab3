using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame
{
    public interface IGame
    {
        void Initialize();
        void LoadContent();
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);

    }
}
