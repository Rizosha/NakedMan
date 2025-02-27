using UnityEngine;

public class MidPoint : MonoBehaviour
{

    public Transform pointA;
    public Transform pointB;
    public float radius;
   

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = (pointA.position + pointB.position) / 2;
         gameObject.transform.position = (pointA.position + pointB.position) / 2;
         Vector3 direction = midpoint - pointB.position;
         float distance = direction.magnitude;

         // Clamp the position within the radius
         if (distance > radius)
         {
             // If the midpoint is beyond the radius, bring it back within range
             gameObject.transform.position = pointB.position + direction.normalized * radius;
         }
         else
         {
             // If within range, set position to the midpoint
             gameObject.transform.position = midpoint;
         }
        
    }
}
