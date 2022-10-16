using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostManager : MonoBehaviour
{
    public bool scared = false;
    public TextMeshProUGUI ghostTimer;
    public AudioPlayer backgroundMusic;
    public void powerPellet()
    {
        scared = true;
        foreach(Transform child in transform)
        {
            StartCoroutine(child.GetComponent<ScareManager>().scaredState());
        }
        StopAllCoroutines();
        StartCoroutine(scaredGhosts());
    }

    IEnumerator scaredGhosts()
    {
        ghostTimer.gameObject.SetActive(true);
        backgroundMusic.audioSource.clip = backgroundMusic.clips[2];
        backgroundMusic.audioSource.Play();

        for(int i = 10; i > 3; i--)
        {
            ghostTimer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        //yield return new WaitForSeconds(5); //Because BackToNormal lasts 5 seconds, so in total it's 10 seconds
        foreach(Transform child in transform)
        {
            if(!child.GetComponent<ScareManager>().scared)
            {
                Animator animator = child.GetComponent<Animator>();
                animator.Play("BackToNormal");
            }   
        }
        for(int i = 3; i > 0; i--)
        {
            ghostTimer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        scared = false;
        ghostTimer.gameObject.SetActive(false);

        backgroundMusic.audioSource.loop = true;
        backgroundMusic.audioSource.clip = backgroundMusic.clips[1];
        backgroundMusic.audioSource.Play();
    }
}
