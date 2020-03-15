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

        public Camera(float screenWidth, float screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
        }

        public Matrix Transform { get; private set; }

        public void follow(Player target)
        {
            Vector2 spritePosition = target.getPosition();
            Transform = Matrix.CreateTranslation(
                -spritePosition.X,
                -spritePosition.Y,
                0) * Matrix.CreateTranslation(
                    screenWidth / 2,
                    screenHeight / 2,
                    0);

        }

    }
}
