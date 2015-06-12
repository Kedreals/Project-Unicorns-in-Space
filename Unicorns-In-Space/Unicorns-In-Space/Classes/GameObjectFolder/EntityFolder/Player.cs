using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;

namespace Unicorns_In_Space
{
    class Player : GameObject
    {
        public Player(Vec2 spawnPos) : base(spawnPos) {
            MovementSpeed = 1.5f;
        }

        public void Control()
        {
            float x = Joystick.GetAxisPosition(0, Joystick.Axis.X);
            float y = Joystick.GetAxisPosition(0, Joystick.Axis.Y);

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
