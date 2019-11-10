using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Tile : GameObject, Drawable
    {
        // The tiles texture image.
        private Texture2D texture;

        /// <summary>
        /// A tile is a gameObject with a texture but no other features.
        /// The texture is based on a value given by the level file.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="texture"></param>
        public Tile(Vector2 position, Vector2 size, Texture2D texture) : base(position, size)
        {
            this.texture = texture;
        }

        /// <summary>
        /// The draw method allows the tile to be drawn by the spriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), size.ToPoint()), Color.White);
        }
    }
}
