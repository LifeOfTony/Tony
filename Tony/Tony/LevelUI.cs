using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
// using GeonBit UI elements
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Tony
{
    class LevelUI : Entity
    {
        public Panel LowerUI { get; private set; }
        public Image TextBox { get; private set; }
        public Paragraph text { get; private set; }

        private List<Texture2D> Lanterns;

        private Image lantern;

        public LevelUI(Texture2D texture, List<Texture2D> lanterns)
        {

            Lanterns = lanterns;


            LowerUI = new Panel(new Vector2(700, 250), PanelSkin.Fancy, Anchor.BottomCenter, new Vector2(0, 125));
            LowerUI.Opacity = 70;

            Panel frame = new Panel(new Vector2(175, 250), PanelSkin.None, Anchor.BottomCenter, new Vector2(-250, -25));
            LowerUI.AddChild(frame);

            lantern = new Image(Lanterns[0]);
            frame.AddChild(lantern);


            TextBox = new Image(texture, new Vector2(500, 200), ImageDrawMode.Stretch , Anchor.BottomCenter, new Vector2(frame.Size.X/2,0));

            LowerUI.AddChild(TextBox);

            text = new Paragraph();
            TextBox.AddChild(text);


            


            LowerUI.Visible = false;

            UserInterface.Active.AddEntity(LowerUI);
        }


        public void UpdateLantern(float mentalState)
        {
            if (100 >= mentalState && mentalState >= 85)
            {

                lantern = new Image(Lanterns[0]);
            }
            else if (85 > mentalState && mentalState >= 70)
            {
                lantern = new Image(Lanterns[1]);
            }
            else if (70 > mentalState && mentalState >= 55)
            {
                lantern = new Image(Lanterns[2]);
            }
            else if (55 > mentalState && mentalState >= 40)
            {
                lantern = new Image(Lanterns[3]);
            }
            else if (40 > mentalState && mentalState >= 25)
            {
                lantern = new Image(Lanterns[4]);
            }
            else if (25 > mentalState && mentalState > 0)
            {
                lantern = new Image(Lanterns[5]);
            }
            else
            {
                lantern = new Image(Lanterns[6]);
            }

        }

    }
}
