﻿using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class GameObjectHandler
    {
        public static List<GameObject> gameObjectList { get; set; }

        public GameObjectHandler()
        {
            gameObjectList = new List<GameObject>();
        }

        public static void Add(GameObject obj)
        {
            gameObjectList.Add(obj);
        }

        public void Deleate()
        {
            foreach (GameObject gObj in gameObjectList)
                gObj.Kill();
        }

        public void Update(GameTime gameTime)
        {
            gameObjectList.Sort();

            for(int i =0; i< gameObjectList.Count; ++i)
            {
                if(!gameObjectList[i].IsAlive)
                {
                    gameObjectList.RemoveAt(i);
                    i--;
                }
                else
                {
                    gameObjectList[i].Update(gameTime);
                }
            }
        }

        public void Draw(RenderWindow window)
        {
            foreach (GameObject gObj in gameObjectList)
            {
                gObj.Draw(window);
            }
        }
    }
}
