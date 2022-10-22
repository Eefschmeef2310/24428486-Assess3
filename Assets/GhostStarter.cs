using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStarter : MonoBehaviour
{
    //This script is for moving the ghosts from the pen to the starting position. From there, their AI takes over
    public List<GameObject> ghosts;
    public List<float> zPos;
    int ghost = 0;
    int section = 1;
    public float moveSpeed = 3;

    public void Start()
    {
        ghost = 0;
        section = 1;
    }

    void Update()
    {
        if(ghost == 4)
        {
            gameObject.GetComponent<GhostStarter>().enabled = false;
        }
        else
        {
            moveGhost(ghosts[ghost]);
        }
    }

    void moveGhost(GameObject movingGhost)
    {
        if(section == 4)
        {
            movingGhost.GetComponent<GhostController>().enabled = true;
            ghost++;
            section = 1;
        }
        else{
            if(section == 1)
            {
                if(Vector3.Distance(movingGhost.transform.position, new Vector3(0,1.5f,0)) <= 0.1f)
                {
                    section++;
                    movingGhost.transform.position = new Vector3(0,1.5f,0);
                }
                else
                {
                    movingGhost.transform.rotation = Quaternion.Euler(new Vector3(0,0,zPos[ghost]));
                    movingGhost.transform.position = Vector3.MoveTowards(movingGhost.transform.position, new Vector3(0, 1.5f, 0), moveSpeed * Time.deltaTime);
                }
            }
            else if(section == 2)
            {
                if(Vector3.Distance(movingGhost.transform.position, new Vector3(0,3.5f,0)) <= 0.1f)
                {
                    section++;
                    movingGhost.transform.position = new Vector3(0,3.5f,0);
                }
                else
                {
                    movingGhost.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
                    movingGhost.transform.position = Vector3.MoveTowards(movingGhost.transform.position, new Vector3(0, 3.5f, 0), moveSpeed * Time.deltaTime);
                }
            }
            else if(section == 3)
            {
                if(Vector3.Distance(movingGhost.transform.position, new Vector3(-0.5f,3.5f,0)) <= 0.1f)
                {
                    section++;
                    movingGhost.transform.position = new Vector3(-0.5f, 3.5f,0);
                }
                else
                {
                    movingGhost.transform.rotation = Quaternion.Euler(new Vector3(0,0,270));
                    movingGhost.transform.position = Vector3.MoveTowards(movingGhost.transform.position, new Vector3(-0.5f, 3.5f, 0), moveSpeed * Time.deltaTime);
                }
            }
            
        }
    }
}
