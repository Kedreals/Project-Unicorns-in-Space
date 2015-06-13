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
        public Enemy(Vec2 spawnPos) : base(spawnPos)
        {
            MovementSpeed = 0.5f;
            Texture = new Texture("Textures/Enemy.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Texture.Size.X, Texture.Size.Y);
            stop = new Stopwatch();
            stop.Start();
        }

        public void CollisionWithProjectile()
        {
            foreach (Projectile proj in ProjectileHandler.projectileList)
            {
                if (HitBox.Collide(proj.HitBox))
                {
                    IsAlive = false;
                    proj.IsAlive = false;
                }
            }
        }

        public void RandomMovement()
        {
            Movement = new Vec2(-1, (float)Math.Cos(stop.Elapsed.TotalSeconds * 2));
        }

        public override void Update(GameTime gameTime)
        {

            CollisionWithProjectile();
            RandomMovement();
            Move(Movement);
            base.Update(gameTime);
        }
    }
}
