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
        private Texture2D texture;

        public Tile(Vector2 position, Vector2 size, Texture2D texture) : base(position, size)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), size.ToPoint()), Color.White);
        }
    }
}
