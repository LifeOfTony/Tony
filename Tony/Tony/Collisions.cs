﻿using System;
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

        /// <summary>
        /// The Collisions class is used to compute collision logic.
        /// It takes the player and an objects, as well as their sizess as parameters.
        /// </summary>
        /// <param name="objectPosition"></param>
        /// <param name="objectSize"></param>
        /// <param name="playerPosition"></param>
        /// <param name="playerSize"></param>
        public Collisions(Vector2 objectPosition, Vector2 objectSize, Vector2 playerPosition, Vector2 playerSize)
        {
            // location data is created to simplify collision logic.

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
        /// computes if the player is touching the left side of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingLeft()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectLeft &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        /// <summary>
        /// computes if the player is touching the right side of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingRight()
        {
            return playerRight > objectRight &&
                   playerLeft < objectRight &&
                   playerBottom > objectTop &&
                   playerTop < objectBottom;
        }

        /// <summary>
        /// computes if the player is touching the top of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingTop()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom > objectTop &&
                   playerTop < objectTop;
        }

        /// <summary>
        /// computes if the player is touching the bottom of the object.
        /// </summary>
        /// <returns></returns>
        public bool IsTouchingBottom()
        {
            return playerRight > objectLeft &&
                   playerLeft < objectRight &&
                   playerBottom > objectBottom &&
                   playerTop < objectBottom;
        }
    }
}
