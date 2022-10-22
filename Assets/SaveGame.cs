using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public Score score;
    public Timer timer;

    public void saveGame()
    {
        if(score.score >= PlayerPrefs.GetInt("Score")) //score always takes priority (Sidenote, score can be infinite, cause of the cherry, so checking the timer doesn't matter)
        {
            PlayerPrefs.SetInt("Score", score.score);
            PlayerPrefs.SetInt("Minutes", timer.minutes);
            PlayerPrefs.SetInt("Seconds", timer.seconds);
            PlayerPrefs.SetInt("Milliseconds", (int)(timer.elapsedTime*100));
        }
    }
}
