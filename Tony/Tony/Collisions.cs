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
        //object edge data.
        private float objectLeft;
        private float objectRight;
        private float objectTop;
        private float objectBottom;

        //player edge data.
        private float playerLeft;
        private float playerRight;
        private float playerTop;
        private float playerBottom;

        private int moveSpeed;

        public Collisions(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize, int moveSpeed)
        {
            this.objectLeft = objectPosition.X;
            this.objectRight = objectPosition.X + objectSize.X;
            this.objectTop = objectPosition.Y;
            this.objectBottom = objectPosition.Y + objectSize.Y;

            this.playerLeft = playerPosition.X;
            this.playerRight = playerPosition.X + playerSize.X;
            this.playerTop = playerPosition.Y;
            this.playerBottom = playerPosition.Y + playerSize.Y;

            this.moveSpeed = moveSpeed;


        }

        //public bool DetectCollision(string keyPressed)
      //  {
        //    if(playerLeft + moveSpeed)
         //   return false;
       // }
    }
}
