using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class GroundFalling : MonoBehaviour
{ 
    [SerializeField] public Transform lastPos;
    [SerializeField] GameObject player;
    
    private float interval;
    public float saveTime;
    private bool waitingSave = true;

    public bool grounded;
    public LayerMask groundMask;

    public float gizmo;

    [SerializeField] GameObject respawn;
    public float respawnTimeElapsed;
    public float respawnTime;
    public bool respawnBool;

    [SerializeField] public GameObject groundCheck;

    private GameObject playerH;
   
    
    
    void Start()
    {
        lastPos = player.transform;
       playerH = GameObject.Find("Player");
    }

    
    void Update()
    {
        //grounded check
        grounded = Physics.CheckSphere(groundCheck.transform.position, gizmo, groundMask);
        
        //timer 
        if (interval < saveTime)
        {
            waitingSave = true;
        }
        
        //if the time has elapsed when the grounded check is true,move the transform of the spawn point
        if (waitingSave)
        {
            interval += Time.deltaTime;
            if (interval > saveTime && grounded)
            {
                respawn.transform.position = lastPos.transform.position;
               waitingSave = false;
               interval = 0;
            }
        }
    }   
    
    // FixedUpdate seemed to solve updating the players position with a respawn
    private void FixedUpdate()
    {
        //uses a timer 
        if (respawnTimeElapsed < respawnTime)
        {
            respawnBool = false;
        }
        
        //if grounded check is false start the timer
        if (!grounded)
        { 
            interval = 0;
            respawnTimeElapsed += Time.deltaTime;
            if (respawnTimeElapsed >= respawnTime)
            {
                respawnBool = true;
                respawnTimeElapsed = 0;
            }
        }
        
        // when timer exceeds, move the player and take damage
        if (respawnBool)
        {
            player.transform.position = respawn.transform.position;
            playerH.GetComponent<CharacterHealth>().TakeDamage(20);
        }
    }

    //gizmo
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position,gizmo);
      
    }
}
