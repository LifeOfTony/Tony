using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Player : GameObject
    {
        private int age { get; set; }

        public Player(String name, Vector2 position, float depth, Texture2D texture) :
            base(name, position, depth, texture)
        {
            age = 3;
        }
        public void move()
        {

        }
    }
}
