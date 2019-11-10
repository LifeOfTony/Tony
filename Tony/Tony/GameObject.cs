using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    abstract class GameObject
    {
        protected Vector2 position;
        protected Vector2 size;
        protected float rotation;
        protected Vector2 rotationOrigin;
        protected bool collidable;

        /// <summary>
        /// GameObjects are anything that has a location and size in the game.
        /// These are taken as parameters, along with a bool to represent whether
        /// the object has collision.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="collidable"></param>
        /// <param name="rotation"></param>
        /// <param name="rotationOrigin"></param>
        public GameObject(Vector2 position, Vector2 size, bool collidable, float rotation, Vector2 rotationOrigin)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;
            this.rotationOrigin = rotationOrigin;
            this.collidable = collidable;
        }

        // Constructor overrides are used for objects types that do not specify details.
        public GameObject(Vector2 position, Vector2 size, bool collidable) : this(position, size, collidable, 0, new Vector2(0))
        {

        }

        public GameObject(Vector2 position, Vector2 size) : this(position, size, false, 0, new Vector2(0))
        {

        }

        /// <summary>
        /// returns whether the object is collidable.
        /// </summary>
        /// <returns></returns>
        public bool getCollidable()
        {
            return this.collidable;
        }

        /// <summary>
        /// returns the position of the object.
        /// </summary>
        /// <returns></returns>
        public Vector2 getPosition()
        {
            return this.position;
        }

        /// <summary>
        /// returns the size of the object.
        /// </summary>
        /// <returns></returns>
        public Vector2 getSize()
        {
            return this.size;
        }
 
        
    }
}
