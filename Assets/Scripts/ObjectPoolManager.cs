using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour {
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance { get { return instance; } }

	private ObjectPool[] objectPools;

	private void Awake() {
        instance = this;
	}

	private void Start() {
		objectPools = GetComponents<ObjectPool>();
	}

	public GameObject GetGameObject(PoolObject poolObject){
		foreach(ObjectPool objectPool in objectPools) if(objectPool.GetPoolType() == poolObject) return objectPool.GetGameObject();
		Debug.LogError("No ObjectPool for type: '"+poolObject.ToString()+"'");
		return null;
	}
}
