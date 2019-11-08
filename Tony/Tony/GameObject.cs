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

        public GameObject(Vector2 position, Vector2 size, bool collidable, float rotation, Vector2 rotationOrigin)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;
            this.rotationOrigin = rotationOrigin;
        }

        public GameObject(Vector2 position, Vector2 size, bool collidable) : this(position, size, collidable, 0, new Vector2(0))
        {

        }

        public bool getCollidable()
        {
            return this.collidable;
        }
 
        
    }
}
