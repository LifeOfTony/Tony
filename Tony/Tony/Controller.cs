﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Tony
{
    public static class Controller
    {
       public enum GameState { mainmenu, playing, paused, gameOver, quit}
       public static GameState gameState;
       public static bool exit = false;
       private static ContentManager Content;


        public static void Initialize(ContentManager content)
        {
            Content = content;
            View.Initialize(Content);
            gameState = GameState.mainmenu;
        }

        public static void SwitchState()
        {
            if (gameState == GameState.playing)
            {
                View.ShowLevelUI();

                View.HideMainMenu();
                View.HidePauseMenu();
                View.HideGameOver();
            }
            else if (gameState == GameState.mainmenu)
            {
                View.ShowMainMenu();

                View.HideLevelUI();
                View.HidePauseMenu();
                View.HideGameOver();
            }
            else if (gameState == GameState.paused)
            {
                View.ShowPauseMenu();

                View.HideMainMenu();
                View.HideLevelUI();
                View.HideGameOver();
            }
            else if (gameState == GameState.gameOver)
            {
                View.ShowGameOver();

                View.HideMainMenu();
                View.HideLevelUI();
                View.HidePauseMenu();
            }
        }


        /* Button handler for the menus */
        public static void ProcessButtons()
        {
            View.mainMenu.MainToGame.OnClick = (Entity button) =>
            {
                ObjectManager.SetLevels(Content);
                gameState = GameState.playing;
            };

            View.mainMenu.MainToLoad.OnClick = (Entity button) =>
            {
                SaveNLoad loadGame = new SaveNLoad ();
                loadGame.read();
                Pathfinder.CreateGrid(ObjectManager.CurrentLevel);
                ObjectManager.CurrentLevel.setPaths();
                gameState = GameState.playing;
            };

            View.mainMenu.MainToLevels.OnClick = (Entity button) => { View.ShowLevels(); };

            View.mainMenu.MainToQuit.OnClick = (Entity button) => exit = true;

            View.gameOver.OverToMain.OnClick = (Entity button) => gameState = GameState.mainmenu;

            View.gameOver.OverToExit.OnClick = (Entity button) => exit = true;


            View.mainMenu.LevelSetOne.OnClick = (Entity button) =>
            {
                Level selectedLevel = ObjectManager.Levels.Find(x => x.level == 0);
                Pathfinder.CreateGrid(selectedLevel);
                selectedLevel.setPaths();
                ObjectManager.CurrentLevel = selectedLevel;
                ObjectManager.ResetMentalState();
                gameState = GameState.playing;
            };

            View.mainMenu.LevelSetTwo.OnClick = (Entity button) =>
            {
                Level selectedLevel = ObjectManager.Levels.Find(x => x.level == 1);
                Pathfinder.CreateGrid(selectedLevel);
                selectedLevel.setPaths();
                ObjectManager.CurrentLevel = selectedLevel;
                ObjectManager.ResetMentalState();
                gameState = GameState.playing;
            };

            View.pauseMenu.PausetoGame.OnClick = (Entity button) => gameState = GameState.playing;

            View.pauseMenu.PausetoMain.OnClick = (Entity button) =>
            {
                SaveNLoad saveGame = new SaveNLoad();
                saveGame.save();
                gameState = GameState.mainmenu;
            };
            View.pauseMenu.PausetoQuit.OnClick = (Entity button) => exit = true;

        }


        public static void DisplayText(string text)
        {
            View.levelUI.text.Text = text;
        }

        public static void ClearText()
        {
            View.levelUI.text.Text = "";
        }


        public static void Update(GameTime gameTime)
        {
            ProcessButtons();
            SwitchState();
            // GeonBit.UIL update UI manager
            UserInterface.Active.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            // GeonBit.UI: draw UI using the spriteBatch you created above
            UserInterface.Active.Draw(spriteBatch);

        }

    }
   
}
