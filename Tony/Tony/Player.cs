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
        private int age;

        public Player(Vector2 position, Vector2 size, float rotation, Vector2 rotationOrigin, int age) :
            base(position, size, rotation, rotationOrigin)
        {
            age = 3;
        }

        public void move()
        {

        }
    }
}
