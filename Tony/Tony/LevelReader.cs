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
        //width, height, tilewidth and tileheight represent basic map information.
        private int width;
        private int height;
        public int tileWidth;
        public int tileHeight;

        //reader stores the content of an Xml file in memory.
        private XDocument reader;

        //tileNumbers, tileset and objectElements store the information taken from the Xml file.
        public List<String[]> tileNumbers;
        public List<String> tileset;
        public List<XElement> objectElements;
        private XElement map;


        /// <summary>
        /// the LevelReader takes a file path string as a parameter.
        /// It then reads the xml file found and sets the instance variables to the value of certain information in the file.
        /// </summary>
        /// <param name="filePath"></param>
        public LevelReader(String filePath)
        {
            reader = XDocument.Load(filePath);
            this.map = reader.Element("map");
            this.objectElements = new List<XElement>();
            this.tileset = new List<string>();
            if (map.Attribute("orientation").Value != "orthogonal")
            {
                //return exception level file invalid.
            }
            else
            {
                //sets the basic map information.
                this.width = Int32.Parse(map.Attribute("width").Value);
                this.height = Int32.Parse(map.Attribute("height").Value);
                this.tileWidth = Int32.Parse(map.Attribute("tilewidth").Value);
                this.tileHeight = Int32.Parse(map.Attribute("tileheight").Value);
            }

            //finds the element called tileset from the map file and uses it to create a new xmlreader for the tileset file.
            XElement tilesetNode = map.Element("tileset");
            XmlReader tileReader = XmlReader.Create(tilesetNode.Attribute("source").Value);
            while (tileReader.Read())
            {
                //for each tile in the tileset file, add its file location to the tileset list.
                if ((tileReader.NodeType == XmlNodeType.Element) && (tileReader.Name == "image"))
                {
                    tileset.Add(tileReader.GetAttribute("source"));
                }
                tileReader.Read();
            }

            //sets the tilenumbers instance variable to the list returned by tileSplitter.
            tileNumbers = tileSplitter();

            //finds the objectLayer element and adds each of its child elements to the objectElements list.
            XElement objectLayer = map.Element("objectgroup");
            foreach (XElement currentObject in objectLayer.Elements())
            {
                objectElements.Add(currentObject);
            }

        }
        /// <summary>
        /// tileSpitter is used to take the tile data saved in teh map file and split it into sets based on the rows and columns of the map.
        /// </summary>
        /// <returns></returns>
        public List<String[]> tileSplitter()
        {
            //finds the tilelayer, and splits it into individual tile strings.
            XElement tilelayer = map.Element("layer");
            String tiledata = tilelayer.Element("data").Value;
            String[] alltiles = tiledata.Split(',');


            //runs through the tile data and splits it into equal groups based on the current maps row length.
            List<string[]> rows = new List<string[]>();
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
