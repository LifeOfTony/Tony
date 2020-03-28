using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tony
{
    public static class ObjectManager
    {



   

        private static List<Level> levels = new List<Level>();

        private static Level currentLevel;

        private static float mentalState;

        private static float countDuration = 2f;

        private static float currentTime = 0f;

        //static list of all Items in the current game.
        private static List<Item> _Items = new List<Item>();





        public static void AddLevel(Level newLevel)
        {
            levels.Add(newLevel);
        }



        public static Level CurrentLevel
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

        public static float MentalState
        {
            get
            {

                return mentalState;
            }
        }

        public static List<Item> Items
        {
            get
            {
                return _Items;
            }
        }

        public static void MentalDecay(GameTime gameTime)
        {
            if (Controller.gameState == Controller.GameState.playing)
            {
                if (mentalState > 0)
                {
                    currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

                    if (currentTime >= countDuration)
                    {
                        currentTime -= countDuration;
                        mentalState--;
                    }
                }
                else
                {
                    Controller.gameState = Controller.GameState.gameOver;
                }
            }



        }


        public static void ResetLevel()
        {
            currentLevel = levels.Find(x => x.level == 0);
            Pathfinder.CreateGrid(currentLevel);
            currentLevel.setPaths();
            ResetMentalState();

        }

        public static void ResetMentalState()
        {
            mentalState = 100;
        }





        /// <summary>
        /// AddItem is called to add a new Item to the Items list.
        /// </summary>
        public static void AddItem(Item newItem)
        {
            Items.Add(newItem);
        }

        /// <summary>
        /// RemoveItem is called to remove an Item from the Items list.
        /// </summary>
        public static void RemoveItem(Item oldItem)
        {
            Items.Remove(oldItem);
        }

        public static int LevelSize()
        {
            return levels.Count;
        }

        public static List<Level> Levels
        {
            get
            {
                return levels;
            }
        }

        public static float ModifyMentalState(Item item)
        {
            mentalState += item.GetModifier();
            return mentalState;
        }

        public static void setMentalState(float newMentalStalte)
            {
                    mentalState = newMentalStalte;
            }

        public static void setPosition (Vector2 position)
        {
            currentLevel.Player.setPosition(position);
        }

        public static void setCorrectedItem(Item correctedItem)
        {
            foreach(Item i in Items)
            {
                if (i.Equals(correctedItem))
                {
                    i.loadCollect ();
                }
            }
        }
    }
}
