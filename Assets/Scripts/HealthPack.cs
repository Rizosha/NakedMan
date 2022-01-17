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
        playerH = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
    
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    
        inRange = Physics.CheckSphere(transform.position, gizmo, player);
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            playerH.GetComponent<CharacterHealth>().HealthPickup(50);
            Destroy(gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmo);
        
        
    }
}
