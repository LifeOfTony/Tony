using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony
{
    /// <summary>
    /// Objects that are Drawable make up anything seen on the screen that needs a drawn texture
    /// </summary>
    public interface Drawable
    {
        void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
