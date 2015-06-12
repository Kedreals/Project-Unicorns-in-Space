using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space.Classes
{
    abstract class GameObject
    {
        public Sprite sprite { get; set; }
        public Vec2 position { get; set; }
        public HitBox hitBox { get; protected set; }
        protected bool _isAlive = true;
        public bool isAlive { get { return _isAlive; } set { _isAlive = value; } }

    }
}
