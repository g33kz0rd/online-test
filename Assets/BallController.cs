using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool Grabbed { get; set; }


    private void OnGUI()
    {
        GUI.TextField(new Rect(240, 0, 100, 20), Grabbed ? "Grabbed" : "Loose");
    }
            
}
