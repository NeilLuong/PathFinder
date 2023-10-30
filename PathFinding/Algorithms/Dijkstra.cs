using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class Dijkstra
    {
        // Performs Dijkstra's algorithm and returns nodes in the order they were visited.
        // It also sets the 'PreviousNode' property on nodes, which allows backtracking to determine the shortest path.
        public static List<CellViewModel> PerformDijkstra(
            List<List<CellViewModel>> grid,
            StartCellViewModel startNode,
            EndCellViewModel finishNode)
        {
            var visitedNodesInOrder = new List<CellViewModel>();
            startNode.Distance = 0;

            var unvisitedNodes = GetAllNodes(grid).ToList();

            while (unvisitedNodes.Any())
            {
                SortNodesByDistance(unvisitedNodes);

                var closestNode = unvisitedNodes[0];
                unvisitedNodes.RemoveAt(0);

                // Skip if the node is an obstacle/wall
                if (closestNode.IsWall) continue;

                // If the closest node's distance is infinity, we're trapped and halt the algorithm
                if (closestNode.Distance == int.MaxValue) return visitedNodesInOrder;

                closestNode.IsVisited = true;
                visitedNodesInOrder.Add(closestNode);

                if (closestNode == finishNode) return visitedNodesInOrder;

                UpdateUnvisitedNeighbors(closestNode, grid);
            }

            return visitedNodesInOrder;
        }

        // Sorts the list of nodes by their distance in ascending order
        private static void SortNodesByDistance(List<CellViewModel> unvisitedNodes)
        {
            unvisitedNodes.Sort((nodeA, nodeB) => nodeA.Distance.CompareTo(nodeB.Distance));
        }

        // Updates the distance and previous node for all unvisited neighbors of the given node
        private static void UpdateUnvisitedNeighbors(CellViewModel node, List<List<CellViewModel>> grid)
        {
            var unvisitedNeighbors = GetUnvisitedNeighbors(node, grid);
            foreach (var neighbor in unvisitedNeighbors)
            {
                neighbor.Distance = node.Distance + 1;
                neighbor.PreviousNode = node;
            }
        }

        // Fetches the unvisited neighboring nodes of the given node
        private static IEnumerable<CellViewModel> GetUnvisitedNeighbors(CellViewModel node, List<List<CellViewModel>> grid)
        {
            int row = node.Row;
            int col = node.Col;
            var neighbors = new List<CellViewModel>();

            if (row > 0) neighbors.Add(grid[row - 1][col]);
            if (row < grid.Count - 1) neighbors.Add(grid[row + 1][col]);
            if (col > 0) neighbors.Add(grid[row][col - 1]);
            if (col < grid[0].Count - 1) neighbors.Add(grid[row][col + 1]);

            return neighbors.Where(neighbor => !neighbor.IsVisited);
        }

        // Fetches all nodes from the grid
        private static IEnumerable<CellViewModel> GetAllNodes(List<List<CellViewModel>> grid)
        {
            return grid.SelectMany(row => row);
        }

        // Determines the shortest path by backtracking from the finish node using the 'PreviousNode' property
        public static List<CellViewModel> GetNodesInShortestPathOrder(EndCellViewModel finishNode)
        {
            var nodesInShortestPathOrder = new List<CellViewModel>();
            var currentNode = finishNode as CellViewModel;

            while (currentNode != null)
            {
                nodesInShortestPathOrder.Insert(0, currentNode);
                currentNode = currentNode.PreviousNode;
            }

            return nodesInShortestPathOrder;
        }
    }
}
