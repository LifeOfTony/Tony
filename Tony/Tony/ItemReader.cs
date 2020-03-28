using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tony
{
    /// <summary>
    /// This class reads an XML file containing the Item data for the game.
    /// </summary>
    class ItemReader
    {
        //reader stores the content of an Xml file in memory.

        //items is the root element.
        private XElement items;
        private XDocument reader;
        /// <summary>
        /// takes a filepath to the XML file and reads the file.
        /// </summary>
        /// <param name="filePath"></param>
        public ItemReader(string filePath)
        {
            reader = XDocument.Load(filePath);
            this.items = reader.Element("items");

            //foreach item tag, create a new Item object, and add it to the Item list.
            foreach(XElement currentItem in items.Elements())
            {
                string name = currentItem.Attribute("name").Value;
                int modifier = Int32.Parse(currentItem.Attribute("modifier").Value);

                Item newItem = new Item(name, modifier);
                ObjectManager.AddItem(newItem);
            }
        }


    }
}
