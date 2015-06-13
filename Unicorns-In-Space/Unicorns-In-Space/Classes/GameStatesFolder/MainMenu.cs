using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class MainMenu : GameStates
    {
        Random r = new Random();

        Texture BackgroundTexture = new Texture("Textures/TitleScreenBackgroundTexture.png");
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
            
        }
        
        public void LoadContent()
        {
            Background = new Sprite(BackgroundTexture);
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
        


        public EnumGameStates Update(GameTime gameTime)
        {
            flashShader.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds * 5f);
            fadeShade.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds);

            if (Math.Sin(gameTime.TotalTime.Seconds) * 0.5 + 0.5 <= 0.01)
            {
                newPointsSet = true;
            }

            return EnumGameStates.mainMenu;
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
        }
    }
}
