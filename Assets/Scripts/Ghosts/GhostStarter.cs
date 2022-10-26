using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStarter : MonoBehaviour
{
    //This script is for moving the ghosts from the pen to the starting position. From there, their AI takes over
    public List<GameObject> ghosts;
    public List<Animator> animators;
    public List<ScareManager> scareManagers;
    public List<SpriteRenderer> spriteRenderers;
    public List<Color> colors;
    public List<float> zPos;
    public List<GhostController> controllers;
    int ghost = 0;
    [HideInInspector] public int section = 1;
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
            this.enabled = false;
        }
        else
        {
            animators[ghost].enabled = false;
            
            if(scareManagers[ghost].scared)    
            {
                spriteRenderers[ghost].color = new Color(0,0,0.82353f);
            }        
            else
            {
                spriteRenderers[ghost].color = colors[ghost];
            }
            moveGhost(ghosts[ghost], ghost);
        }
    }

    public void moveGhost(GameObject movingGhost, int ghost)
    {
        if(section == 4)
        {
            this.ghost++;
            section = 1;
            controllers[ghost].enabled = true;
            controllers[ghost].Reset();
            animators[ghost].enabled = true;
        }
        else{
            if(section == 1)
            {
                if(movingGhost.transform.position == new Vector3(0,1.5f,0))
                {
                    section++;
                }
                else
                {
                    movingGhost.transform.rotation = Quaternion.Euler(new Vector3(0,0,zPos[ghost]));
                    movingGhost.transform.position = Vector3.MoveTowards(movingGhost.transform.position, new Vector3(0, 1.5f, 0), moveSpeed * Time.deltaTime);
                }
            }
            else if(section == 2)
            {
                if(movingGhost.transform.position == new Vector3(0,3.5f,0))
                {
                    section++;
                }
                else
                {
                    movingGhost.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
                    movingGhost.transform.position = Vector3.MoveTowards(movingGhost.transform.position, new Vector3(0, 3.5f, 0), moveSpeed * Time.deltaTime);
                }
            }
            else if(section == 3)
            {
                if(movingGhost.transform.position == new Vector3(-0.5f,3.5f,0))
                {
                    section++;
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
