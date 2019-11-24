using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Npc : InteractableObject, Drawable
    {
        private Texture2D texture;
        private float depth;
        /// <summary>
        /// An Npc is a moving interactable object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="collidable"></param>
        /// <param name="requirement"></param>
        /// <param name="gives"></param>
        public Npc(Vector2 position, Vector2 size, string requirement, string gives, float depth, Texture2D texture) :
            base(position, size, requirement, gives)
        {
            this.texture = texture;
            this.depth = depth;
        }

        /// <summary>
        /// The draw method to allow the sprite to be crawn by the spriteBatch.
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
