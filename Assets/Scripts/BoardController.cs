using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
[ExecuteInEditMode]
public class BoardController : MonoBehaviour
{
    public Vector2Int boardSize;
    public GameObject boardTile;
    public GameObject prefab;
    public float showUpAnimationSecs;
    private Grid mGrid;
    private Plane mLocalPlane;

    private Vector2Int mMinCell;
    private Vector2Int mMaxCell;

    private Dictionary<Vector3Int, GameObject> mPieces = new Dictionary<Vector3Int, GameObject>();
    private GameObject[] mTiles;

    private float mShowedUpTime;

    private Vector3 GridShift
    {
        get => mGrid != null ?
            new Vector3(boardSize.x % 2 != 0 ? -mGrid.cellSize.x * 0.5f : 0f, 0f, 0f)
            : Vector3.zero;
    }

    private void Awake()
    {
        mGrid = GetComponent<Grid>();
        mLocalPlane = new Plane(Vector3.up, Vector3.zero);
        OnValidate();
    }

    private void Start()
    {
        mShowedUpTime = Time.time;
    }

    public void Update()
    {
        if (!Application.isPlaying) return;

        var elapsedTime = Time.time - mShowedUpTime;
        if (elapsedTime < showUpAnimationSecs)
        {
            var animationLength = showUpAnimationSecs * 0.5f;
            var shift = (showUpAnimationSecs * 0.5f) / mTiles.Length;
            for (var i = 0; i < mTiles.Length; i++)
            {
                var start = i * shift;
                var factor = Mathf.Clamp01((elapsedTime - start) / animationLength);
                var rotation = Quaternion.Slerp(
                    Quaternion.AngleAxis(-90, Vector3.right),
                    Quaternion.AngleAxis(90, Vector3.right),
                    factor
                );
                mTiles[i].transform.localRotation = rotation;
            }
        }

        var mouseClick = Input.GetMouseButtonDown(0);
        var touchClick = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (mouseClick || touchClick)
        {
            Ray ray;
            if (mouseClick)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            ray = new Ray(
                transform.InverseTransformPoint(ray.origin),
                transform.InverseTransformDirection(ray.direction)
            );
            if (!mLocalPlane.Raycast(ray, out var enter)) return;

            var point = ray.GetPoint(enter);

            var cell = mGrid.LocalToCell(point);
            if (cell.x >= mMinCell.x && cell.x <= mMaxCell.x &&
                cell.z >= mMinCell.y && cell.z <= mMaxCell.y)
            {
                if (!mPieces.ContainsKey(cell))
                {
                    var pos = mGrid.GetCellCenterLocal(cell) - Vector3.up * mGrid.cellSize.y * 0.5f;
                    mPieces.Add(cell, Instantiate(prefab,
                        transform.TransformPoint(pos) + prefab.transform.localPosition,
                        transform.rotation * prefab.transform.localRotation,
                        transform));
                }
            }
        }
    }

    private void OnValidate()
    {
        if (!mGrid) return;
        mMinCell = new Vector2Int(-Mathf.CeilToInt((boardSize.x - 1) / 2f), 0);
        mMaxCell = new Vector2Int((boardSize.x - 1) / 2, boardSize.y - 1);
        transform.localPosition = GridShift;

        if (!(boardTile && Application.isPlaying)) return;
        var tiles = new List<GameObject>();
        for (var x = mMinCell.x; x <= mMaxCell.x; x++)
        {
            for (var y = mMinCell.y; y <= mMaxCell.y; y++)
            {
                var pos = mGrid.GetCellCenterLocal(new Vector3Int(x, 0, y)) - Vector3.up * mGrid.cellSize.y * 0.5f;
                var tile = Instantiate(boardTile,
                    transform.TransformPoint(pos),
                    transform.rotation * Quaternion.AngleAxis(-90, Vector3.right),
                    transform
                );
                tile.transform.localScale = mGrid.cellSize;
                tiles.Add(tile);
            }
        }
        mTiles = tiles.ToArray();
    }

    private void OnDrawGizmos()
    {
        for (var x = mMinCell.x; x <= mMaxCell.x; x++)
        {
            for (var y = mMinCell.y; y <= mMaxCell.y; y++)
            {
                var pos = mGrid.CellToLocal(new Vector3Int(x, 0, y));
                var cornerA = transform.TransformPoint(pos);
                var cornerB = transform.TransformPoint(pos + new Vector3(mGrid.cellSize.x, 0, 0));
                var cornerC = transform.TransformPoint(pos + new Vector3(mGrid.cellSize.x, 0, mGrid.cellSize.z));
                var cornerD = transform.TransformPoint(pos + new Vector3(0, 0, mGrid.cellSize.z));

                Gizmos.DrawLine(cornerA, cornerB);
                Gizmos.DrawLine(cornerB, cornerC);
                Gizmos.DrawLine(cornerC, cornerD);
                Gizmos.DrawLine(cornerD, cornerA);
            }
        }
    }
}
