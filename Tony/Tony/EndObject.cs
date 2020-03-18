﻿using System;
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
        private string requirement;
        public EndObject(Vector2 position, Vector2 size, float depth, Texture2D texture, string requirement, string gives)
            : base(position, size, depth, texture, requirement, gives)
        {
            this.requirement = requirement;
        }

        
        public override void Interact()
        {
            foreach (Item i in ObjectManager .Instance .Items)
            {
                if (i.IsCollected() == true && i.Equals (requirement))
                {
                    if(ObjectManager .Instance .CurrentLevel .getLevel < ObjectManager.Instance.LevelSize() )
                    {
                        ObjectManager.Instance.CurrentLevel = ObjectManager.Instance.Levels[ObjectManager .Instance .CurrentLevel .getLevel +1];
                        Console.WriteLine("Level incremented!");
                    }
                }
                else
                {
                    Console.WriteLine("Can't find next level");
                }
            }
            

            
        }
        
    }
}
