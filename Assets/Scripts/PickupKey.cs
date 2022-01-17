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

    // Update is called once per frame
    void Update()
    {
    
        transform.Rotate(Vector3.right * Time.deltaTime * speed);
    
        inRange = Physics.CheckSphere(transform.position, gizmo, player);
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            playerH.GetComponent<CharacterHealth>().KeyPickup(1);
            keySpawn.GetComponent<SpawnKey>().stopKeySpawn(1);
            
            gameObject.SetActive(false);
            
            //set thingie back to false
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmo);
        
        
    }
}
