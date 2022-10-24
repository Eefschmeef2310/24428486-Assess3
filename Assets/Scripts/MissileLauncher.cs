using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GhostManager ghostManager;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ghostManager.pelletMode)
        {
            Debug.Log("Missile launched!");
            ghostManager.exitPelletMode();
        }
    }
}
