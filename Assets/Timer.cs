using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    float elapsedTime;
    int minutes;
    int seconds;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > 1)
        {
            seconds++;
            elapsedTime--;
        }
        if(seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }
        timer.text = minutes + ":" + seconds + ":" + (int)(elapsedTime*100);
    }
}
