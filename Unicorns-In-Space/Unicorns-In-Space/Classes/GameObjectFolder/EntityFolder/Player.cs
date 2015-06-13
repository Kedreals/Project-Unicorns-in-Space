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
            float epsylon = 15;
            Joystick.Update();

            float x, y;

            if (Math.Abs(Joystick.GetAxisPosition(0, Joystick.Axis.X)) > epsylon)
                x = Joystick.GetAxisPosition(0, Joystick.Axis.X);
            else
                x = 0;

            if (Math.Abs(Joystick.GetAxisPosition(0, Joystick.Axis.Y)) > epsylon)
                y = Joystick.GetAxisPosition(0, Joystick.Axis.Y);
            else
                y = 0;

            Vec2 move = new Vec2(x, y);

            if (move.Length < epsylon)
                move = Vec2.ZERO;

            if (move != Vec2.ZERO)
                move = move.GetNormalized();

            Movement = move;
        }

        public override void Update(GameTime gameTime) 
        {
            Control();
            Move(Movement);
            base.Update(gameTime);
        }
    }
}
