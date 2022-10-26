using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepCanvasUp : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        //transform.position = pos;
        /*
        Debug.Log((int)transform.parent.rotation.eulerAngles.z == 90);
        switch((int)transform.parent.rotation.eulerAngles.z)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0,0,0);
                break;
            case 90:
                //Debug.Log("test");
                transform.rotation = Quaternion.Euler(0,0,0);
                break;
            case 180:
                transform.rotation = new Quaternion(0,0,180,0);
                break;
            case 270:
                transform.rotation = new Quaternion(0,0,90,0);
                break;
        }
        */
        
    }
}
