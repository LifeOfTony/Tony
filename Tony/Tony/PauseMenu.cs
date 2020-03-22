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
    class PauseMenu : Entity
    {
        public Panel Menu { get; private set; }
        public Button PausetoGame { get; private set; }
        public Button PausetoMain { get; private set; }
        public Button PausetoQuit { get; private set; }
        public Button PausetoLoad { get; private set; }

        public PauseMenu()
        {
            Menu = new Panel(new Vector2(350, 325), PanelSkin.Simple, Anchor.Center);

            PausetoGame = new Button("Resume", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 225));
            Menu.AddChild(PausetoGame);

            PausetoLoad = new Button("Continue", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 150));
            Menu.AddChild(PausetoLoad);

            PausetoMain = new Button("MainMenu", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 75));
            Menu.AddChild(PausetoMain);

            PausetoQuit = new Button("Quit", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 0));
            Menu.AddChild(PausetoQuit);

            UserInterface.Active.AddEntity(Menu);
        }

    }
}
