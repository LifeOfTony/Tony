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
    static class View
    {

        public static MainMenu mainMenu;

        static LevelUI levelUI;


        public static void Initialize(ContentManager content)
        {
            //load textures for the big lantern and textbox.
            //create mainmenu.

            Texture2D logo = content.Load<Texture2D>("tonylogo-12");
            Texture2D lantern = content.Load<Texture2D>("biglanternOne");
            mainMenu = new MainMenu(logo);
            levelUI = new LevelUI(lantern);
            levelUI.Visible = false;
        }


        static void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }

        static void Draw(SpriteBatch spriteBatch)
        {
            UserInterface.Active.Draw(spriteBatch);
        }

        public static void ShowLevels()
        {
            mainMenu.LevelSetOne.Visible = true;
            mainMenu.LevelSetTwo.Visible = true;
        }

        public static void HideLevels()
        {
            mainMenu.LevelSetOne.Visible = false;
            mainMenu.LevelSetTwo.Visible = false;
        }

        public static void HideMainMenu()
        {
            mainMenu.Menu.Visible = false;
        }

        public static void ShowMainMenu()
        {
            mainMenu.Menu.Visible = true;
        }

        public static void ShowLevelUI()
        {
            levelUI.LowerUI.Visible = true;
        }

        public static void HideLevelUI()
        {
            levelUI.LowerUI.Visible = false;
        }

        //creates the textbox element
        static void CreateTextBox()
        {

        }
    }
}
