using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using System.Diagnostics;

namespace Unicorns_In_Space
{
    class Player : GameObject
    {
        public Int64 HighScore = 0;
        Stopwatch specialShootTimer;
        Stopwatch notMoving;
        public static long highScoreStatic1;
        public static long highScoreStatic2;
        private uint joyStickNumber;
        public bool buttonPressed = true;
        long i;

        public bool sinus = false;
        public bool cosinus = false;
        bool shootMoveSwitchPressed = true;

        public Player(Vec2 spawnPos, uint _joyStickNumber) : base(spawnPos) {
            MovementSpeed = 4f;
            Texture = new Texture("Textures/PlayerTexture.png");
            Sprite = new Sprite(Texture);
            Sprite.Position = spawnPos;
            HitBox = new HitBox(spawnPos, Sprite.Texture.Size.X, Sprite.Texture.Size.Y);
            joyStickNumber = _joyStickNumber;
            specialShootTimer = new Stopwatch();
            notMoving = new Stopwatch();
        }

        public void SpecialShoot()
        {
            i = HighScore % 1000;
            if (HighScore > 0 && (i <= 900 && i >= 100))
            {
                specialShootTimer.Start();
            }

            if (specialShootTimer.Elapsed.Seconds > 5)
                specialShootTimer.Reset();
        }

        public void ShootMovment(GameTime gameTime)
        {
            if (Joystick.IsButtonPressed(joyStickNumber, 2) && !shootMoveSwitchPressed) //X Button Sinus
            {
                shootMoveSwitchPressed = true;
                sinus = !sinus;
                cosinus = false;
            }

            else if (Joystick.IsButtonPressed(joyStickNumber, 3) && !shootMoveSwitchPressed) //Y Button Cosinus
            {
                shootMoveSwitchPressed = true;
                sinus = false;
                cosinus = !cosinus;
            }

            else if (!Joystick.IsButtonPressed(joyStickNumber, 2) && !Joystick.IsButtonPressed(joyStickNumber, 3))
            {
                shootMoveSwitchPressed = false;
            }
        }

        public void Shoot()
        {
            SpecialShoot();
            
            if(specialShootTimer.IsRunning)
            {
                ProjectileHandler.projectileList.Add(new Projectile(this, new Vec2(1, 1), true));
                ProjectileHandler.projectileList.Add(new Projectile(this, new Vec2(1, -1), true));
            }
            ProjectileHandler.projectileList.Add(new Projectile(this, new Vec2(1, 1), false));
        }

        public void Control(GameTime gameTime)
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
            {
                move = Vec2.ZERO;
            }

            if(move == Vec2.ZERO)
            {
                notMoving.Start();
            }

            if (notMoving.Elapsed.Seconds > 2)
                HighScore -= gameTime.TotalTime.Seconds/ gameTime.TotalTime.Seconds;

            if (move != Vec2.ZERO)
            {
                notMoving.Reset();
                move = move.GetNormalized();
            }

            Movement = move;
        }

        public void CollisionWithEnemy()
        {
            foreach (Enemy enemy in EnemyHandler.enemyList)
            {
                if (HitBox.Collide(enemy.HitBox))
                {
                    if(InGame.PlayerNumbers > 1)
                        HighScore = (HighScore * 9) / 10;
                    IsAlive = false;
                }
            }
        }

        public override void Update(GameTime gameTime) 
        {
            Control(gameTime);
            Move(Movement * gameTime.EllapsedTime.Milliseconds);
            CollisionWithEnemy();
            ShootMovment(gameTime);

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
