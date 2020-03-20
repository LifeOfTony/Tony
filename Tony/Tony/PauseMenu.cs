﻿using Microsoft.Xna.Framework;
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
        public Button PausetoSettings { get; private set; }
        public Button PausetoQuit { get; private set; }

    public PauseMenu(Texture2D texture)
        {
            Menu = new Panel(new Vector2(0, 0), PanelSkin.Simple, Anchor.Center);

            Panel frame = new Panel(new Vector2(800, 400), PanelSkin.None, Anchor.TopCenter, new Vector2(0, 80));
            Menu.AddChild(frame);

            Image img = new Image(texture);
            frame.AddChild(img);

            PausetoGame = new Button("Resume to Game", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 260));
            Menu.AddChild(PausetoGame);

            PausetoMain = new Button("Main Menu", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 185));
            Menu.AddChild(PausetoMain);

            PausetoSettings = new Button("Settings", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 110));
            Menu.AddChild(PausetoSettings);

            PausetoQuit = new Button("Quit", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(400, 260));
            Menu.AddChild(PausetoQuit);

            UserInterface.Active.AddEntity(Menu);
        }

    }
}
