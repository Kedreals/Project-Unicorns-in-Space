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
        ProjectileHandler projectileHandler;
        EnemyHandler enemyHandler;

        public void Initialize()
        {

        }
        
        public void LoadContent()
        {
            playerOne = new Player(new Vec2(10, 10), 0);
            projectileHandler = new ProjectileHandler();
            enemyHandler = new EnemyHandler();
            enemyHandler.Add(new Enemy(new Vec2(1200, 500)));
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            playerOne.Update(gameTime);
            projectileHandler.Update(gameTime);
            enemyHandler.Update(gameTime);

            return EnumGameStates.inGame;
        }

        public void Draw(RenderWindow window)
        {
            
            playerOne.Draw(window);
            projectileHandler.Draw(window);
            enemyHandler.Draw(window);
        }
    }
}
