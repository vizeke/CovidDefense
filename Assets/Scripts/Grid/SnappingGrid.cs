using UnityEngine;

public class SnappingGrid : MonoBehaviour
{
    public int columns;
    public int rows;
    public bool isClosed = false;

    public bool isDisplayDebugActive = false;

    public Transform origin { get => transform; }
    public Transform end;

    /**
     *  origin
     *  + ------------- (width)
     *  |              |
     *  |______________|
     *  (height)
     * 
     */

    public float Width { get => Mathf.Abs(origin.position.x - end.position.x); }
    public float Height { get => Mathf.Abs(origin.position.z - end.position.z); }

    public float CellWidth { get => Width / columns; }
    public float CellHeight { get => Height / rows; }

    public Vector3 SnapPositionToGrid(Vector3 position)
    {
        if (!IsInGrid(position))
        {
            if (!isClosed) return position;
        }

        var columnIndex = (int)((position.x - origin.position.x) / CellWidth);
        var rowIndex = (int)((origin.position.z - position.z) / CellHeight);

        if (isClosed)
        {
            columnIndex = Mathf.Max(0, Mathf.Min(columnIndex, columns - 1));
            rowIndex = Mathf.Max(0, Mathf.Min(rowIndex, rows - 1));
        }

        return PlaceInGrid(rowIndex, columnIndex);
    }

    public bool IsInGrid(Vector3 position)
    {
        if (position.x < origin.position.x) return false;
        if (position.x > end.position.x) return false;
        if (position.z > origin.position.z) return false;
        if (position.z < end.position.z) return false;

        return true;
    }

    public Vector3 PlaceInGrid(int row, int column)
    {
        return new Vector3(
            origin.position.x + (column * CellWidth),
            origin.position.y,
            origin.position.z - (row * CellHeight)
        );
    }

    public void Update()
    {
        if (!isDisplayDebugActive) return;
        
        for (var i = 0; i < rows; i += 1)
        {
            for (var j = 0; j < columns; j += 1)
            {
                var start = PlaceInGrid(i, j);
                Debug.DrawLine(start, new Vector3(start.x, start.y + 1f, start.z), Color.red);
            }
        }
    }
}
