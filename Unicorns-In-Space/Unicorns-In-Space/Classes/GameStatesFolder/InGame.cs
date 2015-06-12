using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class InGame : GameStates
    {
        Player player;

        public void Initialize()
        {

        }
        
        public void LoadContent()
        {
            player = new Player(new Vec2(10, 10));
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            player.Update(gameTime);

            return EnumGameStates.inGame;
        }

        public void Draw(RenderWindow window)
        {
            player.Draw(window);
        }
    }
}
