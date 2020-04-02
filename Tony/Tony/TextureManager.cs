using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;


namespace Tony
{
    public static class TextureManager
    {

        private static Dictionary<string, List<TextureData>> TileSets = new Dictionary<string, List<TextureData>>();

        public static void LoadTextures(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            List<string> filenames = Directory.EnumerateFiles("Content/TileSets").Select(Path.GetFileName).ToList<string>();
            foreach (string tileSetName in filenames)
            {
                string tileSetPath = "Content/TileSets/" + tileSetName;

                List<TextureData> currentTileSet = new List<TextureData>();
                XmlReader tileReader = XmlReader.Create(tileSetPath);
                while (tileReader.Read())
                {

                    //for each tile in the tileset file, add its file location to the tileset list.
                    if ((tileReader.NodeType == XmlNodeType.Element) && (tileReader.Name == "image"))
                    {
                        string textureName = tileReader.GetAttribute("source");
                        TextureData newTexture = new TextureData(content.Load<Texture2D>(textureName), textureName);
                        currentTileSet.Add(newTexture);
                    }
                    tileReader.Read();
               }
    
                TileSets.Add(tileSetPath, currentTileSet);

            }
        }


        public static Texture2D GetTextureByNum(string tileSetPath, int textureNum)
        {
            List<TextureData> tileSet = TileSets[tileSetPath];
            Texture2D texture = tileSet[textureNum].texture;
            return texture;
        }

        public static Texture2D GetTextureByName(string tileSetPath, string textureName)
        {
            List<TextureData> tileSet = TileSets[tileSetPath];
            TextureData textureData = tileSet.Find(x => x.textureName.Contains(textureName));
            return textureData.texture;
        }


        internal struct TextureData
        {

            public TextureData(Texture2D tex, string texName)
            {
                texture = tex;
                textureName = texName;
            }

            public Texture2D texture { get; }
            public string textureName { get; }
        }




    }
}
