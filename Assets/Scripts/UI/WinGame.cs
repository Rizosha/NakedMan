using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    /// <summary>
    /// Loads end scene
    /// </summary>

    void OnTriggerEnter(Collider coll)
    {
     if(coll.gameObject.tag == "Player")
         SceneManager.LoadScene(sceneBuildIndex: 2);
    }
}
