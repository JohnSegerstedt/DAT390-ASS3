using System;
using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class SpawningBehaviour : MonoBehaviour {

	[SerializeField] private float spawnCooldown = 1f;
	[SerializeField] private Transform spawningPoint;
	[SerializeField] private PoolObject spawnObject;

	private float currentCooldown = 0f;
	private GamePiece gamePiece;

	private void Start() {
		gamePiece = GetComponent<GamePiece>();
	}

	private void Update() {
		if(currentCooldown < spawnCooldown) currentCooldown += Time.deltaTime;
		else Spawn();
	}

	private void Spawn(){
		currentCooldown = 0f;
		GameObject spawnedObject = ObjectPoolManager.Instance.GetGameObject(spawnObject);
        spawnedObject.transform.position = spawningPoint.position;
        spawnedObject.transform.forward = gamePiece.transform.forward;
		spawnedObject.transform.rotation = UnityEngine.Random.rotation;
	}

}
