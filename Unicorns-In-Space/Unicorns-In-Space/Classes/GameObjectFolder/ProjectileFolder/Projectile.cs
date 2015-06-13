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
        public Projectile(Vec2 spawnPos) : base(spawnPos)
        {
            MovementSpeed = 3f;
            Texture = new Texture("Textures/Projectile.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Texture.Size.X, Texture.Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
            Position += new Vec2(1,0);

            if (Position.X > Game.WindowWidth)
                this.Kill();
            
            base.Update(gameTime);
        }
    }
}
