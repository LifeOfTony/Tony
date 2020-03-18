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

        Level levelRead;
        private int levelNo;
        private XDocument reader;
        //tileNumbers, tileset and objectElements store the information taken from the Xml file.
        public List<Texture2D> tileset;
        public List<XElement> interactors;
        public List<XElement> colliders;
        private XElement map;
        private Microsoft.Xna.Framework.Content.ContentManager Content;

        /// <summary>
        /// the LevelReader takes a file path string as a parameter.
        /// It then reads the xml file found and sets the instance variables to the value of certain information in the file.
        /// </summary>
        /// <param name="filePath"></param>
        public LevelReader(string filePath, Microsoft.Xna.Framework.Content.ContentManager content, int level)
        {
            reader = XDocument.Load(filePath);
            Content = content;
            levelNo = level;
            LoadFile();
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



        public void LoadFile()
        {
            this.map = reader.Element("map");
            this.interactors = new List<XElement>();
            this.colliders = new List<XElement>();
            this.tileset = new List<Texture2D>();
            if (map.Attribute("orientation").Value != "orthogonal")
            {
                //return exception level file invalid.
            }

            //sets the basic map information.
            this.width = Int32.Parse(map.Attribute("width").Value);
            this.height = Int32.Parse(map.Attribute("height").Value);
            this.tileWidth = Int32.Parse(map.Attribute("tilewidth").Value);
            this.tileHeight = Int32.Parse(map.Attribute("tileheight").Value);

            levelRead = new Level(levelNo, width, height);


            CreateTileSet();
            FetchTileData();
            FetchObjectData();
        }


        private void FetchTileData()
        {
            IEnumerable<XElement> tileLayers = map.Elements("layer");
            foreach (XElement currentLayer in tileLayers)
            {
                int depth = Int32.Parse(currentLayer.Attribute("name").Value);
                string tileData = currentLayer.Element("data").Value;
                CreateTiles(tileData, depth);
            }
        }

        private void CreateTiles(string tileData, int depth)
        {
           
            List<string[]> currentLayer = tileSplitter(tileData);
            for (int y = 0; y < currentLayer.Count; y++)
            {
                string[] currentRow = currentLayer[y];
                for (int x = 0; x < currentRow.Length; x++)
                {

                    int textureNumber = Int32.Parse(currentRow[x]);
                    if (textureNumber != 0)
                    {
                        Vector2 position = new Vector2(x * tileWidth, y * tileHeight);
                        Vector2 size = new Vector2(tileWidth, tileHeight);
                        Sprite currentTile = new Sprite(position, size, depth, tileset[textureNumber - 1]);
                        levelRead.AddObject(currentTile);
                    }

                }
            }

            
        }

        private void CreateProps(XElement objectData, int depth)
        {
            // Data taken from the object element.
            Vector2 size = new Vector2(float.Parse(objectData.Attribute("width").Value), float.Parse(objectData.Attribute("height").Value));
            Vector2 position = new Vector2(float.Parse(objectData.Attribute("x").Value), float.Parse(objectData.Attribute("y").Value) - size.Y);
            // creates a new collider.
            Sprite currentProp = new Sprite(position, size, depth, tileset[Int32.Parse(objectData.Attribute("gid").Value) - 1]);
            levelRead.AddObject(currentProp);
        }


        private void FetchObjectData()
        {
            foreach (XElement objectLayer in map.Elements("objectgroup"))
            {
                IEnumerable<XElement> objects = objectLayer.Elements("object");
                switch (objectLayer.Attribute("name").Value)
                {
                    case "Interactable Layer":
                        foreach (XElement objectData in objects)
                        {
                            CreateInteractors(objectData);
                        }
                        break;

                    case "Collision Layer":
                        foreach (XElement objectData in objects) CreateColliders(objectData);
                        break;

                    case "Player":
                        XElement playerData = objects.First();
                        CreatePlayer(playerData);
                        break;

                    case "Prop Layer":
                        int depth = Int32.Parse(objectLayer.Element("properties").Element("property").Attribute("value").Value);
                        foreach (XElement objectData in objects) CreateProps(objectData, depth);
                        break;

                }
            }
        }

        private void CreateInteractors(XElement objectData)
        {
            // Data taken from the object element.
            Vector2 size = new Vector2(float.Parse(objectData.Attribute("width").Value), float.Parse(objectData.Attribute("height").Value));
            Vector2 position = new Vector2(float.Parse(objectData.Attribute("x").Value), float.Parse(objectData.Attribute("y").Value) - size.Y);
            string requires = null;
            string gives = null;
            string basic = null;
            string route = null;
            bool basicMove = false;

            #region object property navigation
            if (objectData.Element("properties") != null)
            {
                IEnumerable<XElement> properties = objectData.Element("properties").Elements();
                foreach (XElement property in properties)
                {
                    if (property.Attribute("name").Value == "Requires") requires = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "Gives") gives = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "Basic") basic = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "BasicMove") basicMove = bool.Parse(property.Attribute("value").Value);
                    if (property.Attribute("name").Value == "Route")
                    {
                        route = property.Attribute("value").Value;
                    }

                }
            }

            
            #endregion

            #region object creation
            if (objectData.Attribute("type").Value == "Interactable")
            {

                InteractableObject currentObject = new InteractableObject(position, size, 4, tileset[Int32.Parse(objectData.Attribute("gid").Value) - 1], requires, gives);
                levelRead.AddObject(currentObject);
            }
            if (objectData.Attribute("type").Value == "NPC")
            {
                Npc currentObject = new Npc(position, size, 4, tileset[Int32.Parse(objectData.Attribute("gid").Value) - 1], route, basicMove, requires, gives);
                levelRead.AddObject(currentObject);
            }

            if(objectData.Attribute("type").Value == "Actor")
            {
                Npc currentObject = new Npc(position, size, 4, tileset[Int32.Parse(objectData.Attribute("gid").Value) - 1], route, true, basicMove, requires, gives);
                levelRead.AddObject(currentObject);
            }
            if (objectData.Attribute("type").Value == "EndObject")
            {
                EndObject currentObject = new EndObject(position, size, 4, tileset[Int32.Parse(objectData.Attribute("gid").Value) - 1], requires, gives);
                levelRead.AddObject(currentObject);
            }
            #endregion
        }

        private void CreateColliders(XElement objectData)
        {
            // Data taken from the object element.
            Vector2 position = new Vector2(float.Parse(objectData.Attribute("x").Value), float.Parse(objectData.Attribute("y").Value));
            Vector2 size = new Vector2(float.Parse(objectData.Attribute("width").Value), float.Parse(objectData.Attribute("height").Value));

            // creates a new collider.
            Collider currentCollider = new Collider(position, size);
            levelRead.AddObject(currentCollider);
        }

        private void CreatePlayer(XElement playerData)
        {
            Vector2 size = new Vector2(float.Parse(playerData.Attribute("width").Value), float.Parse(playerData.Attribute("height").Value));
            Vector2 position = new Vector2(float.Parse(playerData.Attribute("x").Value), float.Parse(playerData.Attribute("y").Value) - size.Y);


            Player player = new Player(position, size, 1, tileset[Int32.Parse(playerData.Attribute("gid").Value) - 1], 4);
            levelRead.AddObject(player);
        }

        private void CreateTileSet()
        {

            foreach (XElement tilesetNode in map.Elements("tileset"))
            {
                //finds the element called tileset from the map file and uses it to create a new xmlreader for the tileset file.
                XmlReader tileReader = XmlReader.Create(tilesetNode.Attribute("source").Value);
                while (tileReader.Read())
                {
                    //for each tile in the tileset file, add its file location to the tileset list.
                    if ((tileReader.NodeType == XmlNodeType.Element) && (tileReader.Name == "image"))
                    {
                        tileset.Add(Content.Load<Texture2D>(tileReader.GetAttribute("source")));
                    }
                    tileReader.Read();
                }
            }


            
        }

        public Level GetLevel()
        {
            return levelRead;
        }

    }
}
