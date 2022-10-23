using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public Countdown countdown;
    public AudioSource backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void BeginGame()
    {
        countdown.enabled = true;
        backgroundMusic.enabled = true;
        AudioListener.pause = false;
        gameObject.SetActive(false);
    }
}
