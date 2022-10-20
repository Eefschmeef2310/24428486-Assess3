using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PacStudentCollisions : MonoBehaviour
{
    public PacStudentController pacStudentController;
    public Transform ghosts;
    public Score stats;
    public Animator animator;
    public AudioSource backgroundMusic;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public Lives lives;
    public Countdown countdown;
    public Timer timer;
    public CherryController cherryController;
    int hits;
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
                    hits++;
                    if(hits == 218) //every pellet eaten
                    {
                        StartCoroutine(GameComplete());
                    }
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
        backgroundMusic.Stop();
        backgroundMusic.enabled = false;
        timer.enabled = false;

        pacStudentController.enabled = false; //stop movement

        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(2); //let the game hang for two seconds, letting the player absorb the impact before losing a life
        Time.timeScale = 1.0f;

        turnOffGhosts();

        cherryController.StopAllCoroutines();

        foreach(GameObject collectable in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            if(collectable.name == "Cherry(Clone)")
            {
                Destroy(collectable);
            }
        }

        audioSource.clip = deathSound;
        audioSource.loop = false;
        animator.Play("Exit Machine", 1); //stop layer 1
        audioSource.Play();

        animator.Play("PacDeath", 0);

        lives.RemoveLife();
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[5].length);
        resetLevel();
    }

    void resetLevel()
    {
        transform.position = new Vector3(-12.5f, 13.5f);

        animator.Play("Rolling", 0);

        pacStudentController.enabled = true;
        pacStudentController.Start();

        foreach(Transform child in ghosts)
        {
            child.gameObject.SetActive(true);
            child.GetComponent<ResetPosition>().Start();
        }

        backgroundMusic.enabled = true;
        backgroundMusic.transform.GetComponent<AudioPlayer>().Start();
        audioSource.clip = pacStudentController.audioClips[0];
        countdown.Start();
    }

    IEnumerator deadGhost(Collider2D collider)
    {
        audioSource.enabled = false;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;

        audioSource.enabled = true;
        backgroundMusic.loop = true;
        backgroundMusic.clip = backgroundMusic.GetComponent<AudioPlayer>().clips[3];
        backgroundMusic.Play();

        ScareManager scareManager = collider.GetComponent<ScareManager>();

        StopCoroutine(scareManager.scaredState());
        StartCoroutine(scareManager.RecoveringGhost());
    }
    IEnumerator GameComplete()
    {
        Debug.Log("Game complete!");

        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(2); //let the game hang for two seconds, letting the player absorb the impact before losing a life
        Time.timeScale = 1.0f;

        turnOffGhosts();

        //TODO: add a fun animation of the player winning the game, then save prefs+load the menu (DO THIS IN A DIFFERENT OBJECT THIS TIME)

        yield return null;
    }

    void turnOffGhosts()
    {
        foreach(Transform child in ghosts)
        {
            child.gameObject.SetActive(false);
        }
    }
}
