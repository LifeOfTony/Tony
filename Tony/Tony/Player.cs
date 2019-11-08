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


        public Player(Vector2 position, Vector2 size, bool collidable, int age, float depth, Texture2D texture) :
            base(position, size, collidable)
        {
            this.age = age;
            this.depth = depth;
            this.texture = texture;

        }

        public void move(string key)
        {
            foreach(GameObject currentObject in ObjectManager.Objects)
            {
                Vector2 objectPosition = currentObject.getPosition();
                Vector2 objectSize = currentObject.getSize();
                switch (key)
                {
                    case "A":
                        if (currentObject.getCollidable() == true && (this.position.X -= 5) <= (objectPosition.X + objectSize.X)) break;
                        else
                        {
                            this.position.X -= 5;
                            break;
                        }
                    case "W":
                        this.position.Y -= 5;
                        break;
                    case "S":
                        this.position.Y += 5;
                        break;
                    case "D":
                        this.position.X += 5;
                        break;

                }
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
