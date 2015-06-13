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
        public Sprite Sprite { get; protected set; }
        protected Texture Texture { get; set; }
        public Vec2 Position { get; protected set; }
        public HitBox HitBox { get; protected set; }
        public bool IsAlive { get; set; }
        public float MovementSpeed { get; set; }
        public Vec2 Movement { get; set; }

        public GameObject(Vec2 spawnPos)
        {
            IsAlive = true;
            Position = spawnPos;
        }

        public void Move(Vec2 direction)
        {
            Vec2 nextPos = Position + MovementSpeed * direction;
            Vec2 help = nextPos;

            if (GetType().Name.Equals("Player"))
            {
                if (nextPos.X >= Game.WindowWidth - Sprite.Texture.Size.X || nextPos.X <= 0)
                    help.X = Position.X;
            }

            if (nextPos.Y >= Game.WindowHeight - Sprite.Texture.Size.Y || nextPos.Y <= 0)
                help.Y = Position.Y;

            Position = help;
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            Sprite.Position = Position;
            HitBox.Update(Sprite);
        }

        public virtual void Draw(RenderWindow window)
        {
            window.Draw(Sprite);
        }
    }
}
