using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    Vector3 spawnPos;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        if(spriteRenderer.enabled == false)
        {
            InvokeRepeating("spawnCherry", 10, 10);
        }

        spawnPos = transform.position;
    }

    void Update()
    {
        if(spriteRenderer.enabled == true) //all cloned cherries will have an active sprite renderer, while the base cherry won't
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

    void spawnCherry()
    {
        Vector3 position = Vector3.zero;

        switch(Random.Range(0,4)) //randomly pick a side, then spawn a cherry at a random spot along that axis
        {
            case 0: //top
                position = new Vector3(Random.Range(-32.5f, 32.5f), 18.5f);
                break;
            case 1: //bottom
                position = new Vector3(Random.Range(-32.5f, 32.5f), -18.5f);
                break;
            case 2: //left
                position = new Vector3(-32.5f, Random.Range(-18.5f, 18.5f));
                break;
            case 3: //right
                position = new Vector3(32.5f, Random.Range(-18.5f, 18.5f));
                break;
        }

        spawnPos = position;

        spriteRenderer.enabled = true;
        Instantiate(gameObject, position, Quaternion.identity);
        spriteRenderer.enabled = false;
    }
}