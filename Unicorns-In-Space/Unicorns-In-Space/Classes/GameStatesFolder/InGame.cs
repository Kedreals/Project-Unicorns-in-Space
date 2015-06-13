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
        Random r = new Random();
        public static int PlayerNumbers { get; set; }

        Texture FlashyTitelTexture = new Texture("Textures/TitelTexture.png");

        Sprite Background;
        Sprite FlashyTitel;
        CircleShape point;

        Shader flashShader;
        Shader fadeShade;

        RenderStates flashState;
        RenderStates fade;

        RenderTexture renderTexture;

        bool newPointsSet = true;

        public void Initialize()
        {
            Background = new Sprite((new RenderTexture(Game.WindowWidth, Game.WindowHeight)).Texture);
            FlashyTitel = new Sprite(FlashyTitelTexture);
            Background.Position = Vec2.ZERO;
            FlashyTitel.Position = new Vec2(((float)Game.WindowWidth - (float)FlashyTitel.Texture.Size.X) / 2, 10);

            flashShader = new Shader(null, "Shader/flashShader.frag");
            flashState = new RenderStates(flashShader);

            fadeShade = new Shader(null, "Shader/fade.frag");
            fade = new RenderStates(fadeShade);

            renderTexture = new RenderTexture(Game.WindowWidth, Game.WindowHeight);
            renderTexture.Display();
            fadeShade.SetParameter("overlay", renderTexture.Texture);

            point = new CircleShape(2);
            point.FillColor = Color.White;
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
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            playerOne.Update(gameTime);
            if (playerTwo != null)
                playerTwo.Update(gameTime);

            projectileHandler.Update(gameTime);
            enemyHandler.Update(gameTime);

            fadeShade.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds);

            if (Math.Sin(gameTime.TotalTime.Seconds) * 0.5 + 0.5 <= 0.01)
            {
                newPointsSet = true;
            }

            int help = (int)gameTime.TotalTime.TotalSeconds / 10 + 1;

            if (EnemyHandler.enemyList.Count < help)
                for (int i = 0; i < help; ++i)
                    enemyHandler.Add(new Enemy(new Vec2(Game.WindowWidth + 5, (float)r.NextDouble() * Game.WindowHeight)));

            if (!playerOne.IsAlive)
                return EnumGameStates.none;

            return EnumGameStates.inGame;
        }

        public void Draw(RenderWindow window)
        {
            if (newPointsSet)
            {
                renderTexture.Clear(Color.Black);

                for (int i = 0; i < 117; ++i)
                {
                    point.Position = new Vec2(r.Next((int)Game.WindowWidth), r.Next((int)Game.WindowHeight));

                    renderTexture.Draw(point);
                }
                renderTexture.Draw(point);

                newPointsSet = false;
            }

            renderTexture.Display();
            window.Draw(Background, fade);

            playerOne.Draw(window);
            if (playerTwo != null)
                playerTwo.Draw(window);
            projectileHandler.Draw(window);
            enemyHandler.Draw(window);
            
        }
    }
}
