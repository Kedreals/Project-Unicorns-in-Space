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
        private uint joyStickNumber;
        public bool buttonPressed = true;

        public Player(Vec2 spawnPos, uint _joyStickNumber) : base(spawnPos) {
            MovementSpeed = 2f;
            Texture = new Texture("Textures/PlayerTexture.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Sprite.Texture.Size.X, Sprite.Texture.Size.Y);
            joyStickNumber = _joyStickNumber;
        }

        public void Shoot()
        {
            ProjectileHandler.projectileList.Add(new Projectile(new Vec2(Sprite.Position.X + Sprite.Texture.Size.X + 10, Sprite.Position.Y + Sprite.Texture.Size.Y / 2)));
        }

        public void Control()
        {
            float epsylon = 15;
           
            float x, y;

            if (Math.Abs(Joystick.GetAxisPosition(joyStickNumber, Joystick.Axis.X)) > epsylon)
                x = Joystick.GetAxisPosition(joyStickNumber, Joystick.Axis.X);
            else
                x = 0;

            if (Math.Abs(Joystick.GetAxisPosition(joyStickNumber, Joystick.Axis.Y)) > epsylon)
                y = Joystick.GetAxisPosition(joyStickNumber, Joystick.Axis.Y);
            else
                y = 0;

            Vec2 move = new Vec2(x, y);

            if (move.Length < epsylon)
                move = Vec2.ZERO;

            if (move != Vec2.ZERO)
                move = move.GetNormalized();

            Movement = move;
        }

        public void CollisionWithEnemy()
        {
            foreach (Enemy enemy in EnemyHandler.enemyList)
            {
                if (HitBox.Collide(enemy.HitBox))
                {
                    IsAlive = false;
                }
            }
        }

        public override void Update(GameTime gameTime) 
        {
            Control();
            Move(Movement);
            CollisionWithEnemy();

            if (Joystick.IsButtonPressed(joyStickNumber, 0) && !buttonPressed) //button A
            {
                Shoot();
                buttonPressed = true;
            }

            else if(!Joystick.IsButtonPressed(joyStickNumber, 0))
            {
                buttonPressed = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(RenderWindow window)
        {
            base.Draw(window);
        }
    }
}
