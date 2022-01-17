using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    public int ammo = 0;
    public Text ScoreText;
    void Update()
    {
        ScoreText.text = ammo.ToString();
    }
}
