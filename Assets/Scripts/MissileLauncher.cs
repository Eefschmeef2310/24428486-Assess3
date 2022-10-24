using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GhostManager ghostManager;
    public GameObject missile;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ghostManager.pelletMode)
        {
            missile.SetActive(true);
            Instantiate(missile, transform.position, Quaternion.identity);
            missile.SetActive(false);
            ghostManager.exitPelletMode();
        }
    }
}
