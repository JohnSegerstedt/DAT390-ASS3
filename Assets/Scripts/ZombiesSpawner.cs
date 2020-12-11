using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoardController))]
class ZombiesSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float increaseSquadMaxPeriod;
    public float increaseSquadMinPeriod;
    public float squadSectionDelay;
    public float minSquadCooldown;
    public float maxSquadCooldown;
    private BoardController mBoardController;

    private float mNextSquadTime;
    private int mMinSquadSize;
    private int mMaxSquadSize;

    private void ProgramSquad()
    {
        mNextSquadTime = Time.time + Random.Range(minSquadCooldown, maxSquadCooldown);
    }

    private IEnumerator SpawnSquad()
    {
        var squadSize = Random.Range(mMinSquadSize, mMaxSquadSize);
        var places = new List<Vector2Int>();
        var min = mBoardController.MinCell;
        var max = mBoardController.MaxCell;
        for (var j = min.x; j <= max.x; j++)
        {
            places.Add(new Vector2Int(j, max.y + 1));
        }
        for (var i = 0; i < squadSize; i += mBoardController.boardSize.x)
        {
            var copyPlaces = new List<Vector2Int>(places);
            for (var j = i; j < squadSize && copyPlaces.Count > 0; j++)
            {
                var idx = Random.Range(0, copyPlaces.Count - 1);
                var cell = copyPlaces[idx];
                copyPlaces.RemoveAt(idx);
                mBoardController.SpawnInCell(zombiePrefab, cell);
            } 
            yield return new WaitForSeconds(squadSectionDelay);
        }
    }

    private void Awake()
    {
        mBoardController = GetComponent<BoardController>();
        mMinSquadSize = 1;
        mMaxSquadSize = 1;
        ProgramSquad();
    }

    private void Update()
    {
        mMinSquadSize = Mathf.CeilToInt(Time.time / increaseSquadMinPeriod);
        mMaxSquadSize = Mathf.CeilToInt(Time.time / increaseSquadMaxPeriod);
        if (Time.time > mNextSquadTime)
        {
            StartCoroutine(SpawnSquad());
            ProgramSquad();
        }
    }
}
