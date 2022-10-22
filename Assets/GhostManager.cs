using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostManager : MonoBehaviour
{
    public bool scared = false;
    public TextMeshProUGUI ghostTimer;
    public AudioSource backgroundMusic;
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
        backgroundMusic.clip = backgroundMusic.GetComponent<AudioPlayer>().clips[2];
        backgroundMusic.Play();

        for(int i = 10; i > 3; i--)
        {
            ghostTimer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        foreach(Transform child in transform)
        {
            if(child.GetComponent<ScareManager>().scared)
            {
                Animator animator = child.GetComponent<Animator>();
                animator.enabled = true;
                animator.Play("BackToNormal");
            }   
        }
        for(int i = 3; i > 0; i--)
        {
            ghostTimer.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        scared = false;
        foreach(Transform child in transform)
        {
            if(child.GetComponent<ScareManager>().scared)
            {
                child.GetComponent<ScareManager>().scared = false;
            }
        }
        ghostTimer.gameObject.SetActive(false);

        backgroundMusic.loop = true;
        backgroundMusic.clip = backgroundMusic.GetComponent<AudioPlayer>().clips[1];
        backgroundMusic.Play();
    }
}
