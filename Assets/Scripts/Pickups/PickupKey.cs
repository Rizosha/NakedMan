using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    public float gizmo;
    private bool inRange;
    public LayerMask player;
    [SerializeField] public GameObject playerH;
    [SerializeField] public GameObject keySpawn;
    public float speed = 10f;
    void Start()
    {
        playerH = GameObject.Find("Player");
        keySpawn = GameObject.Find("KeySpawn");
    }
    
    void Update()
    {
        // rotates the key
        transform.Rotate(Vector3.right * Time.deltaTime * speed);
    
        // in range check while pressing E
        inRange = Physics.CheckSphere(transform.position, gizmo, player);
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            //adds a key and stops the key from spawning again
            playerH.GetComponent<CharacterHealth>().KeyPickup(1);
            keySpawn.GetComponent<SpawnKey>().stopKeySpawn(1);
            
            //set pooled object to false
            gameObject.SetActive(false);
        }
    }
    
    // gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmo);
   
    }
}
