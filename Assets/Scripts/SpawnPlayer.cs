using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
         GameObject prefab = RespawnPoint.SharedInstance.GetPooledObject();
                if(prefab != null)
                {
                    prefab.transform.position = transform.position;
                    prefab.SetActive(true);
                }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
