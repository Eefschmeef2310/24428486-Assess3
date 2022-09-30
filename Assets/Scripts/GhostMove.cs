using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float speed = 1f;
    public GameObject main;

    // Update is called once per frame
    void Update()
    {
        main.transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
