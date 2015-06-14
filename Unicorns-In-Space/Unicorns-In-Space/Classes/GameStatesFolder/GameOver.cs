using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class GameOver : GameStates
    {
        Random r = new Random();

        Texture BackgroundTexture = new Texture("Textures/TitleScreenBackgroundTexture.png");
        Texture FlashyTitelTexture = new Texture("Textures/TitelTexture.png");
        Texture GameOverTexture = new Texture("Textures/GameOver.png");
        Texture HighscoreTexture = new Texture("Textures/Highscore.png");

        Sprite Background;
        Sprite FlashyTitel;
        Sprite GameOverSprite;
        Sprite Highscore;
        CircleShape point;

        Shader flashShader;
        Shader fadeShade;

        RenderStates flashState;
        RenderStates fade;

        RenderTexture renderTexture;

        Text HighscorePlayer1;
        Text HighscorePlayer2;

        Text HIGHSCOREFlashy;
        Text highscore;

        bool newPointsSet = true;

        public void Initialize()
        {
            
        }
        
        public void LoadContent()
        {
            Background = new Sprite(BackgroundTexture);
            Background.Scale = new Vec2((float)Game.WindowWidth / (float)BackgroundTexture.Size.X, (float)Game.WindowHeight / (float)BackgroundTexture.Size.Y);
            FlashyTitel = new Sprite(FlashyTitelTexture);
            Background.Position = Vec2.ZERO;
            FlashyTitel.Position = new Vec2(((float)Game.WindowWidth - (float)FlashyTitel.Texture.Size.X) / 2, 10);

            GameOverSprite = new Sprite(GameOverTexture);
            GameOverSprite.Position = new Vec2(((float)Game.WindowWidth - (float)GameOverTexture.Size.X) / 2, FlashyTitel.Position.Y + 200);
            Highscore = new Sprite(HighscoreTexture);
            Highscore.Position = new Vec2(((float)Game.WindowWidth - (float)HighscoreTexture.Size.X) / 2, GameOverSprite.Position.Y + 150);

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
            HighscorePlayer1.Color = Color.Cyan;
            HighscorePlayer1.Position = (Vec2)Highscore.Position + new Vec2(HighscoreTexture.Size.X / 6, HighscoreTexture.Size.Y);
            HighscorePlayer2 = new Text("", new Font("Font/arial_narrow_7.ttf"), 20);
            HighscorePlayer2.Color = Color.Magenta;
            HighscorePlayer2.Position = (Vec2)HighscorePlayer1.Position + new Vec2(HighscoreTexture.Size.X *4/6, 0);

            HIGHSCOREFlashy = new Text("HIGHSCORE", new Font("Font/arial_narrow_7.ttf"), 50);
            HIGHSCOREFlashy.Position = new Vec2(100, 100);

            highscore = new Text(Game.Highscores.ToString(), new Font("Font/arial_narrow_7.ttf"), 20);
            highscore.Position = (Vec2)HIGHSCOREFlashy.Position + new Vec2(0, 50);
        }
        
        public EnumGameStates Update(GameTime gameTime)
        {
            HighscorePlayer1.DisplayedString = Player.highScoreStatic1.ToString();

            if (InGame.PlayerNumbers > 1)
                HighscorePlayer2.DisplayedString = Player.highScoreStatic2.ToString();
            
            flashShader.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds * 5f);
            fadeShade.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds);

            if (Math.Sin(gameTime.TotalTime.Seconds) * 0.5 + 0.5 <= 0.01)
            {
                newPointsSet = true;
            }

            if(Joystick.IsButtonPressed(0, 7))
            {
                return EnumGameStates.mainMenu;
            }

            if (Joystick.IsButtonPressed(0, 6))
            {
                return EnumGameStates.none;
            }

            return EnumGameStates.gameOver;
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
            
            window.Draw(FlashyTitel, flashState);
            window.Draw(GameOverSprite, flashState);
            window.Draw(Highscore, flashState);

            window.Draw(HIGHSCOREFlashy, flashState);
            window.Draw(highscore);

            window.Draw(HighscorePlayer1);
            if (InGame.PlayerNumbers > 1)
                window.Draw(HighscorePlayer2);
        }
    }
}
