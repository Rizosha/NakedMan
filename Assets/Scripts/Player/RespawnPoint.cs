using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RespawnPoint : MonoBehaviour
{
    /// <summary>
    /// gizmo for respawn point
    /// </summary>
    public float gizmo;
   
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,gizmo);
    }
}


