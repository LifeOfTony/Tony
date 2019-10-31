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
        public Npc(String name, Vector2 position, float depth, Texture2D texture, String requirement) :
            base(name, position, depth, texture, requirement)
        {

        }
    }
}
