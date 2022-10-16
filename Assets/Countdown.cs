using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void Start()
    {
        text.transform.parent.gameObject.SetActive(true);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        AudioListener.pause = true;
        Time.timeScale = 0;
        for(int i = 3; i > 0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        text.text = "GO!";
        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1;

        text.transform.parent.gameObject.SetActive(false);
        AudioListener.pause = false;
    }
}
