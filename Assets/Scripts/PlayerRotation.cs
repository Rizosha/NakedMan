using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform mouse;
    public GameObject bullet;

    private int aDirection = 0;

    public Animator animator;


    private void Update()
    {
      
        //sets true north
        Vector3 north = Quaternion.Euler(-45f, 0, 0) * player.forward;
        //direction of mouse 
        Vector3 direction = mouse.position - transform.position;
        //creates a reference from true north
        Vector3 cross = Vector3.Cross(north, direction);

        Debug.DrawRay(transform.position, direction, Color.green);
        
        //calculates angle of north and mouse direction
        float angle = Vector3.Angle(direction, north);
        if (cross.y < 0)
        {
            //makes the angle 360
            angle = (180 - angle) + 180;

            //makes the angle 180
            // angle = 180 - angle;
        }

       // Debug.Log(angle);
        Debug.DrawRay(player.position, north, Color.magenta);

        if (angle >= 0 && angle <= 30)
        {
            aDirection = 1;
        }
        if (angle >= 30 && angle <= 60 )
        {
            aDirection = 2;
        }
        if (angle >= 60 && angle <= 150)
        {
            aDirection = 3;
        }
        if (angle >= 150 && angle <= 180)
        {
            aDirection = 4;
        }
        if (angle >= 180 && angle <= 210)
        {
            aDirection = 4;
        }
        if (angle >= 210 && angle <= 300)
        {
            aDirection = 5;
        }
        if (angle >= 300 && angle <= 330)
        {
            aDirection = 6;
        }

        if (angle >= 330)
        {
            aDirection = 1;
        }
        
        //Debug.Log(aDirection);
        

        /* float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
         {
           return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }*/
        
        animator.SetInteger("Rotation", aDirection);
       // Debug.Log("rotation log" + animator.GetInteger("Rotation"));
       
     
       //public void RotateAround(Vector3 player, Vector3 y, float mouse.Position); 
    }

   
}
