using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("Lives", 3);
    }

    public void Level2()
    {
        Debug.Log("On to Level 2!");
    }
}
