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


        private List<Npc> _Npcs;

        private List<Event> _Events;


        private int level;

        private int mapHeight;
        private int mapWidth;
        private int tileHeight;
        private int tileWidth;

        private Player _player;

        private EndObject _end;

        public Level(int level, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.level = level;
            _Objects = new List<GameObject>();
            _Drawables = new List<Drawable>();
            _Collidables = new List<GameObject>();
            _Npcs = new List<Npc>();
            _Events = new List<Event>();
        }


        public int getLevel
        {
            get
            {
                return level;
            }
            
        }


        public List<Npc> Npcs
        {
            get
            {
                return _Npcs;
            }
        }

        public List<Event> Events
        {
            get
            {
                return _Events;
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
        }

        public int MapHeight
        {
            get
            {
                return mapHeight;
            }
        }


        public int TileWidth
        {
            get
            {
                return tileWidth;
            }

        }

        public int TileHeight
        {
            get
            {
                return tileHeight;
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
                _Npcs.Add((Npc)newObject);
            }



            if (newObject is EndObject)
            {
                _end = (EndObject)newObject;
            }

            if (newObject is Event)
            {
                _Events.Add((Event)newObject);
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
                Npcs.Remove((Npc)oldObject);
            }
        }

        public void setPaths()
        {
            foreach (Npc currentNpc in _Npcs)
            {
                currentNpc.setPath();
            }
        }


    }
}
