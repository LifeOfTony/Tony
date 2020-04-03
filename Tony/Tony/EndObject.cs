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

        public EndObject(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string requirement)
            : base(position, size, texture, baseDepth, "End", requirement)
        {

        }

        
        public override void Interact()
        {
            foreach (Item i in ObjectManager.Items)
            {
                if (i.IsCollected() == true && i.GetName().Equals (requirement))
                {

                    if(ObjectManager.currentLevel.level < ObjectManager.levels.Count() - 1 )
                    {
                        Controller.DisplayText(complex);
                        ObjectManager.currentLevel = ObjectManager.levels.Find
                            (x => x.level == (ObjectManager.currentLevel.level + 1));

                        Pathfinder.CreateGrid(ObjectManager.currentLevel);
                        ObjectManager.currentLevel.setPaths();
                    }
                    else
                    {
                        Controller.gameState = Controller.GameState.gameOver;
                    }
                }
                else
                {
                    Controller.DisplayText(basic);
                }
            }


        }
        
    }
}
