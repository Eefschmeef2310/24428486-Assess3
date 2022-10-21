using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
    public Canvas endCanvas;
    public TextMeshProUGUI wonText;
    public AudioSource audioSource;
    public AudioPlayer audioPlayer;
    public SaveGame saveGame;
    public IEnumerator gameComplete(bool winLose)
    {
        if(winLose)
        {
            audioSource.clip = audioPlayer.clips[4]; //play winning sound
        }
        
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        saveGame.saveGame();
        SceneManager.LoadScene(0);

    }
}
