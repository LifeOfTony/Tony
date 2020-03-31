using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tony
{
    public class Npc : InteractableObject
    {
        private string route;
        private bool move;
        private Vector2 destination;
        private Queue<Vector2> path;

        private bool actor;
        private string[] routes;

        /// <summary>
        /// An Npc is a moving interactable object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="collidable"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string routes, string name, bool actor,
            string requirement = "", string gives = "") :
            base(position, size, texture, baseDepth, name, requirement, gives)
        {
            SplitRoutes(routes);
            this.route = this.routes[0];
            FindDestination(route);
            move = false;
            this.actor = actor;

        }

        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string route, string name,
            string requirement = "", string gives = "")
            : this(position, size, texture, baseDepth, route, name, false, requirement, gives)
        {

        }


        #region RouteStuff

        public void SplitRoutes(string allRoutes)
        {
            routes = allRoutes.Split(':');
        }
       
        public void FindDestination(string route)
        {
            string[] coordinates = route.Split(',');
            destination = new Vector2(Int32.Parse(coordinates[0]), Int32.Parse(coordinates[1]));
        }

        public void setPath()
        {
            path = Pathfinder.FindPath(this.position, destination);
        }

        public void Move()
        {
            if (move == true)
            {
                
                if (path.Any())
                {
                    this.position = path.Dequeue();
                }
                else move = false;
                
            }
        }

        #endregion






        public override void Interact()
        {
            base.Interact();
        }




        public override void TakerInteract()
        {
            // checks to see if the player has got the required item to trigger the interaction.
            foreach (Item currentItem in ObjectManager.Items)
            {
                // if an item is used, text feedback is given.
                if (currentItem.GetName().Equals(requirement))
                {
                    if (currentItem.IsCollected())
                    {
                        move = true;
                        Controller.DisplayText(complex);
                        GiverInteract();
                    }
                    else
                    {
                        Controller.DisplayText(basic);
                    }
                }
            }
        }


        public override void BasicInteract()
        {

            move = true;
            Controller.DisplayText(basic);
            if (gives != null) GiverInteract();
        }

        public override void GiverInteract()
        {
            // finds the correct item and sets it to collected.
            foreach (Item currentItem in ObjectManager.Items)
            {
                // text feedback is given when the item is gained.
                if (currentItem.GetName().Equals(gives))
                {
                    currentItem.Collect();
                }
            }
        }


        public bool getActor()
        {
            return actor;
        }



    }

   
}
