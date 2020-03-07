using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    public class EndObject : InteractableObject
    {
        public EndObject(Vector2 position, Vector2 size, bool complex, string requirement, string gives, string basic, float depth, Texture2D texture)
            : base(position, size, complex, requirement, gives, basic, depth, texture)
        {

        }

        public override void ComplexInteract()
        {
            // finds the correct item and sets it to collected.
            foreach (Item currentItem in ObjectManager.Instance.Items)
            {
                // text feedback is given when the item is gained.
                if (currentItem.GetName().Equals(gives))
                {
                    currentItem.Collect();
                    GameManager.textOutput += "gained " + gives + "\n\r";
                    //GameManager.setMainMenuState();
                }
            }
        }
    }
}
