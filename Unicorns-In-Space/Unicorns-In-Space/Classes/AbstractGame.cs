using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    abstract class AbstractGame
    {
        protected RenderWindow window;
        GameTime gameTime;

        public AbstractGame(uint width, uint heigth, string titel)
        {
            window = new RenderWindow(new VideoMode(width, heigth), titel);
            window.Closed += (sender, eventargs) => { ((RenderWindow)sender).Close(); };

            gameTime = new GameTime();
        }

        public void Run()
        {
            gameTime.Start();
            while (window.IsOpen())
            {
                window.Clear(new Color(100, 156, 239));
                window.DispatchEvents();
                gameTime.Update();
                Update(gameTime);
                Joystick.Update();
                Draw(window);
                

                window.Display();
            }
        }

        public abstract void Draw(RenderWindow win);
        public abstract void Update(GameTime gameTime);
    }
}
