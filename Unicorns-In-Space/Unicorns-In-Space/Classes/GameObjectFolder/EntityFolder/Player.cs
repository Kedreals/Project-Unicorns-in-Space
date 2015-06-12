using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Unicorns_In_Space
{
    class Player : GameObject
    {
        public Player(Vec2 spawnPos) : base(spawnPos) {
            MovementSpeed = 1.5f;
            Texture = new Texture("Textures/PlayerTexture.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Sprite.Texture.Size.X, Sprite.Texture.Size.Y);
            Movement = new Vec2(0, 0);
        }

        public void Control()
        {
            float x = Joystick.GetAxisPosition(1, Joystick.Axis.PovX);
            Console.WriteLine("X: " + x);
            float y = Joystick.GetAxisPosition(1, Joystick.Axis.PovY);
            Console.WriteLine("Y: " + y);

            Movement = new Vec2(x, y);
        }

        public override void Update(GameTime gameTime) 
        {
            Control();
            Move(Movement);
            base.Update(gameTime); 
        }
    }
}
