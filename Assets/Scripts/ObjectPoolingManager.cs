using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    public GameObject peaPrefab;
    public int numOfPeas = 2;

    private List<GameObject> peas;
    
    void Awake()
    {
        instance = this;

        //Preload peas
        peas = new List<GameObject>(numOfPeas);

        for(int i = 0; i < numOfPeas; i++)
        {
            GameObject prefabInstance = Instantiate(peaPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            peas.Add(prefabInstance);
        }
    }

   public GameObject GetPea()
    {
        foreach (GameObject pea in peas)
        {
            if (!pea.activeInHierarchy)
            {
                pea.SetActive(true);
                return pea;
            }
        }

        GameObject prefabInstance = Instantiate(peaPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.SetActive(false);
        peas.Add(prefabInstance);

        return prefabInstance;
    }
}
