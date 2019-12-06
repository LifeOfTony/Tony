using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class InteractableObject : Sprite, Interactable
    {
        private string requirement;
        private string gives;

        /// <summary>
        /// InteractableObjects are any object that can be interacted with by the player.
        /// this includes: objects in the world, events, npcs, etc.
        /// all interactables have a requirement and give/trigger something.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public InteractableObject(Vector2 position, Vector2 size, string requirement, string gives, float depth, Texture2D texture) :
            base(position, size, depth, texture)
        {
            this.requirement = requirement;
            this.gives = gives;
        }

        /// <summary>
        /// The Interact method handles when the player triggers this object in game.
        /// </summary>
        public void Interact()
        {
            // an object can have no requirement, in which case the interaction moves on.
            if(requirement.Equals("none"))
            {
                Collect();
            }
            else
            {
                // checks to see if the player has got the required item to trigger the interaction.
                foreach (Item currentItem in ObjectManager.Instance.Items)
                {
                    // if an item is used, text feedback is given.
                    if (currentItem.GetName().Equals(requirement) && currentItem.IsCollected())
                    {
                        GameManager.textOutput += "used " + requirement + "\n\r";
                        Collect();
                    }
                }
            }
        }

        // The Collect method collects the item given by this object.
        // this method will become redundant once more complex interactions are created.
        public void Collect()
        {
            // finds the correct item and sets it to collected.
            foreach (Item currentItem in ObjectManager.Instance.Items)
            {
                // text feedback is given when the item is gained.
                if (currentItem.GetName().Equals(gives))
                {
                    currentItem.Collect();
                    GameManager.textOutput += "gained " + gives + "\n\r";
                }
            }
        }
    }
}
