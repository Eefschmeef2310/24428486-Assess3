using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScared : MonoBehaviour
{
    public Animator animator;
    public IEnumerator scaredState()
    {
        animator.Play("Scared");
        yield return null;
    }
}
