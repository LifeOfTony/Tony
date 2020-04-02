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
        private bool textureNum;
        private string texturePath;

        /// <summary>
        /// The Player object represents the character Tony.
        /// It has an age variable to hold the current version of Tony
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="age"></param>
        /// <param name="depth"></param>
        /// <param name="texture"></param>
        public Player(Vector2 position, Vector2 size, int age, string filePath, Texture2D texture, float baseDepth) :
            base(position, size, texture, baseDepth)
        {
            this.age = age;
            this.moveSpeed = 1;
            velocity = Vector2.Zero;
            textureNum = true;
            texturePath = filePath;
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
            foreach(GameObject currentObject in ObjectManager.currentLevel.Collidables)
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
            Vector2 newPosition = this.position + velocity;
            this.texture = Animation.Animate(this.position, newPosition, textureNum, texturePath);
            textureNum = !textureNum;
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


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: texture,
                position: position,
                sourceRectangle: null,
                color: Color.White,
                rotation: rotation,
                origin: rotationOrigin,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: baseDepth);
        }





    }
}
