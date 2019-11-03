using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class InteractableObject : GameObject
    {
        private String requirement;

        public InteractableObject(Vector2 position, Vector2 size, String requirement) :
            base(position, size)
        {
            this.requirement = requirement;
        }

        public void interact()
        {

        }
    }
}
