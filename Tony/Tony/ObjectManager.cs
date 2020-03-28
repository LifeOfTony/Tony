using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

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


        public static void SetLevels(ContentManager Content)
        {
            //Get all Levels from the directory and store in the array.
            string[] filePaths = Directory.GetFiles(@"Content\Levels\", "*.tmx");
            //Adding Level to the ObjectManager.Instance.levels
            for (int i = 0; i < filePaths.Length; i++)
            {
                LevelReader iLevel = new LevelReader(@filePaths[i], Content);
                Level iNewLevel = iLevel.GetLevel();
                levels.Add(iNewLevel);
                if (iNewLevel.level == 0)
                {
                    currentLevel = iNewLevel;
                    Pathfinder.CreateGrid(currentLevel);
                    currentLevel.setPaths();
                    
                }
            }
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
