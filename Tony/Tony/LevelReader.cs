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
        public int width;
        public int height;
        public int tileWidth;
        public int tileHeight;

        //reader stores the content of an Xml file in memory.
        private XDocument reader;

        //tileNumbers, tileset and objectElements store the information taken from the Xml file.
        public List<string> layers;
        public List<String> tileset;
        public List<XElement> interactors;
        public List<XElement> colliders;
        public XElement player;
        private XElement map;


        /// <summary>
        /// the LevelReader takes a file path string as a parameter.
        /// It then reads the xml file found and sets the instance variables to the value of certain information in the file.
        /// </summary>
        /// <param name="filePath"></param>
        public LevelReader(string filePath)
        {
            #region Setting up basic map data.
            reader = XDocument.Load(filePath);
            this.map = reader.Element("map");
            this.interactors = new List<XElement>();
            this.colliders = new List<XElement>();
            this.tileset = new List<string>();
            this.layers = new List<string>();
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
            #endregion

            #region Fetching the tileset data.
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
            #endregion

            #region Fetching tile data.
            IEnumerable<XElement> tileLayers = map.Elements("layer");
            foreach(XElement currentLayer in tileLayers)
            {
                string tiledata = currentLayer.Element("data").Value;
                this.layers.Add(tiledata);
            }
            #endregion

            #region Fetching object data.
            //finds the objectLayer element and adds each of its child elements to the objectElements list.
            foreach (XElement objectLayer in map.Elements("objectgroup"))
            {
                IEnumerable<XElement> objects = objectLayer.Elements();
                switch (objectLayer.Attribute("name").Value)
                {
                    case "Interactable Layer":
                        foreach (XElement objectData in objects) interactors.Add(objectData);
                        break;
                    case "Collision Layer":
                        foreach (XElement objectData in objects) colliders.Add(objectData);
                        break;
                    case "Player":
                        this.player = objects.First();
                        break;
                }
            }

            #endregion

        }
        /// <summary>
        /// tileSpitter is used to take the tile data saved in the map file and split it into sets based on the rows and columns of the map.
        /// </summary>
        /// <returns></returns>
        public List<string[]> tileSplitter(string layerData)
        {
            string[] alltiles = layerData.Split(',');


            //runs through the tile data and splits it into equal groups based on the current maps row length.
            List<string[]> rows = new List<string[]>();
            for (int i = 0; i < alltiles.Length; i += this.width)
            {
                string[] slice = new string[this.width];
                Array.Copy(alltiles, i, slice, 0, this.width);
                rows.Add(slice);
            }
            return rows;
        }
 
    }
}
