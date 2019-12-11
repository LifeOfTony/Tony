using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    class GridNode
    {

        private bool isStart = false;
        private Vector2 position;
        private float gNumber;
        private float hNumber;
        private float fNumber;
        private List<GridNode> connections;


        public float GNumber
        {
            get
            {
                return gNumber;
            }
            set
            {
                gNumber = value;
            }
        }

        public float HNumber
        {
            get
            {
                return hNumber;
            }
        }

        public float FNumber
        {
            get
            {
                return fNumber;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public GridNode(Vector2 position, bool isStart)
        {
            this.position = position;
            this.isStart = isStart;
            connections = new List<GridNode>();
        }

        public GridNode(Vector2 position) : this(position, false)
        {

        }


        public void AddConnection(GridNode neighbour)
        {
            connections.Add(neighbour);
        }



        public void FindHNumber(Vector2 endPosition)
        {
            hNumber = (float)Math.Sqrt(Math.Pow((endPosition.X - position.X), 2) + Math.Pow((endPosition.Y - position.Y), 2));
        }



        public void FindGNumber(Vector2 startPosition)
        {
            gNumber = (float)Math.Sqrt(Math.Pow((startPosition.X - position.X), 2) + Math.Pow((startPosition.Y - position.Y), 2));
        }



        public void FindFNumber()
        {
            fNumber = gNumber + hNumber;
        }

    }
}
