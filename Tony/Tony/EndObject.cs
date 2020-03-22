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

        public EndObject(Vector2 position, Vector2 size, float depth, Texture2D texture, string requirement, string gives)
            : base(position, size, depth, texture, requirement, gives)
        {

        }

        
        public override void Interact()
        {
            foreach (Item i in ObjectManager .Instance .Items)
            {
                if (i.IsCollected() == true && i.GetName().Equals (requirement))
                {
                    if(ObjectManager .Instance .CurrentLevel.level < ObjectManager.Instance.LevelSize() )
                    {
                        ObjectManager.Instance.CurrentLevel = ObjectManager.Instance.Levels.Find
                            (x => x.level == (ObjectManager.Instance.CurrentLevel.level + 1));

                        Pathfinder.CreateGrid(ObjectManager.Instance.CurrentLevel);
                        ObjectManager.Instance.CurrentLevel.setPaths();
                    }
                }
            }
            

            
        }
        
    }
}
