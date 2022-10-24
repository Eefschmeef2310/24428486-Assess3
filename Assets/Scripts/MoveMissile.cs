using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveMissile : MonoBehaviour
{
    Vector3 nextPos;
    public PacStudentController pacStudentController;
    Vector3Int movingDirection;
    public Tilemap tilemap;
    public float speed;
    void Start()
    {
        nextPos = transform.position;
        movingDirection = pacStudentController.movingDirection;
    }
    void Update()
    {
        if(transform.position == nextPos)
        {
            if(CheckPos(movingDirection + nextPos))
            {
                nextPos += movingDirection;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
    }

    bool CheckPos(Vector3 input)
    {
        Vector3Int checkTile = new Vector3Int((int)(input.x-0.5f), (int)(input.y-0.5f));
        if(tilemap.GetTile(checkTile) == null || tilemap.GetTile(checkTile).name == "Pellet" || tilemap.GetTile(checkTile).name == "PowerPellet")
        {
            return true;
        }
        return false;
    }
}
