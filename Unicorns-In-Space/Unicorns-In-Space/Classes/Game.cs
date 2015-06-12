using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Game : AbstractGame
    {
        public static uint WindowWidth = 1280;
        public static uint WindowHeight = 720;

        public Game() : base(WindowWidth, WindowHeight, "Project: Unicorns in Space") { }

        public override void Draw(RenderWindow win)
        {
        }

        public override void Update(GameTime gameTime)
        {
            gameTime.Update();
        }
    }
}
