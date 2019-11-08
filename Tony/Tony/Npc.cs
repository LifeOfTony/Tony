using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class Npc : InteractableObject
    {
        public Npc(Vector2 position, Vector2 size, bool collidable, string requirement, string gives) :
            base(position, size, collidable, requirement, gives)
        {

        }
    }
}
