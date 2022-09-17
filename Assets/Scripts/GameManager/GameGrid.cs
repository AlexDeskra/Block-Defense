using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid
{
    private enum IDs
    {
        Empty,
        Rock1x1,
        Rock2x1,
        Rock2x2,
        Rock4x1,
    }
    private int[,] Grid;
    private Vector2Int Anchor; // [0,0] Coordonates of grid.
    private Vector2 CellSize;
    private Vector2Int TemporaryPoint;

    public GameGrid()
    {
        Grid = null;
        Anchor = Vector2Int.zero;
        CellSize = Vector2.zero;
        TemporaryPoint = Vector2Int.zero;
    }
    public GameGrid(int size, Vector2Int anchor, Vector2 cellSize)
    {
        Grid = new int[size, size];

        Anchor = anchor;
        CellSize = cellSize;
        TemporaryPoint = Vector2Int.zero;
    }
    public GameGrid(GameGrid grid)
    {
        Grid = grid.Grid;
        Anchor = grid.Anchor;
        CellSize = grid.CellSize;
        TemporaryPoint = grid.TemporaryPoint;
    }

    /// <summary>
    /// Adds object ID to grid.
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    private bool AddToGrid(Vector2Int Position, int Value)
    {
        if (Position.x < 0 || Position.y < 0)
            return false;
        Grid[Position.x, Position.y] = Value;
        return true;
    }

    /// <summary>
    /// Adds object ID to grid from world coordonates.
    /// </summary>
    /// <param name="WorldPosition"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    private bool AddToGrid(Vector3 WorldPosition, int Value)
    {
        return AddToGrid(WorldToGrid(WorldPosition), Value);
    }

    /// <summary>
    /// Searches cell in grid by grid coordonates.
    /// </summary>
    /// <param name="Position"></param>
    /// <returns></returns>
    public int SearchGrid(Vector2Int Position, bool ExcludeTemporaryPoint)
    {
        if (Position.x < 0 || Position.y < 0)
            return -1;
        if (Position == TemporaryPoint && ExcludeTemporaryPoint == false) return 0;
        return Grid[Position.x, Position.y];
    }

    /// <summary>
    /// Searches cell in grid by world coordonates.
    /// </summary>
    /// <param name="WorldPosition"></param>
    /// <returns></returns>
    public int SearchGrid(Vector3 WorldPosition, bool ExcludeTemporaryPoint)
    {
        return SearchGrid(WorldToGrid(WorldPosition), ExcludeTemporaryPoint);
    }
    /// <summary>
    /// Converts the World Coordinates to Grid Coordinates.
    /// </summary>
    /// <param name="WorldPosition"></param>
    /// <returns></returns>
    public Vector2Int WorldToGrid(Vector3 WorldPosition)
    {
        WorldPosition += new Vector3(Anchor.x, Anchor.y, 0);
        Vector2Int Position = new Vector2Int();
        float temp = new float();
        float v = Mathf.Abs(Utility.GetFloatNumber(WorldPosition.x / CellSize.x));
        if (v > .5f)
        {
            temp = (WorldPosition.x / CellSize.x) + (int)Mathf.Sign(WorldPosition.x);
            Position.x = (int)temp + Anchor.x;
        }
        else
        {
            temp = (WorldPosition.x / CellSize.x);
            Position.x = (int)temp + Anchor.x;
        }
        v = Mathf.Abs(Utility.GetFloatNumber(WorldPosition.y / CellSize.y));
        if (v > .5f)
        {
            temp = (WorldPosition.y / CellSize.y) + (int)Mathf.Sign(WorldPosition.y);
            Position.y = (int)temp + Anchor.y;
        }
        else
        {
            temp = (WorldPosition.y / CellSize.y);
            Position.y = (int)temp + Anchor.y;
        }
        return Position;
    }

    public Vector3 GridToWorld(Vector2Int LocalPosition)
    {
        Vector3 WorldPosition = new Vector3();
        WorldPosition.x = LocalPosition.x * CellSize.x + Anchor.x;
        WorldPosition.y = LocalPosition.y * CellSize.y + Anchor.y;
        return WorldPosition;
    }
    /// <summary>
    /// Returns a Vector3 which contains the position snapped on a cell of the grid.
    /// </summary>
    /// <param name="Position"></param>
    /// <returns></returns>
    public Vector3 SnapPositionToGrid(Vector3 Position)
    {
        Position.z = 0;
        Vector2Int Result = WorldToGrid(Position);
        return GridToWorld(Result);
    }
    /// <summary>
    /// Adds a Point to the grid as a temporary position. Calling the method again will overwrite the previous position.
    /// </summary>
    /// <param name="Position"></param>
    public void AddTemporaryPointToGrid(Vector3 Position)
    {
        TemporaryPoint = WorldToGrid(Position);
    }
    public void ConvertRockToGrid(GameObject Rock)
    {
        foreach (IDs id in System.Enum.GetValues(typeof(IDs)))
        {
            if (Rock.name.Contains(id.ToString()))
            {
                AddGameObjectToGrid(WorldToGrid(Rock.transform.position), (int)System.Enum.Parse(typeof(IDs), id.ToString()));
                break;
            }
        }
    }
    public void ConvertTowerToGrid(Vector3 Position)
    {
        AddGameObjectToGrid(WorldToGrid(Position), 0);
    }
    public void AddGrassTileToGrid(Vector3 Position)
    {
        AddToGrid(Position, 1);
    }
    public void AddStartingLocationToGrid(Vector3 Position)
    {
        AddToGrid(Position, 0);
    }
    private void AddGameObjectToGrid(Vector2Int Position, int Value)
    {
        switch (Value)
        {
            case 2:
                AddToGrid(Position, 0);
                AddToGrid(Position + new Vector2Int(1, 0), 0);
                break;
            case 3:
                AddToGrid(Position, 0);
                AddToGrid(Position + new Vector2Int(1, 0), 0);
                AddToGrid(Position + new Vector2Int(0, 1), 0);
                AddToGrid(Position + new Vector2Int(1, 1), 0);
                break;
            case 4:
                AddToGrid(Position, 0);
                AddToGrid(Position + new Vector2Int(1, 0), 0);
                AddToGrid(Position + new Vector2Int(2, 0), 0);
                AddToGrid(Position + new Vector2Int(3, 0), 0);
                break;
            default:
                AddToGrid(Position, 0);
                break;
        }
    }
    public int[,] getGrid() { return Grid; }
    public void setGrid(int[,] grid) { Grid = grid; }
}
