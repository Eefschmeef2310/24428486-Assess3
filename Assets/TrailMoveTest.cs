using UnityEngine;

public class TrailMoveTest : MonoBehaviour
{
    public Animator animator;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
