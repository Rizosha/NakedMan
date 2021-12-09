using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
 
    
    public CharacterController controller;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    
    private Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    private Vector3 moveDir;

    

   
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        // takes input from unity controller
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(x, z).normalized;
        
       
        
        // creates a transform based off input
        
        Vector3 move = transform.right * x + transform.forward * z;
        
        // moves the character using speed and delta time 
        
        controller.Move(move * speed * Time.deltaTime);

        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.fixedDeltaTime); 
        
        
        
    }
    
    
}
