using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    
    public void Start()
    {
        audioSource.clip = clips[0];
        if(audioSource.enabled)
        {
            audioSource.Play();
        }
        StopAllCoroutines();
        StartCoroutine(PlayMusic());
    }

    public IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        if(audioSource.clip == clips[0])
        {
            audioSource.loop = true;
            audioSource.clip = clips[1];
            if(audioSource.enabled)
            {
                audioSource.Play();
            }
        }
    }
}
