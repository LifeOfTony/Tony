using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Collider : GameObject
    {
        public Collider(Vector2 position, Vector2 size) :
            base(position, size)
        {

        }
    }
}
