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
        private static List<GridNode> allNodes;
        private static List<GridNode> closedList;
        private static GridNode startNode;
        private static GridNode endNode;


        public static Queue<Vector2> FindPath(Vector2 position, Vector2 endPosition)
        {
            startPosition = new Vector2((position.X/32), (position.Y/32));
            finalPosition = new Vector2((endPosition.X / 32), (endPosition.Y / 32));
            mapWidth = ObjectManager.Instance.CurrentLevel.MapWidth;
            mapHeight = ObjectManager.Instance.CurrentLevel.MapHeight;
            allNodes = new List<GridNode>();
            CreateGrid();
            FindConnections();
            AStarSearch();
            var shortestpath = new List<GridNode>();
            shortestpath.Add(endNode);
            BuildShortestPath(shortestpath, endNode);
            shortestpath.Reverse();
            return convertToPoints(shortestpath);

        }


        private static Queue<Vector2> convertToPoints(List<GridNode> path)
        {
            Queue<Vector2> pathPoints = new Queue<Vector2>(); 
            foreach (GridNode node in path)
            {
                Vector2 nextPosition = new Vector2((node.Position.X * 32), (node.Position.Y * 32));
                pathPoints.Enqueue(nextPosition);
            }  
            return pathPoints;
        }





        private static void BuildShortestPath(List<GridNode> path, GridNode node)
        {
            if (node.Parent == startNode) return;
            path.Add(node.Parent);
            BuildShortestPath(path, node.Parent);
        }

        private static void AStarSearch()
        {
            startNode.GNumber = 0;
            var openList = new List<GridNode>();
            closedList = new List<GridNode>();
            openList.Add(startNode);
            do
            {
                openList = openList.OrderBy(x => (x.GNumber + x.HNumber)).ToList();
                var node = openList.First();
                openList.Remove(node);
                closedList.Add(node);
                if (node == endNode) return; 
                foreach (var neighbour in node.Connections)
                {
                    if(!closedList.Contains(neighbour))
                    {
                        float newFNumber = neighbour.HNumber + neighbour.FindGNumber(node);
                        if (!openList.Contains(neighbour))
                        {
                            neighbour.Parent = node;
                            neighbour.FNumber = newFNumber;
                            openList.Add(neighbour);
                        }
                        else if (neighbour.FNumber > newFNumber)
                        {
                            neighbour.Parent = node;
                            neighbour.FNumber = newFNumber;
                        }
                    }
                }
                

            } while (openList.Any());
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
                    bool colliding = false;
                    foreach(Collider c in ObjectManager.Instance.CurrentLevel.Collidables)
                    {
                        Vector2 cPosition = new Vector2(c.getPosition().X / 32, c.getPosition().Y / 32);
                        if (currentPosition == cPosition) colliding = true;
                    }
                    if (!colliding)
                    {
                        GridNode currentNode;
                        currentNode = new GridNode(currentPosition);
                        if (currentPosition == startPosition)
                        {
                            startNode = currentNode;
                        }
                        else if (currentPosition == finalPosition)
                        {
                            endNode = currentNode;
                        }
                        currentNode.FindHNumber(finalPosition);
                        allNodes.Add(currentNode);
                    }
                } 
            }

        }
    }
}
