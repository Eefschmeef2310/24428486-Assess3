using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public float xPos;
    // Start is called before the first frame update
    public void Start()
    {
        transform.position = new Vector3(xPos, -0.5f, 0);
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
