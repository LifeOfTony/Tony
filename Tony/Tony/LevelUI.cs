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

        public LevelUI(Texture2D texture, List<Texture2D> lanterns)
        {

            Lanterns = lanterns;


            LowerUI = new Panel(new Vector2(700, 250), PanelSkin.Fancy, Anchor.BottomCenter, new Vector2(0, 125));
            LowerUI.Opacity = 70;

            Panel frame = new Panel(new Vector2(175, 250), PanelSkin.None, Anchor.BottomCenter, new Vector2(-250, -25));
            LowerUI.AddChild(frame);

            Image img = new Image(Lanterns[0]);
            frame.AddChild(img);


            TextBox = new Image(texture, new Vector2(500, 200), ImageDrawMode.Stretch , Anchor.BottomCenter, new Vector2(frame.Size.X/2,0));

            LowerUI.AddChild(TextBox);

            text = new Paragraph();
            TextBox.AddChild(text);


            


            LowerUI.Visible = false;

            UserInterface.Active.AddEntity(LowerUI);
        }
    }
}
