using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MousePoint : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform player;
    [SerializeField] private Transform aim;

    void Update()
    {
       
        //stores a ray from your mouse into the world
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        // if the raycast hits the provided layer mask, transform the cube to the location. 
        if (Physics.Raycast(ray,out RaycastHit raycastHit, float.MaxValue,_layerMask))
        {
            transform.position = raycastHit.point;
           //transform.position = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
        }
     
        
  
    }
}
