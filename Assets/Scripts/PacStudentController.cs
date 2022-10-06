using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    Vector3Int lastInput = Vector3Int.right; //Vector3.*direction* will be used.
    Vector3Int movingDirection = Vector3Int.right;
    public Tilemap tilemap;
    public Animator animator;

    Vector3 currentInput;
    public float speed;

    void Start()
    {
        currentInput = transform.position + lastInput;
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

        //when the player reaches the next tile, check if lastInput is a valid tile to move to. If so, update target, so the player moves there
        if(transform.position == currentInput)
        {
            Vector3Int nextPos = new Vector3Int((int)(currentInput.x - 0.5f) + lastInput.x, (int)(currentInput.y - 0.5f) + lastInput.y);
            //Debug.Log(nextPos);
            if(tilemap.GetTile(nextPos) == null || tilemap.GetTile(nextPos).name == "Pellet" || tilemap.GetTile(nextPos).name == "PowerPellet") //tile in direction is valid
            {
                currentInput += lastInput;
                movingDirection = lastInput;
            }
            else
            {
                nextPos = new Vector3Int((int)(currentInput.x - 0.5f) + movingDirection.x, (int)(currentInput.y - 0.5f) + movingDirection.y);
                if(tilemap.GetTile(nextPos) == null || tilemap.GetTile(nextPos).name == "Pellet" || tilemap.GetTile(nextPos).name == "PowerPellet") //tile in direction is valid
                {
                    currentInput += movingDirection;
                }
            }
        }
        else
        {
            if(movingDirection == Vector3Int.up)
            {
                animator.Play("Up", 1);
            }
            if(movingDirection == Vector3Int.left)
            {
                animator.Play("Left", 1);
            }
            if(movingDirection == Vector3Int.down)
            {
                animator.Play("Down", 1);
            }
            if(movingDirection == Vector3Int.right)
            {
                animator.Play("Right", 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, currentInput, speed * Time.deltaTime);
        }
        //animator.Play("Right", 1);
    }
}
