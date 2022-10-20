using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Vector2 startPos;
    // Start is called before the first frame update
    public void Start()
    {
        transform.position = startPos;
        transform.rotation = Quaternion.Euler(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
