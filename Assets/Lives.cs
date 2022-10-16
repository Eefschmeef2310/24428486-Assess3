using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public List<Image> images;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("newMap") == null) //first play
        {
            lives = 3;
            PlayerPrefs.SetInt("Lives", lives);
        }
    }

    public void UpdateLives()
    {
        switch(lives)
        {
            case 3:
                lives--;
                images[2].enabled = false;
                break;
            case 2:
                lives--;
                images[1].enabled = false;
                break;
            case 1:
                lives--;
                images[0].enabled = false;
                break;
        }
    }
}
