using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareManager : MonoBehaviour
{
    public Animator animator;
    public Collider2D collider;
    public ResetPosition resetPosition;
    public bool scared = false;
    public bool dead = false;
    public float speed;
    void Update()
    {
        if(dead)
        {
            collider.enabled = false;
            animator.Play("Right", 1);
            animator.Play("",0);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(resetPosition.xPos, -0.5f, 0), speed * Time.deltaTime);
        }
    }
    public IEnumerator RecoveringGhost()
    {
        animator.Play("Recovering");
        yield return new WaitForSeconds(5);
        //TODO: Move back to pen
        scared = false;
        animator.Play("Default");
    }

    public IEnumerator scaredState()
    {
        scared = true;
        animator.Play("Scared");
        yield return null;
    }
}
