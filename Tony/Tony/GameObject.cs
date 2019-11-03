﻿using System;
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

        public GameObject(Vector2 position, Vector2 size, float rotation, Vector2 rotationOrigin)
        {
            this.position = position;
            this.size = size;
            this.rotation = rotation;
            this.rotationOrigin = rotationOrigin;
        }

        public GameObject(Vector2 position, Vector2 size) : this(position, size, 0, new Vector2(0))
        {

        }
 
        
    }
}
