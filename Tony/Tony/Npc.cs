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
        private string texturePath;
        private bool textureNum;

        private Vector2[] destinations;
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
        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string routes, string name, string filePath, bool actor,
            string requirement = "", string gives = "") :
            base(position, size, texture, baseDepth, name, requirement, gives)
        {
            if(routes.Contains(':'))
            {
                string[] endPoints = SplitRoutes(routes);
                destinations = new Vector2[endPoints.Count()];
                for(int i = 0; i < endPoints.Length; i++)
                {
                    string route = endPoints[i];
                    Vector2 currentRoute = FindDestination(route);
                    this.destinations[i] = (currentRoute);

                }
                move = true;
            }  
            else
            {
                Vector2 route = FindDestination(routes);
                this.destinations = new Vector2[1];
                this.destinations[0] = route;
                move = false;
            }

            texturePath = filePath;
            this.actor = actor;
            textureNum = true;

        }


        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string route, string name, string filePath,
            string requirement = "", string gives = "")
            : this(position, size, texture, baseDepth, route, name, filePath, false, requirement, gives)
        {

        }


        #region RouteStuff

        public string[] SplitRoutes(string allRoutes)
        {
            string[] routes = allRoutes.Split(':');
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

            for(int i = 0; i < destinations.Length; i++)
            {
                Vector2 start;
                Vector2 end;
                if (i == 0)
                {
                    start = position;
                    end = destinations[i];
                }
                else
                {
                    start = destinations[i - 1];
                    end = destinations[i];
                }
                Queue<Vector2> path = Pathfinder.FindPath(start, end);
                paths.Enqueue(path);
            }
           
        }


        public void Move()
        {
            if (move == true)
            {
                if (currentPath != null && currentPath.Any())
                {
                    Vector2 OldPosition = this.position;
                    this.position = currentPath.Dequeue();
                    this.texture = Animation.AnimateMoving(OldPosition, this.position, textureNum, texturePath);
                    textureNum = !textureNum;
                }
                else if (paths.Any())
                {
                    currentPath = paths.Dequeue();
                    if (paths.Any())
                    {
                        Queue<Vector2> resetPath = new Queue<Vector2>(currentPath);
                        paths.Enqueue(resetPath);
                    }
                    Vector2 OldPosition = this.position;
                    this.position = currentPath.Dequeue();
                    this.texture = Animation.AnimateMoving(OldPosition, this.position, textureNum, texturePath);
                    textureNum = !textureNum;
                }
                else move = false;

            }
            else
            {
                this.texture = Animation.AnimateIdle(textureNum, texturePath);
                textureNum = !textureNum;
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
                        if (gives != null) GiverInteract();
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
