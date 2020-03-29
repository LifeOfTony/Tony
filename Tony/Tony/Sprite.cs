using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{

    public class Sprite : GameObject, Drawable
    {
        protected Texture2D texture;
        protected float baseDepth;


        /// <summary>
        /// A sprite represents any drawn object that is not the player character or a tile.
        /// Sprites are simply GameObjects with a texture.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="depth"></param>
        /// <param name="texture"></param>
        public Sprite(Vector2 position, Vector2 size, Texture2D texture, float baseDepth) :
            base(position, size)
        {
            this.baseDepth = baseDepth;
            this.texture = texture;
        }

        /// <summary>
        /// The draw method to allow the sprite to be crawn by the spriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            float depth = baseDepth;
            if (baseDepth > 0)
            {
                    if (ObjectManager.currentLevel.Player.position.Y + (ObjectManager.currentLevel.Player.size.Y / 2) > position.Y + (size.Y / 2))
                        depth = baseDepth - 0.2f;
            }

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
