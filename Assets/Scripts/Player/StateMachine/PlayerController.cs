using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController: MonoBehaviour
    {
        private StateInterface _currentState;
      
        
        public Rigidbody rb;
      
        public float speed;
        public float dodgeSpeed;
        public float dodgeTime;
        public bool isDodging;
       
       // public Vector3 velocity;
        private bool isGrounded;
        public Vector3 moveDir;
        public float x;
        public float z;
   
        public InputActionReference move;
        public InputActionReference dodge;
        public InputActionReference fire;
        
        
        
        public bool canDodge;
        

        [SerializeField] Transform player;
        [SerializeField] Transform respawn;
       // public float dodgeDistance;
        public Vector3 lastDir;


        void Start()
        {
            rb = GetComponent<Rigidbody>();
            ChangeState(new MovementState(this));
        }

     
        public void ChangeState(StateInterface newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        public void Update()
        {
            //GetMovementAxis();
            _currentState?.UpdateState();
        }
    }

