using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float speed;
    public Animator animator;

    private Vector3 target;

    void Start()
    {
        target = new Vector3(-12.5f, 13.5f, -0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == target)
        {
            target = changeTarget();
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed/100);
    }

    private Vector3 changeTarget()
    {

        if(animator.GetCurrentAnimatorStateInfo(1).IsName("Up"))
        {
            animator.Play("Right",1, 0);
            return new Vector3(-7.5f, 13.5f, -0.1f);
        }
        else if(animator.GetCurrentAnimatorStateInfo(1).IsName("Right"))
        {
            animator.Play("Down",1, 0);
            return new Vector3(-7.5f, 9.5f, -0.1f);
        }
        else if(animator.GetCurrentAnimatorStateInfo(1).IsName("Down"))
        {
            animator.Play("Left",1, 0);
            return new Vector3(-12.5f, 9.5f, -0.1f);
        }
        else if(animator.GetCurrentAnimatorStateInfo(1).IsName("Left"))
        {
            animator.Play("Up",1, 0);
            return new Vector3(-12.5f, 13.5f, -0.1f);
        }
        return new Vector3(0,0,0);
    }
}
