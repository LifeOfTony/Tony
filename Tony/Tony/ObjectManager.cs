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


        //static list of all objects in the current game.
        private static List<GameObject> _Objects = new List<GameObject>();

        //static list of all drawable objects.
        private static List<Drawable> _Drawables = new List<Drawable>();

        //static list of collidable objects.
        private static List<GameObject> _Collidables = new List<GameObject>();
        
        //static list of all Items in the current game.
        private static List<Item> _Items = new List<Item>();

        private static List<GameObject> _Npcs = new List<GameObject>();

        private static ObjectManager ObjectManagerinstance = null;


        private static Player _player;

        private static float mentalState = 100;
        private static float countDuration = 3f;
        private static float currentTime = 0f;

        private static int mapHeight;
        private static int mapWidth;


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


        public List<GameObject> Npcs
        {
            get
            {
                return _Npcs;
            }
        }



        public List<GameObject> Objects
        {
            get
            {
                return _Objects;
            }
        }

        public List<Drawable> Drawables
        {
            get
            {
                return _Drawables;
            }
        }

        public List<GameObject> Collidables
        {
            get
            {
                return _Collidables;
            }
        }

        public List<Item> Items
        {
            get
            {
                return _Items;
            }
        }

        public Player Player
        {
            get
            {
                return _player;
            }
        }

        public float MentalState
        {
            get
            {
                return mentalState;
            }
        }

        public int MapWidth
        {
            get
            {
                return mapWidth;
            }
            set
            {
                mapWidth = value;
            }

        }

        public int MapHeight
        {
            get
            {
                return mapHeight;
            }
            set
            {
                mapHeight = value;
            }
        }


        /// <summary>
        /// AddObject is called to add a new GameObject to the Objects list.
        /// </summary>
        ///
        public void AddObject(GameObject newObject)
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
                _player = (Player)newObject;
            }

            if (newObject is Npc)
            {
                Npcs.Add(newObject);
            }
        }

        public void MentalDecay(GameTime gameTime)
        {
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

            if (currentTime >= countDuration)
            {
                currentTime -= countDuration;
                mentalState--;
            }
        }






        /// <summary>
        /// RemoveObject is called to remove a GameObject from the Objects list.
        /// </summary>
        public void RemoveObject(GameObject oldObject)
        {
            Objects.Remove(oldObject);
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
