using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tony
{
    public class Npc : InteractableObject
    {
        private bool move;
        private bool actor;


        private Queue<Vector2> destinations = new Queue<Vector2>();
        private Queue<Queue<Vector2>> paths = new Queue<Queue<Vector2>>();

        private Queue<Vector2> currentPath;

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
            if(routes.Contains(':'))
            {
                Queue<string> endPoints = SplitRoutes(routes);
                while (endPoints.Any())
                {
                    string route = endPoints.Dequeue();
                    Vector2 currentRoute = FindDestination(route);
                    this.destinations.Enqueue(currentRoute);

                    /*
                    Queue<Vector2> currentPath = setPath(currentRoute);
                    paths.Enqueue(currentPath);
                    */

                }
                move = true;
            }  
            else
            {
                Vector2 route = FindDestination(routes);
                this.destinations.Enqueue(route);
                move = false;
            }

            this.actor = actor;

        }


        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string route, string name,
            string requirement = "", string gives = "")
            : this(position, size, texture, baseDepth, route, name, false, requirement, gives)
        {

        }


        #region RouteStuff

        public Queue<string> SplitRoutes(string allRoutes)
        {
            Queue<string> routes = new Queue<string>(allRoutes.Split(':'));
            return routes;
        }
       

        public Vector2 FindDestination(string route)
        {
            string[] coordinates = route.Split(',');
            Vector2 destination = new Vector2(Int32.Parse(coordinates[0]), Int32.Parse(coordinates[1]));
            return destination;
        }


        public void setPath()
        {

            while(destinations.Any())
            {
                Queue<Vector2> path = Pathfinder.FindPath(position, destinations.Dequeue());
                paths.Enqueue(path);
            }
            
        }

        /*
         * Setpaths currently always sets from the current position of the npc
         * 
         * The Npc is currently not actually looping.
         * 
         * */



        public void Move()
        {
            if (move == true)
            {

                if (currentPath != null && currentPath.Any())
                {
                    this.position = currentPath.Dequeue();
                }
                else
                {
                    currentPath = paths.Dequeue();
                    if (paths.Any())
                    {
                        paths.Enqueue(currentPath);
                    }
                    else move = false;
                }
                
            }
        }

        #endregion




        #region Interactions

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

        #endregion

        public bool getActor()
        {
            return actor;
        }



    }

   
}
