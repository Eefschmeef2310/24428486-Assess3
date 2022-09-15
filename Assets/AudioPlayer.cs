using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    
    void Start()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(clips[0].length);
        audioSource.loop = true;
        audioSource.clip = clips[1];
        audioSource.Play();
    }
}
