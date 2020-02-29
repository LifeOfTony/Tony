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
    static class Controller
    {


        public static void Initialize(ContentManager content)
        {

            View.Initialize(content);

        }


        public static void SwitchState( /* state */)
        {
            //switch the state between main-menu, pause menu, and playing state.
        }


        /* Button handler for the menus */

/*
        public static void ProcessButtons()
        {
            mainMenu.MainToGame.OnClick = (Entity button) =>
            {
                gameState = GameState.playing;
                ShowMainMenu();
            };

            mainMenu.MainToLevels.OnClick = (Entity button) => { ShowLevels(); };

            mainMenu.MainToQuit.OnClick = (Entity button) => Exit();

            mainMenu.LevelSetOne.OnClick = (Entity button) =>
            {
                level = 0;
                HideLevels();
            };

            mainMenu.LevelSetTwo.OnClick = (Entity button) =>
            {
                level = 1;
                HideLevels();
            };
        }
        */


        public static void Update(GameTime gameTime)
        {
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
