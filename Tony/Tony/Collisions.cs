using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Collisions
    {
        private float objectLeft;
        private float objectRight;
        private float objectTop;
        private float objectBottom;

        private float playerLeft;
        private float playerRight;
        private float playerTop;
        private float playerBottom;

        public Collisions(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize)
        {
            this.objectLeft = objectPosition.X;
            this.objectRight = objectPosition.X + objectSize.X;
            this.objectTop = objectPosition.Y;
            this.objectBottom = objectPosition.Y + objectSize.Y;

            this.playerLeft = playerPosition.X;
            this.playerRight = playerPosition.X + playerSize.X;
            this.playerTop = playerPosition.Y;
            this.playerBottom = playerPosition.Y + playerSize.Y;

        }
        public bool IsTouchingLeft()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectLeft &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        public bool IsTouchingRight()
        {
            return playerRight > objectRight &&
                   playerLeft < objectRight &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        public bool IsTouchingTop()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom > objectTop &&
                   playerTop < objectTop;
        }
        public bool IsTouchingBottom()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom > objectBottom &&
                   playerTop < objectBottom;
        }
    }
}
