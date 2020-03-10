using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tony
{
    public sealed class ObjectManager
    {



        private static ObjectManager ObjectManagerinstance = null;

        private static List<Level> levels = new List<Level>();

        private static Level currentLevel;

        private static float mentalState = 100;

        private static float countDuration = 2f;

        private static float currentTime = 0f;

        //static list of all Items in the current game.
        private List<Item> _Items = new List<Item>();


        private ObjectManager()
        {
        }

        public static ObjectManager Instance
        {
            get
            {
                if (ObjectManagerinstance == null)
                {
                    ObjectManagerinstance = new ObjectManager();
                }
                return ObjectManagerinstance;
            }
        }


        public void AddLevel(Level newLevel)
        {
            levels.Add(newLevel);
        }



        public Level CurrentLevel
        {
            get
            {
                return currentLevel;
            }
            set
            {
                currentLevel = value;
            }
            
        }

        

        public float MentalState
        {
            get
            {
                return mentalState;
            }
        }

        public List<Item> Items
        {
            get
            {
                return _Items;
            }
        }




        public void MentalDecay(GameTime gameTime)
        {
            if( mentalState > 0)
            {
                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

                if (currentTime >= countDuration)
                {
                    currentTime -= countDuration;
                    mentalState--;
                }
            }
           
        }


        

        /// <summary>
        /// AddItem is called to add a new Item to the Items list.
        /// </summary>
        public void AddItem(Item newItem)
        {
            Items.Add(newItem);
        }

        /// <summary>
        /// RemoveItem is called to remove an Item from the Items list.
        /// </summary>
        public void RemoveItem(Item oldItem)
        {
            Items.Remove(oldItem);
        }


    }
}
