using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace Tony
{
    class LevelReader
    {
        private int width;
        private int height;
        private int tileWidth;
        private int tileHeight;
        private XDocument reader;
        private List<String[]> tileNumbers;
        private List<String> tileset;
        private List<XElement> objectElements;

        public LevelReader(String filePath)
        {
            reader = XDocument.Load(filePath);
            XElement map = reader.Element("map");
            if (map.Attribute("orientation").Value != "orthogonal")
            {
                //return exception level file invalid
            }
            else
            {
                this.width = Int32.Parse(map.Attribute("width").Value);
                this.height = Int32.Parse(map.Attribute("height").Value);
                this.tileWidth = Int32.Parse(map.Attribute("tileWidth").Value);
                this.tileHeight = Int32.Parse(map.Attribute("tileHeight").Value);
            }

            XElement tileset = reader.Element("tileset");
            XmlReader tileReader = XmlReader.Create(tileset.Attribute("source").Value);
            while (tileReader.Read())
            {
                if ((tileReader.NodeType == XmlNodeType.Element) && (tileReader.Name == "tile"))
                {
                    tileReader.Read();
                    tileset.Add(tileReader.GetAttribute("source"));
                }
                tileReader.Read();
            }

            tileNumbers = tileSplitter();

            XElement objectLayer = reader.Element("objectgroup");
            foreach (XElement currentObject in objectLayer.Elements())
            {
                objectElements.Add(currentObject);
            }

        }

        public List<String[]> tileSplitter()
        {
            XElement tilelayer = reader.Element("layer");
            String tiledata = tilelayer.Element("data").Value;
            String[] alltiles = tiledata.Split(',');
            List<String[]> rows = new List<string[]>();
            for (int i = 0; i < alltiles.Length; i += this.width)
            {
                String[] slice = new String[this.width];
                Array.Copy(alltiles, i, slice, 0, this.width);
                rows.Add(slice);
            }
            return rows;
        }
    }
}
