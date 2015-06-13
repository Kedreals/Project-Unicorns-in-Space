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
            MovementSpeed = 1.5f;
            Texture = new Texture("Textures/Enemy.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Texture.Size.X, Texture.Size.Y);
        }

        public bool CollisionWithProjectile()
        {
            foreach (Projectile proj in ProjectileHandler.projectileList)
            {
                if (HitBox.Collide(proj.HitBox))
                {
                    Console.Clear();
                    Console.WriteLine("hit!!!!!!!!!!!!!!!!!");
                    return true;
                }
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            CollisionWithProjectile();
            base.Update(gameTime);
        }
    }
}
