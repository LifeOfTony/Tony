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

        private static Microsoft.Xna.Framework.Content.ContentManager Content;


        public static void setContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            Content = content;
        }

        public static Texture2D AnimatePlayer(Vector2 currentPosition, Vector2 newPosition, int textureNum)
        {
            Texture2D newTexture;

            if(currentPosition.X < newPosition.X)
            {
                
            }
            else if(currentPosition.X > newPosition.X)
            {
                //newTexture = Left
            }
            else if(currentPosition.Y < newPosition.Y)
            {
                //newTexture = Up
            }
            else if(currentPosition.Y > newPosition.Y)
            {
                //newTexture = Down
            }
            else
            {
                //newTexture = Idle
            }



            return newTexture;
        }



    }
}
