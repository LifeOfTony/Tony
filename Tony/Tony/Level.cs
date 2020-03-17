using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tony
{
    public class Level
    {
        //static list of all objects in the current game.
        private List<GameObject> _Objects;

        //static list of all drawable objects.
        private List<Drawable> _Drawables;

        //static list of collidable objects.
        private List<GameObject> _Collidables;


        private List<GameObject> _Npcs;

        private int level;

        private int mapHeight;
        private int mapWidth;

        private Player _player;

        private EndObject _end;

        public Level(int level, int mapWidth, int mapHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.level = level;
            _Objects = new List<GameObject>();
            _Drawables = new List<Drawable>();
            _Collidables = new List<GameObject>();
            _Npcs = new List<GameObject>();
        }


        public int getLevel
        {
            get
            {
                return level;
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

        

        public Player Player
        {
            get
            {
                return _player;
            }
        }

        public EndObject End
        {
            get
            {
                return _end;
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
                _Drawables.Add(drawable);
            }

            // Adds to Collidables if collidable.
            if (newObject is Collider)
            {
                _Collidables.Add(newObject);
            }

            if (newObject is Player)
            {
                _player = (Player)newObject;
            }

            if (newObject is Npc)
            {
                _Npcs.Add(newObject);
            }

            if (newObject is EndObject)
            {
                _end = (EndObject)newObject;
            }
        }


        /// <summary>
        /// RemoveObject is called to remove a GameObject from the Objects list.
        /// </summary>
        public void RemoveObject(GameObject oldObject)
        {
            Objects.Remove(oldObject);

            // Adds to Drawables if drawable.
            if (oldObject is Drawable drawable)
            {
                Drawables.Remove(drawable);
            }

            // Adds to Collidables if collidable.
            if (oldObject is Collider)
            {
                Collidables.Remove(oldObject);
            }

            if (oldObject is Player)
            {
                _player = null; ;
            }

            if (oldObject is Npc)
            {
                Npcs.Remove(oldObject);
            }
        }

        public void setPaths()
        {
            foreach (Npc currentNpc in _Npcs)
            {
                currentNpc.setPath();
            }
        }

        public static implicit operator Level(int v)
        {
            throw new NotImplementedException();
        }
    }
}
