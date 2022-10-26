using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GhostManager ghostManager;
    public GameObject missile;
    public AudioClip clip;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ghostManager.pelletMode)
        {
            missile.SetActive(true);
            Instantiate(missile, transform.position, Quaternion.identity);
            missile.SetActive(false);
            AudioSource.PlayClipAtPoint(clip, Vector3.zero);
            ghostManager.exitPelletMode();
        }
    }
}
