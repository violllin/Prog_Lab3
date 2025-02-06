using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstGame.World
{
    interface IEntity
    {
        void AddEntity(Entity entity);
        //void RemoveEntity(Entity entity);
        void AddEntityList(List<Entity> entities);
        void RemoveEntityList();
    }
}
