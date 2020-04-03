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
        private float zoom;

        public Matrix Transform { get; private set; }

        public Camera(float screenWidth, float screenHeight, float zoom)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.zoom = zoom;
        }


        public void changeZoom(float zoomLevel)
        {
            zoom = zoomLevel;
        }

        public void follow(Player target)
        {
            Vector2 spritePosition = target.getPosition();
            Vector2 spriteSize = target.getSize();

                Transform = Matrix.CreateScale( new Vector3(zoom, zoom, 1.0f)) 
                * Matrix.CreateRotationZ(0)
                * Matrix.CreateTranslation( (((-(spritePosition.X + (spriteSize.X/2))) * zoom) + (screenWidth / 2)),
                               (((-(spritePosition.Y + (spriteSize.Y/ 2))) * zoom) + (screenHeight / 2)),
                               0);


        }

    }
}
