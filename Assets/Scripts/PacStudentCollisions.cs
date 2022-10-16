using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PacStudentCollisions : MonoBehaviour
{
    public PacStudentController pacStudentController;
    public Transform ghosts;
    public Stats stats;
    public Animator animator;
    public AudioPlayer backgroundMusic;
    public AudioSource backgroundPlayer;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public Sprite defaultSprite;
    public Countdown countdown;
    public List<Image> images;
    int lives = 3;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch(collider.gameObject.tag)
        {
            case "Teleporter":
                if(collider.gameObject.name == "Left")
                {
                    leftTeleporter();
                }
                else
                {
                    rightTeleporter();
                }
                break;
            case "Collectable":
                if(collider.gameObject.name == "Pellet(Clone)")
                {
                    collectableCollision(collider, 10);
                }
                else if(collider.gameObject.name == "Cherry(Clone)")
                {
                    collectableCollision(collider, 100);
                }
                else if(collider.gameObject.name == "PowerPellet(Clone)")
                {
                    Destroy(collider.gameObject);
                    ghosts.GetComponent<GhostManager>().powerPellet();
                }
                break;
            case "Ghost":
                if(ghosts.GetComponent<GhostManager>().scared)
                {
                    stats.UpdateScore(300);
                    StartCoroutine(deadGhost(collider));
                }
                else
                {
                    StartCoroutine(deadPlayer());
                }
                break;
        }
    }

    void leftTeleporter()
    {
        transform.position = new Vector3(12.5f, 0.5f);
        pacStudentController.currentInput = new Vector2(11.5f, 0.5f);
    }

    void rightTeleporter()
    {
        transform.position = new Vector2(-12.5f, 0.5f);
        pacStudentController.currentInput = new Vector2(-11.5f, 0.5f);
    }

    void collectableCollision(Collider2D collider2D, int amount)
    {
        Destroy(collider2D.gameObject);
        stats.UpdateScore(amount);
    }

    IEnumerator deadPlayer()
    {
        audioSource.Stop();
        backgroundPlayer.enabled = false;

        pacStudentController.enabled = false; //stop movement

        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(2); //let the game hang for two seconds, letting the player absorb the impact before losing a life

        Time.timeScale = 1.0f;
        foreach(Transform child in ghosts)
        {
            child.gameObject.SetActive(false);
        }

        audioSource.clip = deathSound;
        audioSource.loop = false;
        animator.Play("Exit Machine"); //stop layer 1
        
        audioSource.Play();
        animator.Play("PacDeath", 0);

        switch(lives)
        {
            case 3:
                lives--;
                images[2].enabled = false;
                break;
            case 2:
                lives--;
                images[1].enabled = false;
                break;
            case 1:
                lives--;
                images[0].enabled = false;
                break;
            case 0:
                Debug.Log("player is dead");
                break;
        }
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[5].length);

        if(lives != 0)
        {
            resetLevel();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    void resetLevel()
    {
        transform.position = new Vector3(-12.5f, 13.5f);

        animator.enabled = false;
        transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        audioSource.enabled = false;

        pacStudentController.enabled = true;
        pacStudentController.Start();

        foreach(Transform child in ghosts)
        {
            child.gameObject.SetActive(true);
        }

        backgroundPlayer.enabled = true;
        backgroundMusic.enabled = true;
        backgroundMusic.Start();
        countdown.Start();
    }

    IEnumerator deadGhost(Collider2D collider)
    {
        audioSource.enabled = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;

        audioSource.enabled = true;
        backgroundPlayer.loop = true;
        backgroundPlayer.clip = backgroundMusic.clips[3];
        backgroundPlayer.Play();

        ScareManager scareManager = collider.GetComponent<ScareManager>();

        StopCoroutine(scareManager.scaredState());
        StartCoroutine(scareManager.RecoveringGhost());
    }
}
