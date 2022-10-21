using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public Score score;
    public Timer timer;

    public void saveGame()
    {
        PlayerPrefs.SetInt("Score", score.score);
        PlayerPrefs.SetInt("Minutes", timer.minutes);
        PlayerPrefs.SetInt("Seconds", timer.seconds);
        PlayerPrefs.SetInt("Milliseconds", (int)(timer.elapsedTime*100));
    }
}
