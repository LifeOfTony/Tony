using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tony
{
    class Camera
    {
        private float screenWidth;
        private float screenHeight;
        private float tileWidth;
        private float tileHeight;
        private float zoom;

        public Camera(float screenWidth, float screenHeight, float zoom)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.zoom = zoom;
            tileHeight = 32.0f;
            tileWidth = 32.0f;
        }

        public Matrix Transform { get; private set; }

        public void follow(Player target)
        {
            Vector2 spritePosition = target.getPosition();

            Transform =
                Matrix.CreateScale( new Vector3(zoom, zoom, 1.0f)) 
                * Matrix.CreateRotationZ(0)
                * Matrix.CreateTranslation( (-spritePosition.X - (tileWidth/2)) + (screenWidth / 2),
                               (-spritePosition.Y - (tileHeight/2)) + (screenHeight / 2),
                               0);

        }

    }
}
