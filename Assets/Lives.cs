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
        UpdateLives();
    }

    public void UpdateLives()
    {
        switch(PlayerPrefs.GetInt("Lives"))
        {
            case 3:
                images[2].enabled = false;
                break;
            case 2:
                images[1].enabled = false;
                break;
            case 1:
                images[0].enabled = false;
                break;
        }
    }

    public void RemoveLife()
    {
        PlayerPrefs.SetInt("Lives", lives - 1);
        if(PlayerPrefs.GetInt("Lives") == 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
