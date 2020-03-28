using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tony
{
    public class Player : Sprite
    {
        private int age;
        private int moveSpeed;
        private Vector2 velocity;

        /// <summary>
        /// The Player object represents the character Tony.
        /// It has an age variable to hold the current version of Tony
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="age"></param>
        /// <param name="depth"></param>
        /// <param name="texture"></param>
        public Player(Vector2 position, Vector2 size, int age, Texture2D texture, float depth) :
            base(position, size, depth, texture)
        {
            this.age = age;
            this.moveSpeed = 1;
            velocity = Vector2.Zero;

        }


        /// <summary>
        /// The move method computes player movement.
        /// The movement key pressed is given as a parameter.
        /// </summary>
        /// <param name="key"></param>
        public void move(string key)
        {
            // setVelocity sets the values of velocity based on the key given.
            setVelocity(key);


            // Compares the player position to all collidable objects.
            foreach(GameObject currentObject in ObjectManager.CurrentLevel.Collidables)
            {
                if (currentObject == this)
                    continue;

                // conditions for horizontal movement.
                //if not met, the horezontal velocity is set to 0.

                if ((velocity.X > 0 && Detector.IsTouchingLeft(currentObject.getPosition(), currentObject.getSize(), this.position, this.size, this.moveSpeed)) 
                    || (velocity.X < 0 && Detector.IsTouchingRight(currentObject.getPosition(), currentObject.getSize(), this.position, this.size, this.moveSpeed))) velocity.X = 0;

                // conditions for vertical movement.
                //if not met, the vertical velocity is set to 0.
                if ((velocity.Y > 0 && Detector.IsTouchingTop(currentObject.getPosition(), currentObject.getSize(), this.position, this.size, this.moveSpeed))
                    || (velocity.Y < 0 && Detector.IsTouchingBottom(currentObject.getPosition(), currentObject.getSize(), this.position, this.size, this.moveSpeed))) velocity.Y = 0;
            }

            // updates the player position based on velocity and resets velocity.

            this.position += velocity;
            velocity = Vector2.Zero;
            
            
        }

        /// <summary>
        /// sets the velocity based on teh key pressed.
        /// </summary>
        /// <param name="key"></param>
        public void setVelocity(string key)
        {
            switch(key)
            {
                case "A":
                    velocity.X = -moveSpeed;
                    break;
                case "D":
                    velocity.X = moveSpeed;
                    break;
                case "W":
                    velocity.Y = -moveSpeed;
                    break;
                case "S":
                    velocity.Y = moveSpeed;
                    break;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

    }
}
