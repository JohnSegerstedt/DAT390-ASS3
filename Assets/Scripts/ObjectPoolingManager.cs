using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    public GameObject gameObjectPrefab; // todo, currently hard coded to pea
    public int numOfGameObjects = 2;

    private List<GameObject> gameObjects;
    
    void Awake()
    {
        instance = this;

        //Preload gameObjects
        gameObjects = new List<GameObject>(numOfGameObjects);

        for(int i = 0; i < numOfGameObjects; i++)
        {
            GameObject prefabInstance = Instantiate(gameObjectPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            gameObjects.Add(prefabInstance);
        }
    }

   public GameObject GetGameObject()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (!gameObject.activeInHierarchy)
            {
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
