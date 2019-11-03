using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class LevelReader
    {
        private int width;
        private int height;
        private int tileWidth;
        private int tileHeight;
        private XmlReader reader;
        private List<String[]> tileNumbers;

        public LevelReader(String filePath)
        {
            reader = XmlReader.Create(filePath);
            while(reader.Read())
            {
                if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "map"))
                {
                    if(reader.GetAttribute("orientation") != "orthogonal")
                    {
                        //return exception level file invalid
                        break;
                    }
                    else
                    {
                        this.width = Int32.Parse(reader.GetAttribute("width"));
                        this.height = Int32.Parse(reader.GetAttribute("height"));
                        this.tileWidth = Int32.Parse(reader.GetAttribute("tilewidth"));
                        this.tileHeight = Int32.Parse(reader.GetAttribute("tileheight"));
                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "tileset"))
                {
                    XmlReader tileReader = XmlReader.Create(reader.GetAttribute("source"));
                    while(tileReader.Read())
                    {
                        if((tileReader.NodeType == XmlNodeType.Element) && (tileReader.Name == "tile"))
                        {
                            tileReader.Read();
                            Texture2D newTexture = Content.Load<Texture2D>("testimage/City");
                        }
                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && Int32.Parse(reader.GetAttribute("id")) == 1)
                {
                    reader.Read();
                    tileNumbers = tileSplitter();

                }
                else if ((reader.NodeType == XmlNodeType.Element) && Int32.Parse(reader.GetAttribute("id")) == 2)
                {
                    reader.Read();
                    while(reader.Name == "Object")
                    {
                        if(reader.GetAttribute("drawable") != null)
                        {
                            Vector2 position = new Vector2(Int32.Parse(reader.GetAttribute("x")), Int32.Parse(reader.GetAttribute("y")));
                            Vector2 size = new Vector2(Int32.Parse(reader.GetAttribute("width")), Int32.Parse(reader.GetAttribute("height")));
                            // TODO: write object generation.
                        }
                    }
                }
            }
        }

        public List<String[]> tileSplitter()
        {
            String tiledata = reader.Value;
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
