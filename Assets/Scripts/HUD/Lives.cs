using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public List<Image> images;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Lives", 3);
    }

    public void UpdateLives()
    {
        switch(PlayerPrefs.GetInt("Lives"))
        {
            case 2:
                images[2].enabled = false;
                break;
            case 1:
                images[2].enabled = false;
                images[1].enabled = false;
                break;
            case 0:
                images[2].enabled = false;
                images[1].enabled = false;
                images[0].enabled = false;
                break;
            default:
                break;
        }
    }

    public void RemoveLife()
    {
        PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
        UpdateLives();
    }
}
