using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    abstract class GameObject
    {
        public Sprite Sprite { get; set; }
        protected Texture Texture { get; set; }
        public Vec2 Position { get; set; }
        public HitBox HitBox { get; protected set; }
        public bool IsAlive { get; protected set; }
        public float MovementSpeed { get; set; }
        public Vec2 Movement { get; set; }

        public GameObject(Vec2 spawnPos)
        {
            IsAlive = true;
            Position = spawnPos;
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Sprite.Texture.Size.X, Sprite.Texture.Size.Y);
        }

        public void Move(Vec2 direction)
        {
            direction.Normalize();
            Position += MovementSpeed * direction;
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            HitBox.Update(Sprite);
            Sprite.Position = Position;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }
    }
}
