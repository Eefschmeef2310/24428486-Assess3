using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareManager : MonoBehaviour
{
    public Animator animator;
    public bool scared = false;
    public IEnumerator RecoveringGhost()
    {
        animator.Play("Recovering");
        yield return new WaitForSeconds(5);
        animator.Play("Default");
        scared = false;
    }

    public IEnumerator scaredState()
    {
        scared = true;
        animator.Play("Scared");
        yield return null;
    }
}
