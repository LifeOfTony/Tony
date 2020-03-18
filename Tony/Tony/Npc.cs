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
        public Npc(Vector2 position, Vector2 size, float depth, Texture2D texture, string route, bool actor,
            bool basicMove = false, string requirement = "", string gives = "") :
            base(position, size, depth, texture, requirement, gives)
        {
            this.route = route;
            FindDestination(route);
            move = false;
            this.actor = actor;
            this.basicMove = basicMove;
        }

        public Npc(Vector2 position, Vector2 size, float depth, Texture2D texture, string route,
            bool basicMove = false, string requirement = "", string gives = "")
            : this(position, size, depth, texture, route, false, basicMove, requirement, gives)
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
            GameManager.textOutput = "";
            GameManager.textOutput += "Basic Interact \n\r";
        }

        public override void GiverInteract()
        {
            // finds the correct item and sets it to collected.
            foreach (Item currentItem in ObjectManager.Instance.Items)
            {
                // text feedback is given when the item is gained.
                if (currentItem.GetName().Equals(gives))
                {
                    currentItem.Collect();
                    move = true;
                    GameManager.textOutput = "";
                    GameManager.textOutput += "gained " + gives + "\n\r";
                }
            }
        }


        public bool getActor()
        {
            return actor;
        }



    }

   
}
