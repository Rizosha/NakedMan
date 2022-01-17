using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
 
    
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    
    private Vector3 velocity;

    private bool isGrounded;

    private Vector3 moveDir;

    [SerializeField] Transform player;
    [SerializeField] Transform respawn;
    
    
    
    
    private float interval;
    public float saveTime;
    private bool waitingSave = false;
    
     public float normalSpeed;
     public float bonusSpeed;
    private bool shootReloading;

    private void Start()
    {
        speed = normalSpeed;
    }

    void Update()
    {
        
        // takes input from unity controller

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        //Vector3 moveDir = new Vector3(x, z).normalized;


        // creates a transform based off input
        Vector3 move = transform.right * x + transform.forward * z;


        // moves the character using speed and delta time 
        controller.Move(Vector3.ClampMagnitude(move, 1) * speed * Time.deltaTime);
        //controller.Move(move * speed * Time.deltaTime);


        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);

        if (interval < saveTime)
                    {
                        waitingSave = true;
                    }
                     
                    if (waitingSave)
                    {
                        interval += Time.deltaTime;
                        if (interval >= saveTime)
                        { 
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                     
                                    controller.Move(Vector3.ClampMagnitude(move,2 ) * bonusSpeed * Time.fixedDeltaTime);
                                    waitingSave = false;
                                    interval = 0; 
                            }
                            
                        }
                    }

      
        Debug.Log(interval);
    }
}






 

