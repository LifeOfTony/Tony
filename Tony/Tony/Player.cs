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
    class Player : GameObject, Drawable
    {
        private int age;
        private float depth;
        private Texture2D texture;
        private int moveSpeed;
        private int range;
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
        public Player(Vector2 position, Vector2 size, int age, float depth, Texture2D texture) :
            base(position, size)
        {
            this.age = age;
            this.depth = depth;
            this.texture = texture;
            this.moveSpeed = 1;
            this.range = 1;
            velocity = Vector2.Zero;

        }

        /// <summary>
        /// The move method computes player movement.
        /// The movement key pressed is given as a parameter.
        /// </summary>
        /// <param name="key"></param>
        public void move(string key)
        {
            // setVelocity sets the values of velocity based on teh key given.
            setVelocity(key);


            // Compares the player position to all collidable objects.
            foreach(GameObject currentObject in ObjectManager.Collidables)
            {
                if (currentObject == this)
                    continue;

                // an instance of Collisions is created using the current object and the newPosition variable
                Detector detector = new Detector(currentObject.getPosition(), currentObject.getSize(), this.position, this.size, this.moveSpeed);

                // conditions for horizontal movement.
                //if not met, the horezontal velocity is set to 0.
                if ((velocity.X > 0 && detector.IsTouchingLeft()) || (velocity.X < 0 && detector.IsTouchingRight())) velocity.X = 0;

                // conditions for vertical movement.
                //if not met, the vertical velocity is set to 0.
                if ((velocity.Y > 0 && detector.IsTouchingTop()) || (velocity.Y < 0 && detector.IsTouchingBottom())) velocity.Y = 0;
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

        /// <summary>
        /// triggered by the action key (E).
        /// runs through all InteractableObjects and finds out if they are within range of the player.
        /// 
        /// </summary>
        public void interact()
        {
            foreach(GameObject i in ObjectManager.Objects)
            {
                if (i is InteractableObject)
                {
                    InteractableObject currentObject = (InteractableObject)i;

                    // an Interactor is created from teh current object and the player to test interaction logic.
                    Detector interaction = new Detector(currentObject.getPosition(), currentObject.getSize(), this.position, this.size, this.range);

                    //conditions of interaction.
                    //if met and interaction is triggered.
                    if(interaction.IsTouchingBottom() || interaction.IsTouchingTop() || interaction.IsTouchingLeft() || interaction.IsTouchingRight())
                    {
                        currentObject.interact();
                    }
                }
            }
        }

        /// <summary>
        /// the Draw function for the Player sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
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
                layerDepth: depth);
        }
    }
}
