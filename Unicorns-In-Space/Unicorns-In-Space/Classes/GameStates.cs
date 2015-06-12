using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space.Classes
{
    public enum EnumGameStates
    {
        none,
        mainMenu,
        inGame
    }

    interface GameStates
    {
        void Initialize();

        void LoadContent();

        EnumGameStates Update(GameTime gameTime);

        void Draw(RenderWindow window);
    }
}
