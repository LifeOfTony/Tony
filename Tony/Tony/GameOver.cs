using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// using GeonBit UI elements
using GeonBit.UI;
using GeonBit.UI.Entities;

namespace Tony
{
    class GameOver : Entity
    {
        public Panel Menu { get; private set; }
        public Button OverToMain { get; private set; }
        public Button OverToExit { get; private set; }

        public GameOver(Texture2D texture)
        {
            Menu = new Panel(new Vector2(0, 0), PanelSkin.Simple, Anchor.Center);

            Panel frame = new Panel(new Vector2(175, 250), PanelSkin.None, Anchor.Center, new Vector2(0, -40));
            Menu.AddChild(frame);

            Image img = new Image(texture);
            frame.AddChild(img);

            OverToMain = new Button("MainMenu", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 300));
            Menu.AddChild(OverToMain);

            OverToExit = new Button("Quit", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(300, 50), new Vector2(0, 225));
            Menu.AddChild(OverToExit);

            UserInterface.Active.AddEntity(Menu);
        }

    }
}
