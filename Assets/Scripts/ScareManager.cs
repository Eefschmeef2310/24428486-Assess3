using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareManager : MonoBehaviour
{
    public Animator animator;
    public ResetPosition resetPosition;
    public bool scared = false;
    public bool dead = false;
    bool resetting = false;
    public int ghost;
    public float speed;
    public BoxCollider2D collide;
    public GhostController ghostController;
    public GhostStarter ghostStarter;
    void Update()
    {
        if(dead && resetting)
        {
            ghostController.enabled = false;
            collide.enabled = false;
            ghostStarter.enabled = false;

            animator.Play("TurnOffTrails", 1);
            animator.Play("Recovering", 1);
            animator.Play("Right", 0);
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(resetPosition.xPos, -0.5f, 0), speed * Time.deltaTime);
            
            
            if(transform.position == new Vector3(resetPosition.xPos, -0.5f, 0))
            {
                animator.Play("Down", 0);
                animator.Play("Default", 1);
                dead = false;
            }            
        }
        if(!dead && resetting)
        {
            if(transform.position == new Vector3(-0.5f, 3.5f, 0))
            {
                animator.enabled = true;
                
                ghostController.enabled = true;
                ghostController.Reset();

                ghostStarter.enabled = true;

                collide.enabled = true;
                resetting = false;
                scared = false;
            }
            else
            {
                animator.Play("Default", 1);
                animator.enabled = false;
                ghostStarter.moveGhost(gameObject, ghost);
            }
        }
    }
    public void RecoveringGhost()
    {
        dead = true;
        resetting = true;
        animator.enabled = true;
        animator.Play("Recovering", 1);
    }

    public void scaredState()
    {
        scared = true;
        animator.Play("Scared", 1);
    }
}
