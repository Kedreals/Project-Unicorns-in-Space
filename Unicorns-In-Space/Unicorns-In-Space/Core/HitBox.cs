using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class HitBox
    {
        Vec2 Position;
        Vec2 Size;

        public HitBox(Vec2 pos, Vec2 s)
        {
            Position = pos;
            Size = s;
        }

        public HitBox(Vec2 pos, float width, float height) : this(pos, new Vec2(width, height)) { }

        public bool Collide(HitBox h)
        {
            Vec2 v = h.Position - Position;

            float xCheck = Math.Abs(v.X) - CompareX(h).Size.X;
            float yCheck = Math.Abs(v.Y) - CompareY(h).Size.Y;

            return xCheck <= 0 && yCheck <= 0;
        }

        private HitBox CompareX(HitBox h)
        {
            return (Position.X <= h.Position.X) ? (this) : (h);
        }

        private HitBox CompareY(HitBox h)
        {
            return (Position.Y <= h.Position.Y) ? (this) : (h);
        }

        public void Update(Sprite sprite) { Position = sprite.Position; }
    }
}
