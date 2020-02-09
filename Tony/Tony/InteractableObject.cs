using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    public class InteractableObject : Sprite, Interactable
    {
        protected string requirement;
        protected string gives;
        protected string basic;
        protected bool complex;

        /// <summary>
        /// InteractableObjects are any object that can be interacted with by the player.
        /// this includes: objects in the world, events, npcs, etc.
        /// all interactables have a requirement and give/trigger something.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public InteractableObject(Vector2 position, Vector2 size, bool complex, string requirement, string gives, string basic, float depth, Texture2D texture) :
            base(position, size, depth, texture)
        {
            this.requirement = requirement;
            this.gives = gives;
            this.basic = basic;
            this.complex = complex;
        }


        /// <summary>
        /// The Interact method handles when the player triggers this object in game.
        /// </summary>
        public void Interact()
        {
            if(complex == true)
            {
                if(requirement.Equals("none"))
                {
                   ComplexInteract();
                }
                else
                {
                    // checks to see if the player has got the required item to trigger the interaction.
                    foreach (Item currentItem in ObjectManager.Instance.Items)
                    {
                        // if an item is used, text feedback is given.
                        if (currentItem.GetName().Equals(requirement) && currentItem.IsCollected())
                        {
                            GameManager.textOutput = "";
                            GameManager.textOutput += "used " + requirement + "\n\r";
                            //Collect();
                            ComplexInteract();
                        }
                    }
                }

            }
            else
            {
                BasicInteract();
            }
        }


        public virtual void ComplexInteract()
        {
            // finds the correct item and sets it to collected.
            foreach (Item currentItem in ObjectManager.Instance.Items)
            {
                // text feedback is given when the item is gained.
                if (currentItem.GetName().Equals(gives))
                {
                    currentItem.Collect();
                    GameManager.textOutput = "";
                    GameManager.textOutput += "gained " + gives + "\n\r";
                }
            }
        }

        public virtual void BasicInteract()
        {
            GameManager.textOutput = "";
            GameManager.textOutput += (basic + "\n\r");
        }
    }
}
