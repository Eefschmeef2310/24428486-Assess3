using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public GameObject endText;
    public AudioSource audioSource;
    public AudioPlayer audioPlayer;
    public SaveGame saveGame;
    public TextMeshProUGUI timer;
    public GhostManager ghostManager;
    public IEnumerator gameComplete(bool winLose)
    {
        TextMeshProUGUI lastText = endText.GetComponent<TextMeshProUGUI>();
        endText.SetActive(true);
        if(winLose)
        {
            audioSource.clip = audioPlayer.clips[4]; //play winning sound
            lastText.text = "YOU WIN!";
        }
        else
        {
            audioSource.clip = audioPlayer.clips[5]; //play winning sound
            lastText.text = "GAME OVER";
        }
        
        audioSource.enabled = true;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        saveGame.saveGame();
        SceneManager.LoadScene(0);

    }
}
