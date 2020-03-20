using System;
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
       public enum GameState { mainmenu, playing, paused, pausemenu, gameOver }
       public static GameState gameState;
       public static bool exit = false;


        public static void Initialize(ContentManager content)
        {
            View.Initialize(content);
            gameState = GameState.mainmenu;
            gameState = GameState.pausemenu;
            gameState = GameState.gameOver;
        }

        public static void SwitchState()
        {
            if (gameState == GameState.playing)
            {
                View.HideMainMenu();
                View.ShowLevelUI();
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
            else if (gameState == GameState.pausemenu)
            {
                View.HideMainMenu();
                View.HideLevelUI();
                View.ShowPauseMenu();
                View.HideGameOver();
            }
            else if (gameState == GameState.gameOver)
            {
                View.HideMainMenu();
                View.HideLevelUI();
                View.HidePauseMenu();
                View.ShowGameOver();
            }
        }

        /* Button handler for the menus */


        public static void ProcessButtons()
        {
            View.mainMenu.MainToGame.OnClick = (Entity button) =>
            {
                gameState = GameState.playing;
            };

            View.mainMenu.MainToLevels.OnClick = (Entity button) => { View.ShowLevels(); };

            View.mainMenu.MainToQuit.OnClick = (Entity button) => exit = true;

            /*
            View.mainMenu.LevelSetOne.OnClick = (Entity button) =>
            {
                level = 0;
                HideLevels();
            };

            View.mainMenu.LevelSetTwo.OnClick = (Entity button) =>
            {
                level = 1;
                HideLevels();
            };
            */
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
