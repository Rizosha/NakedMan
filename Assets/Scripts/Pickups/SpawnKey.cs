using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour
{
    public int enemiesleft = 2;
    public bool enemiesDefeated;
    public float gizmo;
    public AudioSource spawnSound;
    public AudioClip spawnClip;

    private void Start()
    {
        //plays the key sound
        spawnSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        //conditions to summon the key
        if (enemiesleft <= 0)
        {
            enemiesDefeated = true;
        }

        if (enemiesleft >= 1)
        {
            enemiesDefeated = false;
        }
  
        //sets the key to active
        if (enemiesDefeated)
        {
            GameObject key = poolKey.SharedInstance.GetPooledObject();
            if (key != null)
            {
                spawnSound.PlayOneShot(spawnClip);
                key.transform.position = key.transform.position;
                key.SetActive(true);
               
            }
        }
    }
    //called from PickupKey
    public void stopKeySpawn(int add)
    {
        enemiesleft += 1;
    }
    
    //gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, gizmo);
     
     
    }
}
