using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class ProjectileHandler
    {
        public static List<Projectile> projectileList { get; set; }

        public ProjectileHandler()
        {
            projectileList = new List<Projectile>();
        }

        public void Add(Projectile proj)
        {
            projectileList.Add(proj);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; ++i)
            {
                if (!projectileList[i].IsAlive)
                {
                    projectileList.RemoveAt(i);
                    i--;
                }
                else
                {
                    projectileList[i].Update(gameTime);
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach(Projectile proj in projectileList)
            {
                proj.Draw(window);
                proj.HitBox.Debug(window);
            }
        }

    }
}
