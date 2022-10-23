using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTitleScreen : MonoBehaviour
{
    public int startPoint;
    public Animator animator;
    Vector3[] points = 
    {
        new Vector3(-5.5f,3.8f),
        new Vector3(5.5f,3.8f),
        new Vector3(5.5f,-3.8f),
        new Vector3(-5.5f,-3.8f),
    };
    string[] directions = {
        "Right",
        "Down",
        "Left",
        "Up"
    };

    void Update()
    {
        if(transform.position != points[startPoint])
        {
            transform.position = Vector3.MoveTowards(transform.position, points[startPoint], 2.5f * Time.deltaTime);
        }
        else
        {
            animator.Play(directions[startPoint]);
            startPoint++;
            if(startPoint == 4)
            {
                startPoint = 0;
            }
        }
    }
}
