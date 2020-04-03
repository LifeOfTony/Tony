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
    class MainMenu : Entity
    {
        public Panel Menu { get; private set; }
        public Button MainToGame { get; private set; }
        public Button MainToQuit { get; private set; }
        public Button MainToLoad { get; private set; }

        public MainMenu(Texture2D texture)
        {
            Menu = new Panel(new Vector2(0, 0), PanelSkin.Simple, Anchor.Center);

            Panel frame = new Panel(new Vector2(800, 400), PanelSkin.None, Anchor.TopCenter, new Vector2(0, 80));
            Menu.AddChild(frame);

            Image img = new Image(texture);
            frame.AddChild(img);

            MainToGame = new Button("Start Game", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 335));
            Menu.AddChild(MainToGame);

            MainToLoad = new Button("Continue", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 260));
            Menu.AddChild(MainToLoad);


            MainToQuit = new Button("Quit", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 110));
            Menu.AddChild(MainToQuit);


            UserInterface.Active.AddEntity(Menu);
        }

    }
}
