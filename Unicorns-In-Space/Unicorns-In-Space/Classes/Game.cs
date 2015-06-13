using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Game : AbstractGame
    {
        public static uint WindowWidth = 1920;
        public static uint WindowHeight = 1000;

        public static HighscoreList Highscores { get; protected set; }

        EnumGameStates currentGameState = EnumGameStates.mainMenu;
        EnumGameStates prevGameState;

        GameStates gameState;

        public Game() : base(WindowWidth, WindowHeight, "Project: Unicorns in Space") { Highscores = new HighscoreList(); }

        public override void Draw(RenderWindow win)
        {
            gameState.Draw(win);
        }

        public override void Update(GameTime gameTime)
        {
            if(currentGameState != prevGameState)
            {
                HandleGameStates();
            }
            currentGameState = gameState.Update(gameTime);
        }

        public void HandleGameStates()
        {
            switch(currentGameState)
            {
                case EnumGameStates.none:
                    window.Close();
                    break;
                case EnumGameStates.mainMenu:
                    gameState = new MainMenu();
                    break;
                case EnumGameStates.inGame:
                    gameState = new InGame();
                    break;
                case EnumGameStates.gameOver:
                    gameState = new GameOver();
                    break;
                default:
                    throw new NotFiniteNumberException();
            }

            gameState.LoadContent();
            gameState.Initialize();
            prevGameState = currentGameState;
        }
    }
}
