using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D cross;
    
    // Start is called before the first frame update
    void Start()
    {
        //hide cursor
        //Cursor.visible = false;
        Cursor.SetCursor(cross,Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
