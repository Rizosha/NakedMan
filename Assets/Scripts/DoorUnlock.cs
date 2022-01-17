using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public float gizmo;
    private bool inRange;
    public LayerMask player;
     public GameObject playerKey;
    public bool hasKey ;
     CharacterHealth ch;
   
    void Start()
    {
        playerKey = GameObject.Find("Player");

    }
    
    void Update()
    {
        
        inRange = Physics.CheckSphere(transform.position, gizmo, player);
        if (inRange && Input.GetKeyDown(KeyCode.E) && playerKey.GetComponent<CharacterHealth>().hasAKey)
        {
            Destroy(gameObject);
       
        }

       
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmo);
        
        
    }
}
