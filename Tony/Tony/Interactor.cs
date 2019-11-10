using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    /// <summary>
    /// This class will be intergrated with collision logic eventually.
    /// The Interactor finds out whether the player is within range of an object to interact with.
    /// </summary>
    class Interactor
    {
        private float objectLeft;
        private float objectRight;
        private float objectTop;
        private float objectBottom;

        private float playerLeft;
        private float playerRight;
        private float playerTop;
        private float playerBottom;

        /// <summary>
        /// The interactor takes the player and an object and computes the interaction logic between them.
        /// </summary>
        /// <param name="objectPosition"></param>
        /// <param name="objectSize"></param>
        /// <param name="playerPosition"></param>
        /// <param name="playerSize"></param>
        public Interactor(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize)
        {
            // location data is created to simplify the logic.
            this.objectLeft = objectPosition.X;
            this.objectRight = objectPosition.X + objectSize.X;
            this.objectTop = objectPosition.Y;
            this.objectBottom = objectPosition.Y + objectSize.Y;

            this.playerLeft = playerPosition.X;
            this.playerRight = playerPosition.X + playerSize.X;
            this.playerTop = playerPosition.Y;
            this.playerBottom = playerPosition.Y + playerSize.Y;
        }

        /// <summary>
        /// computes if the player is within range of the left side of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingLeft()
        {
            return playerRight + 1 >= objectLeft &&
                   playerLeft < objectLeft &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        /// <summary>
        /// computes if the player is within range of the right side of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingRight()
        {
            return playerRight > objectRight &&
                   playerLeft - 1 <= objectRight &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        /// <summary>
        /// computes if the player is within range of the top of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingTop()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom + 1 >= objectTop &&
                   playerTop < objectTop;
        }

        /// <summary>
        /// computes if the player is within range of the bottom of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingBottom()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom > objectBottom &&
                   playerTop - 1 <= objectBottom;
        }
    }
}
