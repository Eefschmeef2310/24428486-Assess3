using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadSaves : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI timer;
    void Start()
    {
        score.text = "Highest Score: " + PlayerPrefs.GetInt("Score");
        timer.text = "Best Time: " + PlayerPrefs.GetInt("Minutes") + ":" + PlayerPrefs.GetInt("Seconds") + ":" + PlayerPrefs.GetInt("Milliseconds");
    }
}
