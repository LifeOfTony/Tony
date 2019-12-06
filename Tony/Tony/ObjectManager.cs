using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static ObjectManager ObjectManagerinstance = null;
 
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
