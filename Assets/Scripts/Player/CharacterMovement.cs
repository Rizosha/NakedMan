using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
 /*
    public Rigidbody rb;
    public UnityEngine.CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Vector3 velocity;
    private bool isGrounded;
    private Vector3 moveDir;
    public float x;
    public float z;
    private bool canDodge;

    [SerializeField] Transform player;
    [SerializeField] Transform respawn;
    
    private CharacterController _characterController;
    
    
    void Start()
    {
        _characterController = new CharacterController(this);
        _characterController.ChangeState(new IdleState(_characterController));
    }
    
    void Update()
    {
        GetMovementAxis();
      ///  Movement();
    }
    
    public void GetMovementAxis()
    {
       x = Input.GetAxis("Horizontal");
       z = Input.GetAxis("Vertical");
       moveDir = transform.right * x + transform.forward * z;
    }

    public void Movement()
    {
        rb.MovePosition(rb.position + Vector3.ClampMagnitude(moveDir, 1) * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, velocity.y, rb.linearVelocity.z);
        if (canDodge)
        {
            Dodge();
        }
    }
    
    public void Dodge()
    {
      canDodge = false;
      
       
       

    }*/
}






 

