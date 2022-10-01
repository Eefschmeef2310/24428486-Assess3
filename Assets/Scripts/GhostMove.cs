using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float speed = 1f;
    //public Transform sprite;
    //Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    /*
    public void GetDirection()
    {
        switch(sprite.rotation.eulerAngles.z)
        {
            case 0:
                direction = Vector3.down;
                break;
            case 90:
                direction = Vector3.right;
                break;
            case 180:
                direction = Vector3.up;
                break;
            case -90:
                direction = Vector3.left;
                break;
        }
    }
    */
}