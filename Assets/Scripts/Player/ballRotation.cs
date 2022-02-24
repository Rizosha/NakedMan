using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballRotation : MonoBehaviour
{
   public Transform mouse;

   public void Update()
   {
      //looks at mouse 
      transform.LookAt(mouse);
   }

}
