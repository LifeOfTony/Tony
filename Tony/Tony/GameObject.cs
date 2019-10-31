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

        private String name;
        private Vector2 position;
        private float depth;
        private Texture2D texture;

        public GameObject(String name, Vector2 position, float depth, Texture2D texture)
        {
            this.name = name;
            this.position = position;
            this.depth = depth;
            this.texture = texture;

        }
    }
}
