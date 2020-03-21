using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    public class Event : GameObject, Interactable
    {
        private string[] actors;
        private string requirement;
        private string gives;


        public Event(Vector2 position, Vector2 size, string actors, string requirement = "", string gives = "")
            : base (position, size)
        {
            this.actors = actors.Split(',');
            this.requirement = requirement;
            this.gives = gives;
        }

        public void Interact()
        {
            foreach(string actorName in actors)
            {
                foreach(Npc i in ObjectManager.Instance.CurrentLevel.Npcs.Where(i => i.getActor() == true))
                {
                    i.Interact();
                }
            }

        }


        


    }
}
