using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailToggle : MonoBehaviour
{
    public GameObject[] trails = new GameObject[2];
    
    void Off()
    {
        trails[0].SetActive(false);
        trails[1].SetActive(false);
    }

    void On()
    {
        trails[0].SetActive(true);
        trails[1].SetActive(true);
    }
}
