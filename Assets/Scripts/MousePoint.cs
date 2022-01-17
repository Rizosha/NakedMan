using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MousePoint : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask _layerMask;
  

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit raycastHit, float.MaxValue,_layerMask))
        {
            transform.position = raycastHit.point;
        }
        
        
         
        
    }
}
