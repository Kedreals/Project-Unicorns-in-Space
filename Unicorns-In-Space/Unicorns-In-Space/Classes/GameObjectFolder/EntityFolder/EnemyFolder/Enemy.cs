using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Enemy : GameObject
    {
        public Enemy(Vec2 spawnPos) : base(spawnPos)
        {
            MovementSpeed = 0.5f;
            Texture = new Texture("Textures/Enemy.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Texture.Size.X, Texture.Size.Y);
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

        public void RandomMovement(GameTime gameTime)
        {
            Random r = new Random();

            Movement = new Vec2(-1, (float)Math.Cos(gameTime.TotalTime.TotalSeconds * 2));
        }

        public override void Update(GameTime gameTime)
        {
            CollisionWithProjectile();
            RandomMovement(gameTime);
            Move(Movement);
            base.Update(gameTime);
        }
    }
}
