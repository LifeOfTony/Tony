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
        public Panel TextBox { get; private set; }

        public LevelUI()
        {
            TextBox = new Panel(new Vector2(500, 200), PanelSkin.Fancy, Anchor.BottomCenter, new Vector2(0, 200));
            TextBox.Visible = false;

            UserInterface.Active.AddEntity(TextBox);
        }
    }
}
