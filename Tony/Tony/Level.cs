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
        public List<GameObject> Objects { get; private set; }

        //static list of all drawable objects.
        public List<Drawable> Drawables { get; private set; }



        //static list of collidable objects.
        public List<GameObject> Collidables { get; private set; }


        public List<Npc> Npcs { get; private set; }

        public List<Event> Events { get; private set; }


        public int level { get; private set; }

        public int mapHeight { get; private set; }
        public int mapWidth { get; private set; }
        public int tileHeight { get; private set; }
        public int tileWidth { get; private set; }

        public Player Player { get; private set; }

        public EndObject End { get; private set; }

        public Level(int level, int mapWidth, int mapHeight, int tileWidth, int tileHeight)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.level = level;
            Objects = new List<GameObject>();
            Drawables = new List<Drawable>();
            Collidables = new List<GameObject>();
            Npcs = new List<Npc>();
            Events = new List<Event>();
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
                Player = (Player)newObject;
            }

            if (newObject is Npc)
            {
                Npcs.Add((Npc)newObject);
            }

            if (newObject is EndObject)
            {
                End = (EndObject)newObject;
            }

            if (newObject is Event)
            {
                Events.Add((Event)newObject);
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
                Player = null; ;
            }

            if (oldObject is Npc)
            {
                Npcs.Remove((Npc)oldObject);
            }
        }

        public void setPaths()
        {
            foreach (Npc currentNpc in Npcs)
            {
                currentNpc.setPath();
            }
        }


    }
}
