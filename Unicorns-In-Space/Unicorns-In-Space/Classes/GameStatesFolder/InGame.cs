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
        Player playerOne;
        Player playerTwo;
        ProjectileHandler projectileHandler;
        EnemyHandler enemyHandler;
        public static int PlayerNumbers { get; set; }

        public void Initialize()
        {

        }
        
        public void LoadContent()
        {
            if (PlayerNumbers == 1)
                playerOne = new Player(new Vec2(10, 10), 0);
            else if (PlayerNumbers == 2)
            {
                playerOne = new Player(new Vec2(10, 10), 0);
                playerTwo = new Player(new Vec2(10, 900), 1);
            }
            projectileHandler = new ProjectileHandler();
            enemyHandler = new EnemyHandler();
            enemyHandler.Add(new Enemy(new Vec2(1200, 500)));
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            playerOne.Update(gameTime);
            if (playerTwo != null)
                playerTwo.Update(gameTime);

            projectileHandler.Update(gameTime);
            enemyHandler.Update(gameTime);

            if(!playerOne.IsAlive)
                return EnumGameStates.none;

            return EnumGameStates.inGame;
        }

        public void Draw(RenderWindow window)
        {
            
            playerOne.Draw(window);
            if (playerTwo != null)
                playerTwo.Draw(window);
            projectileHandler.Draw(window);
            enemyHandler.Draw(window);
            
        }
    }
}
