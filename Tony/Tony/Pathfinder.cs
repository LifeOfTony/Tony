using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tony
{
    public static class Pathfinder
    {
        private static int mapWidth;
        private static int mapHeight;
        private static Vector2 startPosition;
        private static Vector2 finalPosition;
        private static int[,] grid;
        private static List<GridNode> allNodes;
        private static GridNode startNode;

        public static Queue<Vector2> FindPath(Vector2 startPostition, Vector2 endPosition)
        {
            startPosition = startPostition;
            finalPosition = endPosition;
            mapWidth = ObjectManager.Instance.MapWidth;
            mapHeight = ObjectManager.Instance.MapHeight;
            allNodes = new List<GridNode>();
            CreateGrid();
            var shortestpath = new Queue<Vector2>();
        }

        private static void BuildShortestPath(Queue<Vector2>)
        {

        }

        private static void AStarSearch()
        {
            startNode.GNumber = 0;
            var prioQueue = new List<GridNode>();
            prioQueue.Add(startNode);
            {
                do
                {



                } while (prioQueue.Any());
            }

        }


        public static void FindConnections()
        {
            foreach(GridNode currentNode in allNodes)
            {
                float x = currentNode.Position.X;
                float y = currentNode.Position.Y;
                foreach (GridNode compareNode in allNodes)
                {
                    if (currentNode != compareNode)
                    {
                        float cX = compareNode.Position.X;
                        float cY = compareNode.Position.Y;
                        if (
                            (cX == (x - 1) || cX == x || cX == (x + 1)) && 
                            (cY == (y - 1) || cY == y || cY == (y + 1)))
                        {
                            currentNode.AddConnection(compareNode);
                        }
                    }
                    
                }
            }
        }

        public static void CreateGrid()
        {

            for(int i = 0; i < mapWidth; i++)
            {
                for(int j = 0; j < mapHeight; j++)
                {
                    Vector2 currentPosition = new Vector2(i, j);
                    GridNode currentNode;
                    if (i == startPosition.X && j == startPosition.Y)
                    {
                        currentNode = new GridNode(currentPosition, true);
                        startNode = currentNode;
                    }
                    else
                    {
                        currentNode = new GridNode(currentPosition);
                    }
                    currentNode.FindHNumber(finalPosition);
                    allNodes.Add(currentNode);
                }
            }

        }
    }
}
