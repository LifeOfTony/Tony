using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Npc : InteractableObject
    {


        private string route;
        private bool move;
        private Vector2 destination;

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

        //public Vector2 Move()
       // {

       // }
    }

   
}
