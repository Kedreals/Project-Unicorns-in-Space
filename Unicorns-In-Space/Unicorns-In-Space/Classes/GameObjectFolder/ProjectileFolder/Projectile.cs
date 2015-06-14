using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Projectile : GameObject
    {
        Vec2 pos;
        Vec2 direction;
        float v = 0;
        bool s;
        bool c;
        bool special;

        public Player owner { get; protected set; }
        public Projectile(Player p, Vec2 dir, bool _special) : base()
        {
            owner = p;
            pos = new Vec2(owner.Position.X + owner.Sprite.Texture.Size.X + 10, owner.Position.Y + owner.Sprite.Texture.Size.Y / 2);
            Texture = new Texture("Textures/Projectile.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = pos;
            HitBox = new HitBox(pos, Texture.Size.X, Texture.Size.Y);
            direction = dir;
            special = _special;
            Position = pos;

            s = owner.sinus;
            c = owner.cosinus;
        }

        public override void Update(GameTime gameTime)
        {
            Console.Clear();
            if (!special)
            {
                if (s)
                {
                    v = (float)Math.Sin((float)gameTime.TotalTime.Milliseconds / 45f);
                    Console.WriteLine("sin");
                }

                else if (c)
                {
                    v = (float)Math.Cos((float)gameTime.TotalTime.Milliseconds / 45f);
                    Console.WriteLine("cos");
                }

                else if (!s && !c)
                {
                    v = 0;
                }

                direction = new Vec2(direction.X, 2 * v);
            }

            Position += direction.GetNormalized() * gameTime.EllapsedTime.Milliseconds;

            if (Position.X > Game.WindowWidth)
                this.Kill();
            
            base.Update(gameTime);
        }
    }
}
