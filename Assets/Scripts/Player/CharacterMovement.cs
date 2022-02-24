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
    
    void Update()
    {
        
        // takes input from unity controller
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // creates a transform based off input
        Vector3 move = transform.right * x + transform.forward * z;


        // moves the character using speed and delta time 
        controller.Move(Vector3.ClampMagnitude(move, 1) * speed * Time.deltaTime);
        
        
        // apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
        
    }
}






 

