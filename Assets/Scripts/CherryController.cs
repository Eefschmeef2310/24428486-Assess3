using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos.y = Random.Range(-18.5f, 18.5f);
        if(spawnPos.y == -18.5f || spawnPos.y == 18.5f)
        {
            spawnPos.x = Random.Range(-32.5f, 32.5f);
        }
        else
        {
            if(Random.value<0.5f)
            {
                spawnPos.x  = -32.5f;
            }
            else
            {
                spawnPos.x = 32.5f;
            }
        }
        transform.position = new Vector3(spawnPos.x, spawnPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != -spawnPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, -spawnPos, movementSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}