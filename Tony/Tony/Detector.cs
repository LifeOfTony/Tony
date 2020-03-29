using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tony
{
    static class Detector
    {



        /// <summary>
        /// computes if the player is touching the left side of the object.
        /// </summary>
        /// <returns></returns>
        public static bool IsTouchingLeft(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize, float offsetValue)
        {
            float objectLeft = objectPosition.X;
            float objectRight = objectPosition.X + objectSize.X;
            float objectTop = objectPosition.Y;
            float objectBottom = objectPosition.Y + objectSize.Y;

            float playerLeft = playerPosition.X;
            float playerRight = playerPosition.X + playerSize.X;
            float playerTop = playerPosition.Y;
            float playerBottom = playerPosition.Y + playerSize.Y;

            float offset = offsetValue;


            return playerRight + offset >= objectLeft &&
                   playerLeft < objectLeft &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        /// <summary>
        /// computes if the player is touching the right side of the object.
        /// </summary>
        /// <returns></returns>
        public static bool IsTouchingRight(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize, float offsetValue)
        {
            float objectLeft = objectPosition.X;
            float objectRight = objectPosition.X + objectSize.X;
            float objectTop = objectPosition.Y;
            float objectBottom = objectPosition.Y + objectSize.Y;

            float playerLeft = playerPosition.X;
            float playerRight = playerPosition.X + playerSize.X;
            float playerTop = playerPosition.Y;
            float playerBottom = playerPosition.Y + playerSize.Y;

            float offset = offsetValue;

            return playerRight > objectRight &&
                   playerLeft - offset <= objectRight &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        /// <summary>
        /// computes if the player is touching the top of the object.
        /// </summary>
        /// <returns></returns>
        public static bool IsTouchingTop(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize, float offsetValue)
        {
            float objectLeft = objectPosition.X;
            float objectRight = objectPosition.X + objectSize.X;
            float objectTop = objectPosition.Y;
            float objectBottom = objectPosition.Y + objectSize.Y;

            float playerLeft = playerPosition.X;
            float playerRight = playerPosition.X + playerSize.X;
            float playerTop = playerPosition.Y;
            float playerBottom = playerPosition.Y + playerSize.Y;

            float offset = offsetValue;

            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom + offset >= objectTop &&
                   playerTop < objectTop;
        }

        /// <summary>
        /// computes if the player is touching the bottom of the object.
        /// </summary>
        /// <returns></returns>
        public static bool IsTouchingBottom(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize, float offsetValue)
        {
            float objectLeft = objectPosition.X;
            float objectRight = objectPosition.X + objectSize.X;
            float objectTop = objectPosition.Y;
            float objectBottom = objectPosition.Y + objectSize.Y;

            float playerLeft = playerPosition.X;
            float playerRight = playerPosition.X + playerSize.X;
            float playerTop = playerPosition.Y;
            float playerBottom = playerPosition.Y + playerSize.Y;

            float offset = offsetValue;

            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom > objectBottom &&
                   playerTop - offset <= objectBottom;
        }


        public static bool isTouching(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize, float offsetValue)
        {
            return (IsTouchingBottom(objectPosition, objectSize, playerPosition, playerSize, offsetValue) ||
                IsTouchingLeft(objectPosition, objectSize, playerPosition, playerSize, offsetValue) ||
                IsTouchingRight(objectPosition, objectSize, playerPosition, playerSize, offsetValue) ||
                IsTouchingTop(objectPosition, objectSize, playerPosition, playerSize, offsetValue));
        }
    }
}
