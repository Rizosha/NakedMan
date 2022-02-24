using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private bool grounded = false;

    public float gizmo;
    
    private float interval;
    public float saveTime;
    private bool waitingSave = true;
    private LayerMask end;

    /// <summary>
    /// Performs a grounded check to set a de-spawn time limit. 
    /// </summary>
    public void Update()
    {
        grounded = Physics.CheckSphere(transform.position, gizmo);
        if (grounded)
        {
            if (interval < saveTime)
            {
                waitingSave = true;
            }
             
            if (waitingSave)
            {
                interval += Time.deltaTime;
                if (interval > saveTime && grounded)
                {
                    gameObject.SetActive(false);
                    waitingSave = false;
                    interval = 0;
                }
            }
        }
    }
    
    // gizmo
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmo);
    }




}

