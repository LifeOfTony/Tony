using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public delegate void InteractHandler();

namespace Tony
{
    public class InteractableObject : Sprite, Interactable
    {
        protected string basic;
        protected string complex;
        protected string requirement;
        protected string gives;
        public string name { get; private set; }
        public InteractHandler InteractType = null;

        /// <summary>
        /// InteractableObjects are any object that can be interacted with by the player.
        /// this includes: objects in the world, events, npcs, etc.
        /// all interactables have a requirement and give/trigger something.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public InteractableObject(Vector2 position, Vector2 size, float depth, Texture2D texture, string name, string requirement = null, string gives = null) :
            base(position, size, depth, texture)
        {
            this.name = name;
            ScriptReader.ReadScript(name);
            this.basic = ScriptReader.basic;
            this.complex = ScriptReader.complex;
            this.requirement = requirement;
            this.gives = gives;
            AssignType();

        }


        private void AssignType()
        {
            if(requirement != null)
            {
                InteractType = new InteractHandler(TakerInteract);
            }
            else if (gives != null)
            {
                InteractType = new InteractHandler(GiverInteract);
            }
            else
            {
                InteractType = new InteractHandler(BasicInteract);
            }
        }


     
        public virtual void Interact()
        {
            InteractType(); 
        }


        public void TakerInteract()
        {
            // checks to see if the player has got the required item to trigger the interaction.
            foreach (Item currentItem in ObjectManager.Items)
            {
                // if an item is used, text feedback is given.
                if (currentItem.GetName().Equals(requirement))
                {
                    if (currentItem.IsCollected())
                    {
                        GiverInteract();
                    }
                    else
                    {
                        BasicInteract();
                    }
                }
                
            }
        }

        public virtual void BasicInteract()
        {
            Controller.DisplayText(basic);
        }

        public virtual void GiverInteract()
        {
            // finds the correct item and sets it to collected.
            foreach (Item currentItem in ObjectManager.Items)
            {
                // text feedback is given when the item is gained.
                if (currentItem.GetName().Equals(gives))
                {
                    currentItem.Collect();
                    Controller.DisplayText(complex);
                }
            }
        }

    }
}
