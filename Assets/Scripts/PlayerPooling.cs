using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPooling : MonoBehaviour
{
    public static PlayerPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    /// <summary>
    /// Unity has a good reference site... i can't explain this code other than it pools a custom amount of beat objects for me. Which is nice for your cpu and bpm :) 
    /// </summary>

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}
