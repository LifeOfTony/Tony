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

        static View()
        {

        }


        static void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }


        static void Draw(SpriteBatch spriteBatch)
        {
            UserInterface.Active.Draw(spriteBatch);
        }


        static void MainMenu()
        {

        }

        static void CreateTextBox()
        {

        }
    }
}
