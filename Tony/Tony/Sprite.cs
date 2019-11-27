using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Sprite : GameObject, Drawable
    {
        protected Texture2D texture;
        protected float depth;

        /// <summary>
        /// A sprite represents any drawn object that is not the player character or a tile.
        /// Sprites are simply GameObjects with a texture.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="depth"></param>
        /// <param name="texture"></param>
        public Sprite(Vector2 position, Vector2 size, float depth, Texture2D texture) :
            base(position, size)
        {
            this.depth = depth;
            this.texture = texture;
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
