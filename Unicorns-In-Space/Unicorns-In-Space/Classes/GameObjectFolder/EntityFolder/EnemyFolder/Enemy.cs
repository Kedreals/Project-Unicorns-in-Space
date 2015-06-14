using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Enemy : GameObject
    {
        Stopwatch stop;
        rand r;
        public Enemy(Vec2 spawnPos) : base(spawnPos)
        {
            MovementSpeed = 0.5f;
            Texture = new Texture("Textures/Enemy.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Texture.Size.X, Texture.Size.Y);
            stop = new Stopwatch();
            stop.Start();
            Random ra = new Random();
            int rando = ra.Next(5);

            switch (rando)
            {
                case 0:
                    r = (d) => { return (float)Math.Sin(d); };
                    break;
                case 1:
                    r = (d) => { return (float)Math.Cos(d); };
                    break;
                case 2:
                    r = (d) => { return -(float)Math.Cos(d); };
                    break;
                case 3:
                    r = (d) => { return -(float)Math.Sin(d); };
                    break;
                default:
                    r = (d) => { return 0.0f; };
                    break;
            }
        }

        public void CollisionWithProjectile()
        {
            foreach (Projectile proj in ProjectileHandler.projectileList)
            {
                if (HitBox.Collide(proj.HitBox))
                {
                    IsAlive = false;
                    proj.IsAlive = false;
                    proj.owner.HighScore += 10;
                }
            }
        }

        delegate float rand(double d);

        public void RandomMovement()
        {
            Movement = new Vec2(-1, r(stop.Elapsed.Seconds + 60*(stop.Elapsed.Minutes + 60*stop.Elapsed.Hours)));
        }

        public override void Update(GameTime gameTime)
        {

            CollisionWithProjectile();
            RandomMovement();
            MovementSpeed = (float)gameTime.TotalTime.Seconds / 10.0f + 1.0f;
            Move(Movement);
            base.Update(gameTime);
        }
    }
}
