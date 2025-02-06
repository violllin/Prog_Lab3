using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame.World
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public string Id;
        public string Type;
        public bool IsActive;
    }
}
