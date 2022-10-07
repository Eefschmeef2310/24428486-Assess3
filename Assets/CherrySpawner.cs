using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherrySpawner : MonoBehaviour
{
    public GameObject cherry;

    void Start()
    {
        InvokeRepeating("spawnCherry", 10, 10);
    }

    void spawnCherry()
    {
        Instantiate(cherry, Vector3.zero, Quaternion.identity);
    }
}
