using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    public void ExitScene()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitScene();
        }
    }
}
