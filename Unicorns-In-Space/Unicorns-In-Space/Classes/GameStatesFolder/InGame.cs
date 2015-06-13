using SFML.Graphics;
using SFML.Window;
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
        Shader shadeShader;

        RenderStates flashState;
        RenderStates fade;
        RenderStates shade;

        RenderTexture renderTexture;

        Text HighscorePlayer1;
        Text HighscorePlayer2;

        bool newPointsSet = true;
        bool resetGameTime = true;

        public void Initialize()
        {
            resetGameTime = true;
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

            HighscorePlayer1 = new Text("", new Font("Font/arial_narrow_7.ttf"), 20);
            HighscorePlayer1.Color = Color.Magenta;
            HighscorePlayer1.Position = new Vec2(10, 10);
            HighscorePlayer2 = new Text("", new Font("Font/arial_narrow_7.ttf"), 20);
            HighscorePlayer2.Color = Color.Cyan;
            HighscorePlayer2.Position = (Vec2)HighscorePlayer1.Position + new Vec2(0, 25);
        }

        public void LoadContent()
        {
            if (PlayerNumbers == 1)
            {
                playerOne = new Player(new Vec2(10, 10), 0);
            }
            else if (PlayerNumbers == 2)
            {
                playerOne = new Player(new Vec2(10, 10), 0);
                playerTwo = new Player(new Vec2(10, 900), 1);
                playerTwo.Sprite.Color = new Color((byte)(playerTwo.Sprite.Color.R + 90), playerTwo.Sprite.Color.G, playerTwo.Sprite.Color.B);
            }
            projectileHandler = new ProjectileHandler();
            enemyHandler = new EnemyHandler();
        }

        public EnumGameStates Update(GameTime gameTime)
        {
            flashShader.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds * 5f);

            if (resetGameTime)
            {
                gameTime.Restart();
                resetGameTime = false;
            }

            HighscorePlayer1.DisplayedString = playerOne.HighScore.ToString();

            if (PlayerNumbers > 1)
                HighscorePlayer2.DisplayedString = playerTwo.HighScore.ToString();

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
                    enemyHandler.Add(new Enemy(new Vec2(Game.WindowWidth + 5, (float)r.NextDouble() * (Game.WindowHeight - 55))));

            if(playerTwo != null)
            {
                if (!playerOne.IsAlive || !playerTwo.IsAlive)
                {
                    Player.highScoreStatic1 = playerOne.HighScore;
                    Player.highScoreStatic2 = playerTwo.HighScore;
                    return EnumGameStates.gameOver;
                }
            }
            else
            {
                if (!playerOne.IsAlive)
                {
                    Player.highScoreStatic1 = playerOne.HighScore;
                    return EnumGameStates.gameOver;
                }
            }

            if (Joystick.IsButtonPressed(0, 6))
            {
                return EnumGameStates.none;
            }

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

            window.Draw(playerOne.Sprite);
            if (playerTwo != null)
                window.Draw(playerTwo.Sprite);
            projectileHandler.Draw(window);
            enemyHandler.Draw(window);
            window.Draw(HighscorePlayer1);
            if (PlayerNumbers > 1)
                window.Draw(HighscorePlayer2);
        }
    }
}
