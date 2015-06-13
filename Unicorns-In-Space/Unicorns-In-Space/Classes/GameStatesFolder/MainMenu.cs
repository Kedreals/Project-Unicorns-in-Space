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
    class MainMenu : GameStates
    {
        Random r = new Random();

        Texture BackgroundTexture = new Texture("Textures/TitleScreenBackgroundTexture.png");
        Texture FlashyTitelTexture = new Texture("Textures/TitelTexture.png");

        Texture OnePlayerTexture = new Texture("Textures/OnePlayerText.png");
        Texture TwoPlayerTexture = new Texture("Textures/TwoPlayerText.png");

        Sprite Background;
        Sprite FlashyTitel;
        CircleShape point;

        Sprite OnePlayer;
        Sprite TwoPlayer;

        Shader flashShader;
        Shader fadeShade;

        RenderStates flashState;
        RenderStates fade;

        RenderTexture renderTexture;

        bool newPointsSet = true;

        bool onePlayerSelected = true;
        bool twoPlayerSelected = false;
        bool buttonPressed = false;

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

            OnePlayer = new Sprite(OnePlayerTexture);
            OnePlayer.Position = new Vec2(((float)Game.WindowWidth - (float)OnePlayerTexture.Size.X) / 2, FlashyTitel.Position.Y + 300);
            TwoPlayer = new Sprite(TwoPlayerTexture);
            TwoPlayer.Position = new Vec2(((float)Game.WindowWidth - (float)TwoPlayerTexture.Size.X) / 2, OnePlayer.Position.Y + 50);

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

        public void SelectedSprite()
        {
            if ((Joystick.GetAxisPosition(0, Joystick.Axis.PovX) > 5 || Joystick.GetAxisPosition(0, Joystick.Axis.PovX) < -5) && !buttonPressed)
            {
                onePlayerSelected = !onePlayerSelected;
                twoPlayerSelected = !twoPlayerSelected;
                buttonPressed = true;
            }
            else if (!(Joystick.GetAxisPosition(0, Joystick.Axis.PovX) > 5) && !(Joystick.GetAxisPosition(0, Joystick.Axis.PovX) < -5))
            {
                buttonPressed = false;
            }
        }
        
        public EnumGameStates Update(GameTime gameTime)
        {
            SelectedSprite();
            flashShader.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds * 5f);
            fadeShade.SetParameter("time", (float)gameTime.TotalTime.TotalSeconds);

            if (Math.Sin(gameTime.TotalTime.Seconds) * 0.5 + 0.5 <= 0.01)
            {
                newPointsSet = true;
            }

            if(Joystick.IsButtonPressed(0,0))
            {
                if (onePlayerSelected)
                    InGame.PlayerNumbers = 1;
                else if (twoPlayerSelected)
                    InGame.PlayerNumbers = 2;
                return EnumGameStates.inGame;
            }

            if (Joystick.IsButtonPressed(0, 6))
            {
                return EnumGameStates.none;
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

            if (onePlayerSelected)
                window.Draw(OnePlayer, flashState);
            else
                window.Draw(OnePlayer);
            if (twoPlayerSelected)
                window.Draw(TwoPlayer, flashState);
            else
                window.Draw(TwoPlayer);
        }
    }
}
