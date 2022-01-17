using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RespawnPoint : MonoBehaviour
{
    public float gizmo;
    
    public static RespawnPoint SharedInstance;
    public List<GameObject> playerList;
    public GameObject playerPool;
    public int amountToPool;
    
    void Awake()
    {
        SharedInstance = this;
    }
    
    void Start()
    {
        playerList = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(playerPool);
            tmp.SetActive(false);
            playerList.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!playerList[i].activeInHierarchy)
            {
                return playerList[i];
            }
        }
        return null;
    }
    
 
    
  
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,gizmo);
    }
}


