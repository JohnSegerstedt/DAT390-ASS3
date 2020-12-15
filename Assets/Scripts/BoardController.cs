using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
[ExecuteInEditMode]
public class BoardController : MonoBehaviour
{
    public Vector2Int boardSize;
    public GameObject boardTile;
    public GameObject lawnMowerPrefab;
	public GameObject fencePrefab;
	public GameObject outOfBoundsPrefab;
    public float showUpAnimationSecs;

    private Grid mGrid;
    private Plane mLocalPlane;

    private Vector2Int mMinCell;
    private Vector2Int mMaxCell;

    private Dictionary<Vector2Int, GameObject> mPlants = new Dictionary<Vector2Int, GameObject>();
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
        if (!Application.isPlaying) return;
        mShowedUpTime = Time.time;
        for (var i = mMinCell.x; i <= mMaxCell.x; i++)
        {
            SpawnInCell(lawnMowerPrefab, new Vector2Int(i, -1));
        }
		SpawnInCell(fencePrefab, new Vector2Int((mMaxCell.x-1)/2, -2));
		SpawnInCell(outOfBoundsPrefab, new Vector2Int((mMaxCell.x-1)/2, -3));
		SpawnInCell(outOfBoundsPrefab, new Vector2Int((mMaxCell.x-1)/2, boardSize.y+3));
    }

    public Vector2Int MinCell => mMinCell;
    public Vector2Int MaxCell => mMaxCell;

    public bool Project(Vector3 pixelCoords, out Vector2Int cell)
    {
        return Project(Camera.main.ScreenPointToRay(pixelCoords), out cell);
    }

    public bool Project(Vector2 pixelCoords, out Vector2Int cell)
    {
        return Project(Camera.main.ScreenPointToRay(pixelCoords), out cell);
    }

    private bool Project(Ray ray, out Vector2Int cell)
    {
        cell = Vector2Int.zero;
        ray = new Ray(
            transform.InverseTransformPoint(ray.origin),
            transform.InverseTransformDirection(ray.direction)
        );

        if (!mLocalPlane.Raycast(ray, out var enter)) return false;

        var point = ray.GetPoint(enter);
        var cell3 = mGrid.LocalToCell(point);
        cell = new Vector2Int(cell3.x, cell3.z);
        return true;
    }

    public GameObject SpawnInCell(GameObject prefab, Vector2Int cell)
    {
        var pos = mGrid.GetCellCenterLocal(new Vector3Int(cell.x, 0, cell.y)) - Vector3.up * mGrid.cellSize.y * 0.5f;
        return Instantiate(prefab,
            transform.TransformPoint(pos) + prefab.transform.position,
            transform.rotation * prefab.transform.localRotation,
            transform);
    }

    public bool TryToPlant(GameObject prefab, Vector2Int cell)
    {
        if (cell.x >= mMinCell.x && cell.x <= mMaxCell.x &&
            cell.y >= mMinCell.y && cell.y <= mMaxCell.y)
        {
            if (!mPlants.ContainsKey(cell))
            {
                var plant = SpawnInCell(prefab, cell);
                mPlants.Add(cell, plant);
                plant.GetComponent<GamePiece>().OnDeath += () => mPlants.Remove(cell);
                return true;
            }
        }
        return false;
    }

    private void ShowUpAnimation()
    {
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
    }

    private void Update()
    {
        if (!Application.isPlaying) return;
        ShowUpAnimation();
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
