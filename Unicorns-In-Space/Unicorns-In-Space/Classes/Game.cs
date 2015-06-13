﻿using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorns_In_Space
{
    class Game : AbstractGame
    {
        public static uint WindowWidth = 1280;
        public static uint WindowHeight = 720;
        
        EnumGameStates currentGameState = EnumGameStates.mainMenu;
        EnumGameStates prevGameState;

        GameStates gameState;

        public Game() : base(WindowWidth, WindowHeight, "Project: Unicorns in Space") { }

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
                default:
                    throw new NotFiniteNumberException();
            }

            gameState.LoadContent();
            gameState.Initialize();
            prevGameState = currentGameState;
        }
    }
}
