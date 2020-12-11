using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ObjectPoolManager))]
public class ObjectPool : MonoBehaviour {

    [SerializeField] private GameObject gameObjectPrefab;
    [SerializeField] private int numOfGameObjects = 2;
    [SerializeField] private PoolObject poolType;

    private List<GameObject> gameObjects;
    
    void Awake(){
        gameObjects = new List<GameObject>(numOfGameObjects);

        for(int i = 0; i < numOfGameObjects; i++){
            GameObject prefabInstance = Instantiate(gameObjectPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            gameObjects.Add(prefabInstance);
        }
    }
	
	public PoolObject GetPoolType(){
		return poolType;
	}

   public GameObject GetGameObject(){
		
        foreach (GameObject gameObject in gameObjects){
            if (!gameObject.activeInHierarchy){
                gameObject.SetActive(true);
                return gameObject;
            }
        }
		
        GameObject prefabInstance = Instantiate(gameObjectPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.SetActive(false);
        gameObjects.Add(prefabInstance);

        return prefabInstance;
    }
}
