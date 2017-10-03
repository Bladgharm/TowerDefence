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

    public Vector3 SnapToGridCell(Vector3 point)
    {
        Vector3 snappedPoint = point;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                var cell = _cellsGrid[i, j];
                if (Vector3.Distance(cell.CellPosition, point) < _cellSize / 2f)
                {
                    snappedPoint = cell.CellPosition;
                }
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
