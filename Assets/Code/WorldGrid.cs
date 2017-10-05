using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField]
    private int _width = 1;
    [SerializeField]
    private int _length = 1;
    [SerializeField]
    private float _spacing = 1f;
    [SerializeField]
    private float _cellSize = 1f;

    private GridCell[,] _cellsGrid;

    private Vector3 _lastCellPosition = Vector3.zero;

    private void Awake()
    {
        _cellsGrid = new GridCell[_width,_length];
    }

    private void Start()
    {
        RebuildGrid();
    }

    public void RebuildGrid()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                var position = new Vector3(i * (_cellSize + _spacing), 0, j * (_cellSize + _spacing));
                _cellsGrid[i,j] = new GridCell(GridCellState.Free, position);
            }
        }
    }

    public void FillGridCellAtPoint(Vector3 point)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                var cell = _cellsGrid[i, j];
                if (Vector3.Distance(cell.CellPosition, point) < _cellSize / 2f)
                {
                    _cellsGrid[i,j].UpdateCellState(GridCellState.Filled);
                }
            }
        }
    }

    public GridCell GetCellAtPoint(Vector3 point)
    {
        GridCell selectedCell = null;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                var cell = _cellsGrid[i, j];
                if (Vector3.Distance(cell.CellPosition, point) < _cellSize / 2f)
                {
                    selectedCell = cell;
                }
            }
        }
        return selectedCell;
    }

    public Vector3 SnapToGridCell(Vector3 point)
    {
        Vector3 snappedPoint = point;
        var cell = GetCellAtPoint(snappedPoint);
        if (cell != null)
        {
            return cell.CellPosition;
        }
        return snappedPoint;
    }

    public Vector3 SnapToGridCellEdge(Vector3 point, Vector3 directionVector)
    {
        Vector3 snappedPoint = point;
        var cell = GetCellAtPoint(snappedPoint);
        if (cell != null)
        {
            if (Math.Abs(directionVector.x) > 0)
            {
                float pointOffset;
                if (point.x > cell.CellPosition.x)
                {
                    pointOffset = cell.CellPosition.x + _cellSize / 2;
                }
                else
                {
                    pointOffset = cell.CellPosition.x - _cellSize / 2;
                }
                snappedPoint = new Vector3(pointOffset, cell.CellPosition.y, cell.CellPosition.z);
            }
            if (Math.Abs(directionVector.z) > 0)
            {
                float pointOffset;
                if (point.z > cell.CellPosition.z)
                {
                    pointOffset = cell.CellPosition.z + _cellSize / 2;
                }
                else
                {
                    pointOffset = cell.CellPosition.z - _cellSize / 2;
                }
                snappedPoint = new Vector3(cell.CellPosition.x, cell.CellPosition.y, pointOffset);
            }
        }
        return snappedPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (_cellsGrid != null && _cellsGrid.Length > 0)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    if (_cellsGrid[i, j].CellState == GridCellState.Free)
                    {
                        Gizmos.color = Color.green;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawCube(_cellsGrid[i,j].CellPosition, new Vector3(_cellSize, 0.2f, _cellSize));
                }
            }
        }
    }
}

public class GridCell
{
    private GridCellState _cellState;
    private Vector3 _cellPosition;

    public GridCellState CellState
    {
        get { return _cellState; }
    }

    public Vector3 CellPosition
    {
        get { return _cellPosition; }
    }

    public GridCell(GridCellState cellState, Vector3 position)
    {
        _cellState = cellState;
        _cellPosition = position;
    }

    public void UpdateCellState(GridCellState cellState)
    {
        _cellState = cellState;
    }
}

public enum GridCellState
{
    Null,
    Free,
    Filled
}
