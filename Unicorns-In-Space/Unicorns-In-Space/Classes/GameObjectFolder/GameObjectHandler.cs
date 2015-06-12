using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space.Classes.GameObjectFolder
{
    abstract class GameObjectHandler
    {
        public Sprite sprite { get; set; }
        public Vector2f position { get; set; }
    }
}
