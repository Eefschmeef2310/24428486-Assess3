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
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public AudioSource backgroundMusic;
    public AudioClip normalMusic;
    void Update()
    {
        if(dead && resetting)
        {
            ghostController.enabled = false;
            collide.enabled = false;
            ghostStarter.enabled = false;

            animator.SetLayerWeight(0,0);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(resetPosition.xPos, -0.5f, 0), speed * Time.deltaTime);
            
            if(transform.position == new Vector3(resetPosition.xPos, -0.5f, 0))
            {
                animator.SetLayerWeight(0,1);
                animator.SetLayerWeight(1,0);
                animator.Play("Down", 0);
                dead = false;
            }            
        }
        if(!dead && resetting)
        {
            if(transform.position == new Vector3(-0.5f, 3.5f, 0))
            {
                animator.enabled = true;
                animator.SetLayerWeight(0,1);
                animator.SetLayerWeight(1,0);
                
                ghostController.enabled = true;
                ghostController.Reset();

                ghostStarter.enabled = true;

                backgroundMusic.clip = normalMusic;
                backgroundMusic.Play();

                collide.enabled = true;
                resetting = false;
                scared = false;
            }
            else
            {
                spriteRenderer.sprite = sprite;
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
        scared = false;
        animator.Play("Right", 0);
        animator.Play("TurnOffTrails", 1);
        animator.Play("Recovering", 1);
    }

    public void scaredState()
    {
        animator.SetLayerWeight(1,1);
        scared = true;
        animator.Play("Scared", 1);
    }
}
