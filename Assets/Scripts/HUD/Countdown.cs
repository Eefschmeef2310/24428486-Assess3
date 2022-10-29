using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI text;
    public PacStudentController pacStudentController;
    public Collider2D collide;
    public Timer timer;
    public GhostStarter ghostStarter;
    public void Start()
    {
        text.transform.parent.gameObject.SetActive(true);
        pacStudentController.enabled = false;
        collide.enabled = false;
        timer.enabled = false;
        ghostStarter.enabled = false;
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

        pacStudentController.enabled = true;
        collide.enabled = true;
        timer.enabled = true;
        ghostStarter.enabled = true;

        text.transform.parent.gameObject.SetActive(false);
        AudioListener.pause = false;
    }
}
