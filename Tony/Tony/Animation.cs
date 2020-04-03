using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    public static class Animation
    {

        public static Texture2D AnimateMoving(Vector2 currentPosition, Vector2 newPosition, bool num, string texturePath)
        {
            Texture2D newTexture;
            int textureNum;

            if (num == true) textureNum = 1;
            else textureNum = 2;


            if(currentPosition.X < newPosition.X)
            {
                newTexture = TextureManager.GetTextureByName(texturePath, "Right" + textureNum); // Right
            }
            else if(currentPosition.X > newPosition.X)
            {
                newTexture = TextureManager.GetTextureByName(texturePath, "Left" + textureNum); //Left
            }
            else if(currentPosition.Y > newPosition.Y)
            {
                newTexture = TextureManager.GetTextureByName(texturePath, "Up" + textureNum);
            }
            else if(currentPosition.Y < newPosition.Y)
            {
                newTexture = TextureManager.GetTextureByName(texturePath, "Down" + textureNum);
            }
            else
            {
                newTexture = TextureManager.GetTextureByName(texturePath, "Idle" + textureNum);
            }

            return newTexture;
        }

        public static Texture2D AnimateIdle(bool num, string texturePath)
        {
            Texture2D newTexture;
            int textureNum;

            if (num == true) textureNum = 1;
            else textureNum = 2;


            newTexture = TextureManager.GetTextureByName(texturePath, "Idle" + textureNum);

            return newTexture;
        }





    }
}
