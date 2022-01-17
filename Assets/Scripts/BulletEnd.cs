using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnd : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {

            col.gameObject.SetActive(false);
        }

    }
}
