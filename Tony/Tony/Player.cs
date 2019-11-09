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
        private Vector2 velocity;


        public Player(Vector2 position, Vector2 size, bool collidable, int age, float depth, Texture2D texture) :
            base(position, size, collidable)
        {
            this.age = age;
            this.depth = depth;
            this.texture = texture;
            this.moveSpeed = 1;
            velocity = Vector2.Zero;

        }

        public void move(string key)
        {
            setVelocity(key);
            Vector2 newPosition = this.position + velocity;
            foreach(GameObject currentObject in ObjectManager.Collidables)
            {
                if (currentObject == this)
                    continue;

                Collisions collider = new Collisions(currentObject.getPosition(), currentObject.getSize(), newPosition, this.size);

                if ((velocity.X > 0 && collider.IsTouchingLeft()) || (velocity.X < 0 && collider.IsTouchingRight())) velocity.X = 0;

                if ((velocity.Y > 0 && collider.IsTouchingTop()) || (velocity.Y < 0 && collider.IsTouchingBottom())) velocity.Y = 0;
            }

            this.position += velocity;

            velocity = Vector2.Zero;
            
            
        }

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
