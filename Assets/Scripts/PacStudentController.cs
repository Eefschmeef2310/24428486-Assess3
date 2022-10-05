using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    Vector3Int lastInput = Vector3Int.right; //Vector3.*direction* will be used.
    public Tilemap tilemap;

    Vector3 target;
    public float speed;

    void Start()
    {
        target = transform.position + lastInput;
    }

    void Update()
    {
        //Get inputs and update lastInput
        if(Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector3Int.up;
            //Debug.Log("W");
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector3Int.left;
            //Debug.Log("A");
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector3Int.down;
            //Debug.Log("S");
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector3Int.right;
            //Debug.Log("D");
        }

        //when the player reaches the next tile, check if lastInput is a vlid tile to move to. If so, update target, so the player moves there
        if(transform.position == target)
        {
            Vector3Int nextPos = new Vector3Int((int)transform.position.x, (int)transform.position.y + lastInput.y);
            if(tilemap.GetTile(nextPos) == null || tilemap.GetTile(nextPos).name == "Pellet" || tilemap.GetTile(nextPos).name == "PowerPellet") //tile in direction is valid
            {
                target = transform.position + lastInput;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
