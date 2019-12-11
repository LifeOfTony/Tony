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

        private Vector2 position;
        private float gNumber;
        private float hNumber;
        private float fNumber;
        private List<GridNode> connections;
        private GridNode parent;


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
            set
            {
                fNumber = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public GridNode Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public List<GridNode> Connections
        {
            get
            {
                return connections;
            }
        }

        public GridNode(Vector2 position)
        {
            this.position = position;
            connections = new List<GridNode>();
        }


        public void AddConnection(GridNode neighbour)
        {
            connections.Add(neighbour);
        }


        public void FindHNumber(Vector2 endPosition)
        {
            hNumber = (float)Math.Sqrt(Math.Pow((endPosition.X - position.X), 2) + Math.Pow((endPosition.Y - position.Y), 2));
        }


        public float FindGNumber(GridNode node)
        {
            float newGNumber = node.GNumber + (float)Math.Sqrt(Math.Pow((node.position.X - position.X), 2) + Math.Pow((node.position.Y - position.Y), 2));
            return newGNumber;
        }


    }
}
