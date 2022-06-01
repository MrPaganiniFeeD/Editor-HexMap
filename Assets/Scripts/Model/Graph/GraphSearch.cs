using System.Collections.Generic;
using System.Linq;
using Model.HexModel;
using UnityEditor;
using UnityEngine;

namespace Model.Graph
{
    public class GraphSearch
    {
        public BFSResult BFSGetRange(HexGrid hexGrid, HexCoordinates startPoint, int movementPoints)
        {
            Dictionary<HexCoordinates, HexCoordinates?> visitedNodes = new Dictionary<HexCoordinates, HexCoordinates?>();
            Dictionary<HexCoordinates, int> costSoFar = new Dictionary<HexCoordinates, int>();
            Queue<HexCoordinates> nodesToVisit = new Queue<HexCoordinates>();

            nodesToVisit.Enqueue(startPoint);
            costSoFar.Add(startPoint, 0);
            visitedNodes.Add(startPoint, null);

            while (nodesToVisit.Count > 0)
            {
                HexCoordinates currentNode = nodesToVisit.Dequeue();
                
                foreach (Hex neighbour in hexGrid.GetHex(currentNode).Neighbours)
                {
                    HexCoordinates neighbourPosition = neighbour.Position;
                    int nodeCost = hexGrid.GetHex(neighbourPosition).BiomeType.CompareTo(4);
                    int currentCost = costSoFar[currentNode];
                    int newCost = currentCost + nodeCost;

                    if (newCost <= movementPoints)
                    {
                        if (!visitedNodes.ContainsKey(neighbourPosition))
                        {
                            visitedNodes[neighbourPosition] = currentNode;
                            costSoFar[neighbourPosition] = newCost;
                            nodesToVisit.Enqueue(neighbourPosition);
                        }
                        else if (costSoFar[neighbourPosition] > newCost)
                        {
                            costSoFar[neighbourPosition] = newCost;
                            visitedNodes[neighbourPosition] = currentNode;
                        }
                    }
                
                }
            
            }

            return new BFSResult(visitedNodes);
        }

        public static List<HexCoordinates> GeneratePathBFS(HexCoordinates current, Dictionary<HexCoordinates, HexCoordinates?> visitedNodes)
        {
            var path = new List<HexCoordinates> {current};
            while (visitedNodes[current] != null)
            {
                path.Add(visitedNodes[current].Value);
                current = visitedNodes[current].Value;
            }
            path.Reverse();
            return path.Skip(1).ToList();
        }
    }

    public struct BFSResult
    {
        private Dictionary<HexCoordinates, HexCoordinates?> _visitedNodes;

        public BFSResult(Dictionary<HexCoordinates, HexCoordinates?> visitedNodes)
        {
            _visitedNodes = visitedNodes;
        }
        
        public List<HexCoordinates> GetPathTo(HexCoordinates destination)
        {
            if (_visitedNodes.ContainsKey(destination) == false)
                return new List<HexCoordinates>();
            return GraphSearch.GeneratePathBFS(destination, _visitedNodes);
        }

        public bool IsHexCoordinatesInRange(HexCoordinates coordinates) => 
            _visitedNodes.ContainsKey(coordinates);

        public IEnumerable<HexCoordinates> GetRangeCoordinates() 
            => _visitedNodes.Keys;
    }
}