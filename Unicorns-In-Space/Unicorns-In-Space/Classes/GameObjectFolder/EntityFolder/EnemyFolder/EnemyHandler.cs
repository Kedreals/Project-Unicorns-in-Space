using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class EnemyHandler
    {
        public static List<Enemy> enemyList;

        public EnemyHandler()
        {
            enemyList = new List<Enemy>();
        }

        public void Add(Enemy enemy)
        {
            enemyList.Add(enemy);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemyList.Count; ++i)
            {
                Console.WriteLine(0 - enemyList[i].Sprite.Texture.Size.X);
                if (!enemyList[i].IsAlive || enemyList[i].Position.X < 0 - (int)enemyList[i].Sprite.Texture.Size.X)
                {
                    enemyList.RemoveAt(i);
                    i--;
                }
                else
                {
                    enemyList[i].Update(gameTime);
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(window); 
            }
        }

    }
}
