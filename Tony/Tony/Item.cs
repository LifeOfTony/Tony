using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tony
{
    public class Item
    {
        private int modifier;
        private string name;
        private Boolean collected;
        private Boolean equiped;

        /// <summary>
        /// items have a name and a mental-state modifier.
        /// by default items are never equiped of collected on creation.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="modifier"></param>
        public Item(string name, int modifier)
        {
            this.name = name;
            this.modifier = modifier;
            this.collected = false;
            this.equiped = false;
        }

        /// <summary>
        /// returns the items name.
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.name;
        }

        /// <summary>
        /// returns whether this item has been collected.
        /// </summary>
        /// <returns></returns>
        public bool IsCollected()
        {
            return this.collected;
        }

        /// <summary>
        /// collects the item.
        /// </summary>
        public void Collect()
        {
            this.collected = true;
        }

    }
}
