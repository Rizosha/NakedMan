using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    /// <summary>
    /// Enemy health looks at the camera
    /// </summary>
    
   public Transform cam;
    
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
