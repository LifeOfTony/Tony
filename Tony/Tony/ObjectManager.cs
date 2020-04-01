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



   

        public static List<Level> levels { get; private set; } = new List<Level>();

        public static Level currentLevel;

        public static float mentalState { get; private set; }

        private static float countDuration = 1f;

        private static float currentTime  = 0f;

        //static list of all Items in the current game.
        public static List<Item> Items { get; private set; } = new List<Item>();





        public static void Update(GameTime gameTime)
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
                        foreach (Npc npc in currentLevel.Npcs)
                        {
                            npc.Move();
                        }
                    }

                    foreach (Event currentEvent in currentLevel.Events)
                    {
                        if (Input.InteractDetection(currentEvent, 0)) currentEvent.Interact();
                    }


                }
                else
                {
                    Controller.gameState = Controller.GameState.gameOver;
                }
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
            levels.Clear();


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
