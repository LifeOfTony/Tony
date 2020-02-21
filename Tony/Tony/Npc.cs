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

        /// <summary>
        /// An Npc is a moving interactable object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="collidable"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public Npc(Vector2 position, Vector2 size, bool complex, string requirement, string gives, string basic, string route, float depth, Texture2D texture) :
            base(position, size, complex, requirement, gives, basic, depth, texture)
        {
            this.route = route;
            FindDestination(route);
            move = false;
        }

        public void FindDestination(string route)
        {
            string[] coordinates = route.Split(',');
            destination = new Vector2(Int32.Parse(coordinates[0]), Int32.Parse(coordinates[1]));
        }

        public void Move()
        {
            if (move == true)
            {
                path = Pathfinder.FindPath(this.position, destination);
                if (path.Any())
                {
                    this.position = path.Dequeue();
                }
                else move = false;
                
            }
        }

        public override void ComplexInteract()
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

        public override void BasicInteract()
        {
            move = true;
            GameManager.textOutput = "";
            GameManager.textOutput += basic;
        }

    }

   
}
