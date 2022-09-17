using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AStarPathfinding : MonoBehaviour
{
    private GameGrid grid;

    public void Init(GameGrid _grid)
    {
        grid = _grid;
    }

    public List<Vector3> GeneratePath(Vector3 StartingPosition, Vector3 Destination, bool DiagonalMovement, Vector2 Offset)
    {
        List<Vector3> Waypoints = new List<Vector3>();
        Vector2Int Start = grid.WorldToGrid(StartingPosition);
        Vector2Int Finish = grid.WorldToGrid(Destination);
        Stack<Vector2Int> V2Waypoints;
        if (SearchCell(new Node(Finish, 0)) == false)
        {
            return new List<Vector3>();
        }
        if (DiagonalMovement)
        {
            V2Waypoints = AstarDiagonal(Start, Finish);
        }
        else V2Waypoints = Astar(Start, Finish);
        foreach (Vector2Int Waypoint in V2Waypoints)
        {
            Vector2Int Point = new Vector2Int(Waypoint.x, Waypoint.y);
            Waypoints.Add(grid.GridToWorld(Point) + new Vector3(Offset.x, Offset.y, 0));
        }
        return Waypoints;
    }
    private Stack<Vector2Int> Astar(Vector2Int StartingPosition, Vector2Int Destination)
    {
        List<Vector2Int> Directions = new List<Vector2Int>();
        Directions.Add(new Vector2Int(1, 0));
        Directions.Add(new Vector2Int(-1, 0));
        Directions.Add(new Vector2Int(0, 1));
        Directions.Add(new Vector2Int(0, -1));
        Stack<Vector2Int> Waypoints = new Stack<Vector2Int>();
        Node CurrentNode = new Node(StartingPosition, 0);
        List<Node> OpenNodes = new List<Node>();
        List<Node> ClosedNodes = new List<Node>();
        OpenNodes.Add(CurrentNode);
        while (OpenNodes.Count > 0)
        {
            CurrentNode = GetLowestCostNode(OpenNodes);
            if (CurrentNode.Position != Destination)
            {
                foreach (Vector2Int offset in Directions)
                {
                    Node node = new Node(CurrentNode.Position + offset, CurrentNode.Gcost + 10);
                    node.PreviousNode = CurrentNode;
                    node.CalculateH(Destination);
                    node.CalculateF();
                    if (SearchCell(node))
                    {
                        if (OpenNodes.Contains(node))
                        {
                            Node listnode = OpenNodes.Find(n => n.Equals(node));
                            if (listnode.Gcost > node.Gcost)
                            {
                                OpenNodes.Remove(listnode);
                                OpenNodes.Add(node);
                            }
                        }
                        else if (!ClosedNodes.Contains(node)) OpenNodes.Add(node);
                    }
                }
                ClosedNodes.Add(CurrentNode);
                OpenNodes.Remove(CurrentNode);
            }
            else
            {
                while (CurrentNode != null)
                {
                    Waypoints.Push(CurrentNode.Position);
                    CurrentNode = CurrentNode.PreviousNode;
                }
                break;
            }
        }
        return Waypoints;
    }
    private Stack<Vector2Int> AstarDiagonal(Vector2Int StartingPosition, Vector2Int Destination)
    {
        List<Vector2Int> Directions = new List<Vector2Int>();
        List<Vector2Int> DirectionsDiagonal = new List<Vector2Int>();
        Directions.Add(new Vector2Int(1, 0));
        Directions.Add(new Vector2Int(-1, 0));
        Directions.Add(new Vector2Int(0, 1));
        Directions.Add(new Vector2Int(0, -1));
        DirectionsDiagonal.Add(new Vector2Int(1, 1));
        DirectionsDiagonal.Add(new Vector2Int(1, -1));
        DirectionsDiagonal.Add(new Vector2Int(-1, 1));
        DirectionsDiagonal.Add(new Vector2Int(-1, -1));
        Stack<Vector2Int> Waypoints = new Stack<Vector2Int>();
        Node CurrentNode = new Node(StartingPosition, 0);
        List<Node> OpenNodes = new List<Node>();
        List<Node> ClosedNodes = new List<Node>();
        OpenNodes.Add(CurrentNode);
        while (OpenNodes.Count > 0)
        {
            CurrentNode = GetLowestCostNode(OpenNodes);
            if (CurrentNode.Position != Destination)
            {
                foreach (Vector2Int offset in Directions)
                {
                    Node node = new Node(CurrentNode.Position + offset, CurrentNode.Gcost + 10);
                    node.PreviousNode = CurrentNode;
                    node.CalculateHDiagonal(Destination);
                    node.CalculateF();
                    if (SearchCell(node))
                    {
                        if (OpenNodes.Contains(node))
                        {
                            Node listnode = OpenNodes.Find(n => n.Equals(node));
                            if (listnode.Gcost > node.Gcost)
                            {
                                OpenNodes.Remove(listnode);
                                OpenNodes.Add(node);
                            }
                        }
                        else if (!ClosedNodes.Contains(node)) OpenNodes.Add(node);
                    }
                }
                foreach (Vector2Int offset in DirectionsDiagonal)
                {
                    Node node = new Node(CurrentNode.Position + offset, CurrentNode.Gcost + 14);
                    node.PreviousNode = CurrentNode;
                    node.CalculateHDiagonal(Destination);
                    node.CalculateF();
                    if (SearchCell(node))
                    {
                        if (OpenNodes.Contains(node))
                        {
                            Node listnode = OpenNodes.Find(n => n.Equals(node));
                            if (listnode.Gcost > node.Gcost)
                            {
                                OpenNodes.Remove(listnode);
                                OpenNodes.Add(node);
                            }
                        }
                        else if (!ClosedNodes.Contains(node)) OpenNodes.Add(node);
                    }
                }
                ClosedNodes.Add(CurrentNode);
                OpenNodes.Remove(CurrentNode);
            }
            else
            {
                while (CurrentNode != null)
                {
                    Waypoints.Push(CurrentNode.Position);
                    CurrentNode = CurrentNode.PreviousNode;
                }
                break;
            }
        }
        return Waypoints;
    }
    private bool SearchCell(Node cell)
    {
        if (grid.SearchGrid(cell.Position, false) == 1) return true;
        return false;
    }
    private Node GetLowestCostNode(List<Node> List)
    {
        Node node = List[0];
        foreach (Node n in List)
        {
            if (n.Fcost < node.Fcost) node = n;
            else if (n.Fcost == node.Fcost)
                if (n.Hcost < node.Hcost) node = n;
        }
        return node;
    }

}

public class Node
{
    public Vector2Int Position;
    public int Hcost;
    public int Gcost;
    public int Fcost;
    public Node PreviousNode;
    public Node()
    {
        Position = Vector2Int.zero;
        Hcost = 0;
        Gcost = 0;
        Fcost = 0;
        PreviousNode = null;
    }
    public Node(Vector2Int position, int gcost)
    {
        Position = position;
        Hcost = 0;
        Gcost = gcost;
        Fcost = 0;
        PreviousNode = null;
    }
    public void CalculateH(Vector2Int Destination)
    {
        Hcost = Mathf.Abs(Destination.x - Position.x) + Mathf.Abs(Destination.y - Position.y);
        Hcost *= 10;
    }
    public void CalculateHDiagonal(Vector2Int Destination)
    {
        int X = Mathf.Abs(Destination.x - Position.x);
        int Y = Mathf.Abs(Destination.y - Position.y);
        Hcost = Mathf.Min(X, Y) * 14 + (Mathf.Max(X, Y) - Mathf.Min(X, Y)) * 10;
    }
    public void CalculateF()
    {
        Fcost = Gcost + Hcost;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Node objAsNode = obj as Node;
        if (objAsNode == null) return false;
        else return Equals(objAsNode);
    }
    public bool Equals(Node other)
    {
        if (other == null) return false;
        else return (this.Position.Equals(other.Position));
    }
    public override int GetHashCode()
    {
        return this.Position.GetHashCode();
    }
}
