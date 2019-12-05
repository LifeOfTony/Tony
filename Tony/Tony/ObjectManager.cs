using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tony
{
    static class ObjectManager
    {


        //static list of all objects in the current game.
        public static List<GameObject> Objects = new List<GameObject>();

        //static list of all drawable objects.
        public static List<Drawable> Drawables = new List<Drawable>();

        //static list of collidable objects.
        public static List<GameObject> Collidables = new List<GameObject>();
        
        //static list of all Items in the current game.
        public static List<Item> Items = new List<Item>();

        public static Player player;

        private static float mentalState;
        private static float countDuration = 3f;
        private static float currentTime = 0f;


        /// <summary>
        /// addObject is called to add a new GameObject to the Objects list.
        /// </summary>
        public static void addObject(GameObject newObject)
        {
            Objects.Add(newObject);
            

            // Adds to Drawables if drawable.
            if (newObject is Drawable drawable)
            {
                Drawables.Add(drawable);
            }

            // Adds to Collidables if collidable.
            if (newObject is Collider)
            {
                Collidables.Add(newObject);
            }

            if (newObject is Player)
            {
                player = (Player)newObject;
            }
        }


        public static void MentalDecay(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

            if (currentTime >= countDuration)
            {
                currentTime -= countDuration;
                mentalState--;
            }
        }



        /// <summary>
        /// removeObject is called to remove a GameObject from the Objects list.
        /// </summary>
        public static void removeObject(GameObject oldObject)
        {
            Objects.Remove(oldObject);
        }


        /// <summary>
        /// addItem is called to add a new Item to the Items list.
        /// </summary>
        public static void addItem(Item newItem)
        {
            Items.Add(newItem);
        }


        /// <summary>
        /// removeItem is called to remove an Item from the Items list.
        /// </summary>
        public static void removeItem(Item oldItem)
        {
            Items.Remove(oldItem);
        }



    }
}
