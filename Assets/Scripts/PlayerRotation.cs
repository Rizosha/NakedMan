using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
  [SerializeField] private Transform player;
  [SerializeField] private Transform mouse;
 
  private void Update()
  {
    /*
    Vector3 _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.z);
    Vector3 direction = mouse - player.position;
    Debug.DrawRay(transform.position,direction, Color.green);
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    */
    
    //Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);
    //Get the Screen position of the mouse
    
    //sets true north
    Vector3 north = Quaternion.Euler(-45f, 0, 0) * player.forward;
    //direction of mouse 
    Vector3 direction = mouse.position - transform.position;  
    //creates a reference from true north
    Vector3 cross = Vector3.Cross(north, direction);
    
    Debug.DrawRay(transform.position,direction, Color.green);
    float angle = Vector3.Angle(direction,north);
    if (cross.y < 0)
    {
        //makes the angle 360
        angle = (180 - angle) + 180;
        
        //makes the angle 180
        // angle = 180 - angle;
    }
    
    Debug.Log(angle);
    Debug.DrawRay(player.position,north, Color.magenta);
    
    

   /* float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
      return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
   }*/
    
  

  

  }

  
}
