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
        public Panel TextBox { get; private set; }


        public LevelUI(Texture2D texture)
        {
            LowerUI = new Panel(new Vector2(675, 250), PanelSkin.Fancy, Anchor.BottomCenter, new Vector2(0, 125));

            Panel frame = new Panel(new Vector2(175, 250), PanelSkin.None, Anchor.BottomCenter, new Vector2(-250, -25));
            LowerUI.AddChild(frame);

            TextBox = new Panel(new Vector2(500, 200), PanelSkin.Simple, Anchor.BottomCenter, new Vector2(65, 0));
            TextBox.Opacity = 70;

            LowerUI.AddChild(TextBox);

           

            Image img = new Image(texture);
            frame.AddChild(img);


            LowerUI.Visible = false;

            UserInterface.Active.AddEntity(LowerUI);
        }
    }
}
