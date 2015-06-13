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
        Vec2 direction;

        public Player owner { get; protected set; }
        public Projectile(Vec2 spawnPos, Player p, Vec2 dir) : base(spawnPos)
        {
            owner = p;
            MovementSpeed = 2f;
            Texture = new Texture("Textures/Projectile.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Texture.Size.X, Texture.Size.Y);
            direction = dir;
        }

        public override void Update(GameTime gameTime)
        {
            Position += direction.GetNormalized() * MovementSpeed;

            if (Position.X > Game.WindowWidth)
                this.Kill();
            
            base.Update(gameTime);
        }
    }
}
