using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollisions : MonoBehaviour
{
    public Score stats;
    public AudioSource backgroundMusic;
    public AudioSource audioSource;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Ghost")
        {
            stats.UpdateScore(300);
            StartCoroutine(deadGhost(collider));
        }
    }

    IEnumerator deadGhost(Collider2D collider)
    {
        AudioSource.PlayClipAtPoint(collider.gameObject.GetComponent<AudioSource>().clip, Vector3.zero);
        audioSource.enabled = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;

        audioSource.enabled = true;
        backgroundMusic.loop = true;
        backgroundMusic.clip = backgroundMusic.GetComponent<AudioPlayer>().clips[3];
        backgroundMusic.Play();

        collider.GetComponent<ScareManager>().RecoveringGhost();
    }
}
