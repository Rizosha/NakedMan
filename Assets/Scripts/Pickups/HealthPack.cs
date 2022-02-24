using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float gizmo;
    private bool inRange;
    public LayerMask player;
    [SerializeField] public GameObject playerH;
    public float speed = 10f;
   
    void Start()
    {
        // finds the player 
        playerH = GameObject.Find("Player");
    }
    
    void Update()
    {
        // rotates the health pack at a set speed. 
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    
        //check sphere to see if the player is nearby, 
        inRange = Physics.CheckSphere(transform.position, gizmo, player);
        
        //if in range is met and E is pressed, adds 50 health and destroys
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            playerH.GetComponent<CharacterHealth>().HealthPickup(50);
            Destroy(gameObject);
        }
    }
    
    // gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmo);
    }
}
