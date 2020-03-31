using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Tony
{
    static class View
    {

        public static MainMenu mainMenu { get; private set; }
        public static PauseMenu pauseMenu { get; private set; }
        public static GameOver gameOver { get; private set; }
        public static LevelUI levelUI { get; private set; }


        public static void Initialize(ContentManager content)
        {
            //load textures for the big lantern and textbox.
            //create mainmenu.

            Texture2D logo = content.Load<Texture2D>("tonylogo-12");
            Texture2D TextBox = content.Load<Texture2D>(@"UI\TextBox");
            int count = 1;
            bool foundLast = false;
            List<Texture2D> Lanterns = new List<Texture2D>();
            do
            {
                if (File.Exists(@"Content\UI\BigLantern\Lantern" + count + ".xnb"))
                {
                    Texture2D currentLantern = content.Load<Texture2D>(@"UI\BigLantern\Lantern" + count);
                    Lanterns.Add(currentLantern);
                    count++;
                }
                else foundLast = true;
            } while (foundLast == false);


            mainMenu = new MainMenu(logo);
            levelUI = new LevelUI(TextBox, Lanterns);
            pauseMenu = new PauseMenu();
            gameOver = new GameOver(Lanterns.Last());

            levelUI.Visible = false;
        }


        public static void Update(GameTime gameTime)
        {
            levelUI.UpdateLantern(ObjectManager.mentalState);
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

        public static void HidePauseMenu()
        {
            pauseMenu.Menu.Visible = false;
        }

        public static void ShowPauseMenu()
        {
            pauseMenu.Menu.Visible = true;
        }


        public static void HideGameOver()
        {
            gameOver.Menu.Visible = false;
        }

        public static void ShowGameOver()
        {
            gameOver.Menu.Visible = true;
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
