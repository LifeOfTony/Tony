using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    public class Npc : InteractableObject
    {
        private string route;
        private bool move;
        private Vector2 destination;
        private Queue<Vector2> path;
        private bool basicMove;
        private bool actor;

        /// <summary>
        /// An Npc is a moving interactable object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="collidable"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string route, string name, bool actor,
            bool basicMove = false, string requirement = "", string gives = "") :
            base(position, size, texture, baseDepth, name, requirement, gives)
        {
            this.route = route;
            FindDestination(route);
            move = false;
            this.actor = actor;
            this.basicMove = basicMove;
        }

        public Npc(Vector2 position, Vector2 size, Texture2D texture, float baseDepth, string route, string name,
            bool basicMove = false, string requirement = "", string gives = "")
            : this(position, size, texture, baseDepth, route, name, false, basicMove, requirement, gives)
        {

        }




        #region RouteStuff

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



        public override void BasicInteract()
        {
            if (basicMove == true)
            {
                move = true;
            }
            Controller.DisplayText(basic);
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
                    move = true;
                    Controller.DisplayText(complex);
                }
            }
        }


        public bool getActor()
        {
            return actor;
        }



    }

   
}
