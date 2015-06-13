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
        Texture BackgroundTexture = new Texture("Textures/TitleScreenBackgroundTexture.png");
        Texture FlashyTitelTexture = new Texture("Textures/TitelTexture.png");

        Sprite Background;
        Sprite FlashyTitel;

        Shader flashShader;
        Shader sparkleShader;

        RenderStates flashState;
        RenderStates sparkle;

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

            sparkleShader = new Shader(null, "Shader/sparkleShader.frag");
            sparkle = new RenderStates(sparkleShader);
        }
        
        public EnumGameStates Update(GameTime gameTime)
        {
            flashShader.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds * 5f);
            sparkleShader.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds);

            return EnumGameStates.mainMenu;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Background, sparkle);
            window.Draw(FlashyTitel, flashState);
        }
    }
}
