using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostController : MonoBehaviour
{
    Vector3Int movingDirection;
    Vector3 nextPos;
    Vector3 previousPos;
    public float speed;
    public Tilemap tilemap;
    public Animator animator;
    public float ghostType;
    public ScareManager scareManager;
    public Transform pacStudent;
    bool topCornerReached = false;
    public List<Vector3> positions;
    int fourPos;
    Vector3Int[] directions = {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left
    };

    void Start()
    {
        Reset();
    }
    public void Reset()
    {
        previousPos = transform.position;
        nextPos = transform.position;
    }
    void Update()
    {
        if(transform.position == nextPos)
        {
            if(scareManager.scared)
            {
                nextPos += One();
            }
            else
            {
                switch(ghostType)
                {
                    case 1:
                        nextPos += One();
                        break;
                    case 2:
                        nextPos += Two(pacStudent.position);
                        break;
                    case 3:
                        nextPos += Three();
                        break;
                    case 4:
                        nextPos += Four();
                        break;
                }
            }
        }
        else
        {
            if(movingDirection == Vector3Int.up)
            {
                animator.Play("Up", 0);
            }
            else if(movingDirection == Vector3Int.left)
            {
                animator.Play("Left", 0);
            }
            else if(movingDirection == Vector3Int.down)
            {
                animator.Play("Down", 0);
            }
            else if(movingDirection == Vector3Int.right)
            {
                animator.Play("Right", 0);
            }
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }
        
    }

    Vector3Int One()
    {
        List<Vector3Int> furtherTiles = new List<Vector3Int>();
        float ghostToPac = Vector3.Distance(transform.position, pacStudent.position);
        foreach(Vector3Int tile in getValids()) //for every valid surrounding tile
        {
            if(Vector3.Distance(pacStudent.position, transform.position+tile) > ghostToPac)
            {
                furtherTiles.Add(tile);
            }
        }

        Vector3Int next;
        if(furtherTiles.Count != 0)
        {
            next = furtherTiles[Random.Range(0, furtherTiles.Count)];
            movingDirection = next;
            return next;
        }
        else
        {
            next = getValids()[0];
            movingDirection = next;
            return next;

        }
        
    }
    Vector3Int Two(Vector3 target)
    {
        List<Vector3Int> closerTiles = new List<Vector3Int>();
        float ghostToTarget = Vector3.Distance(transform.position, target);
        foreach(Vector3Int tile in getValids()) //for every valid surrounding tile
        {
            if(Vector3.Distance(target, transform.position+tile) < ghostToTarget)
            {
                closerTiles.Add(tile);
            }
        }
        Vector3Int next;
        if(closerTiles.Count != 0)
        {
            next = closerTiles[Random.Range(0, closerTiles.Count)];
            movingDirection = next;
            return next;
        }
        else
        {
            next = getValids()[0];
            movingDirection = next;
            return next;

        }
    }
    Vector3Int Three()
    {
        Vector3Int next = getValids()[Random.Range(0, getValids().Count)];
        movingDirection = next;
        return next;
    }
    Vector3Int Four()
    {
        //go to top right corner, using ghost 2 behaviour
        if(transform.position == new Vector3(1.5f,13.5f,0))
        {
            topCornerReached = true;
            Vector3Int next = Vector3Int.right;
            movingDirection = next;
            return next;
        }

        if(!topCornerReached)
        {
            return Two(new Vector3(1.5f,13.5f,0));
        }
        else
        {
            if(transform.position != positions[fourPos])
            {
                return Two(positions[fourPos]);
            }
            else
            {
                fourPos++;
                if(fourPos == positions.Count - 1)
                {
                    fourPos = 0;
                }
                return Two(positions[fourPos]);
            }
            
        }
    }

    List<Vector3Int> getValids()
    {
        List<Vector3Int> valids = new List<Vector3Int>();
        foreach(Vector3Int direction in directions) //for all four possible directions
        {
            if(CheckPos(transform.position + direction)) //if tile isn't a wall
            {
                if(-movingDirection != direction) //and the direction doesn't double-back
                {
                    valids.Add(direction); //direction is valid, so add it to list
                }
                
            }
        }
        return valids;
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
