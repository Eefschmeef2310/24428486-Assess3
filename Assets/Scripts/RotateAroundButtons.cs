using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundButtons : MonoBehaviour
{
    public Vector3[] points;
    int startPoint = 0;
    public Animator animator;
    string[] directions = {
        "Down",
        "Left",
        "Up",
        "Right"
    };
    void Start()
    {
        animator.Play("Right", 1);
    }
    void Update()
    {
        if(transform.position != points[startPoint])
        {
            transform.position = Vector3.MoveTowards(transform.position, points[startPoint], 2.5f * Time.deltaTime);
        }
        else
        {
            animator.Play(directions[startPoint], 1);
            startPoint++;
            if(startPoint == points.Length)
            {
                startPoint = 0;
            }
        }
    }
}
