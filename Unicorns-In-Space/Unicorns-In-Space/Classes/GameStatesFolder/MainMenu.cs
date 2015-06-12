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
        Stopwatch watch;
        Texture BackgroundTexture = new Texture("Textures/TitleScreenBackgroundTexture.png");
        Texture FlashyTitelTexture = new Texture("Textures/TitelTexture.png");

        Sprite Background;
        Sprite FlashyTitel;

        Shader shader;

        RenderStates state;

        public void Initialize()
        {
            
        }
        
        public void LoadContent()
        {
            watch = new Stopwatch();
            watch.Start();
            Background = new Sprite(BackgroundTexture);
            FlashyTitel = new Sprite(FlashyTitelTexture);
            Background.Position = Vec2.ZERO;
            FlashyTitel.Position = new Vec2(((float)Game.WindowWidth - (float)FlashyTitel.Texture.Size.X) / 2, 10);

            shader = new Shader(null, "Shader/shader.frag");
            state = new RenderStates(shader);
        }
        
        public EnumGameStates Update(GameTime gameTime)
        {
            return EnumGameStates.mainMenu;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(Background);
            window.Draw(FlashyTitel, state);
        }
    }
}
