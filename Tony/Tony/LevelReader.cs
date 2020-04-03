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

        private Level levelRead;
        private XDocument reader;
        //tileNumbers, tileset and objectElements store the information taken from the Xml file.
        public List<XElement> interactors;
        public List<XElement> colliders;
        private XElement map;
        private Microsoft.Xna.Framework.Content.ContentManager Content;


        private Dictionary<int, string> activeTileSets = new Dictionary<int, string>();

        /// <summary>
        /// the LevelReader takes a file path string as a parameter.
        /// It then reads the xml file found and sets the instance variables to the value of certain information in the file.
        /// </summary>
        /// <param name="filePath"></param>
        public LevelReader(string filePath, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            reader = XDocument.Load(filePath);
            Content = content;

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
            if (map.Attribute("orientation").Value != "orthogonal")
            {
                //return exception level file invalid.
            }

            int levelNo = Int32.Parse(map.Element("properties").Element("property").Attribute("value").Value);

            //sets the basic map information.
            this.width = Int32.Parse(map.Attribute("width").Value);
            this.height = Int32.Parse(map.Attribute("height").Value);
            this.tileWidth = Int32.Parse(map.Attribute("tilewidth").Value);
            this.tileHeight = Int32.Parse(map.Attribute("tileheight").Value);

            levelRead = new Level(levelNo, width, height, tileWidth, tileHeight);

            ScriptReader.currentLevel = levelNo;

            foreach (XElement tilesetNode in map.Elements("tileset"))
            {
                int startNum = Int32.Parse(tilesetNode.Attribute("firstgid").Value);
                string setPath = tilesetNode.Attribute("source").Value;
                activeTileSets.Add(startNum, setPath);
            }
            FetchTileData();
            FetchObjectData();
        }







        private void FetchTileData()
        {
            IEnumerable<XElement> tileLayers = map.Elements("layer");
            foreach (XElement currentLayer in tileLayers)
            {
                string tileData = currentLayer.Element("data").Value;
                CreateTiles(tileData);
            }
        }






        private void CreateTiles(string tileData)
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
                        Sprite currentTile = new Sprite(position, size, getTexture(textureNumber).Item1, 0); 
                        levelRead.AddObject(currentTile);
                    }

                }
            }

            
        }






        private void CreateProps(XElement objectData, float baseDepth)
        {
            // Data taken from the object element.
            Vector2 size = new Vector2(float.Parse(objectData.Attribute("width").Value), float.Parse(objectData.Attribute("height").Value));
            Vector2 position = new Vector2(float.Parse(objectData.Attribute("x").Value), float.Parse(objectData.Attribute("y").Value) - size.Y);
            // creates a new collider.
            Sprite currentProp = new Sprite(position, size, getTexture(Int32.Parse(objectData.Attribute("gid").Value)).Item1, baseDepth);
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
                        float baseDepth = float.Parse(objectLayer.Element("properties").Element("property").Attribute("value").Value)/10;
                        foreach (XElement objectData in objects) CreateProps(objectData, baseDepth);
                        break;

                }
            }
        }




        private void CreateInteractors(XElement objectData)
        {
            // Data taken from the object element.
            Vector2 size = new Vector2(float.Parse(objectData.Attribute("width").Value), float.Parse(objectData.Attribute("height").Value));
            Vector2 position = new Vector2(float.Parse(objectData.Attribute("x").Value), float.Parse(objectData.Attribute("y").Value) - tileHeight);
            string requires = null;
            string gives = null;
            string route = null;
            string subType = null;

            string actors = "";
            float baseDepth = 0.4f;

            #region object property navigation
            if (objectData.Element("properties") != null)
            {
                IEnumerable<XElement> properties = objectData.Element("properties").Elements();
                foreach (XElement property in properties)
                {
                    if (property.Attribute("name").Value == "Requires") requires = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "Gives") gives = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "SubType") subType = property.Attribute("value").Value;
                    if (property.Attribute("name").Value == "Route")
                    {
                        route = property.Attribute("value").Value;
                    }
                    if (property.Attribute("name").Value == "Actors")
                    {
                        actors = property.Attribute("value").Value;
                    }

                }
            }

            
            #endregion

            #region object creation
            if (objectData.Attribute("type").Value == "Interactable")
            {

                InteractableObject currentObject = new InteractableObject(position, size, getTexture(Int32.Parse(objectData.Attribute("gid").Value)).Item1, baseDepth,
                    objectData.Attribute("name").Value, requires, gives);
                levelRead.AddObject(currentObject);
            }
            if (objectData.Attribute("type").Value == "NPC")
            {
                Tuple<Texture2D, string> textureData = getTexture(Int32.Parse(objectData.Attribute("gid").Value));
                Npc currentObject = new Npc(position, size, textureData.Item1 , baseDepth, route,
                    objectData.Attribute("name").Value, textureData.Item2, requires, gives);
                levelRead.AddObject(currentObject);
            }

            if(objectData.Attribute("type").Value == "Actor")
            {
                Tuple<Texture2D, string> textureData = getTexture(Int32.Parse(objectData.Attribute("gid").Value));

                if(subType.Equals("NPC"))
                {
                    Npc currentObject = new Npc(position, size, textureData.Item1, baseDepth, route,
                    objectData.Attribute("name").Value, textureData.Item2, true, requires, gives);
                    levelRead.AddObject(currentObject);
                }
                else
                {
                    InteractableObject currentObject = new InteractableObject(position, size, getTexture(Int32.Parse(objectData.Attribute("gid").Value)).Item1, baseDepth,
                    objectData.Attribute("name").Value, requires, gives);
                    levelRead.AddObject(currentObject);
                }
            }

            if(objectData.Attribute("type").Value == "Event")
            {
                Event currentObject = new Event(position, size, actors);
                levelRead.AddObject(currentObject);
            }
            if (objectData.Attribute("type").Value == "EndObject")
            {
                EndObject currentObject = new EndObject(position, size, getTexture(Int32.Parse(objectData.Attribute("gid").Value)).Item1, baseDepth, requires, gives);
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
            float playerDepth = 0.2f;

            Tuple<Texture2D, string> textureData = getTexture(Int32.Parse(playerData.Attribute("gid").Value));
            Player player = new Player(position, size, 1, textureData.Item2 , textureData.Item1, playerDepth);
            levelRead.AddObject(player);
        }




        



        public Tuple<Texture2D, string> getTexture(int textureNum)
        {
          
            Texture2D texture = null;
            string currentPath = null;
            List<int> startNums = new List<int>(activeTileSets.Keys);
            startNums.Reverse();
       
            foreach(int i in startNums)
            {
               
                if (textureNum >= i)
                {
                    currentPath = activeTileSets[i];
                    int trueTextureNum = textureNum - i;
                    
                    texture = TextureManager.GetTextureByNum(currentPath, trueTextureNum);
                    break;
                }
            }

            if(texture != null)
            {
                Tuple<Texture2D, string> data = new Tuple<Texture2D, string>(texture, currentPath);
                return data;
            }
            else
            {
                Console.WriteLine("texture doesnt exist");
                return null;
            }
           
        }










        public Level GetLevel()
        {
            return levelRead;
        }

    }
}
