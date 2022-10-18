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
    public Stats stats;
    public Animator animator;
    public AudioSource backgroundMusic;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public Lives lives;
    public GameObject map;
    public Timer timer;
    GameObject newMap;
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
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("newMap") != null)
        {
            Destroy(GameObject.FindGameObjectWithTag("Map"));
            map = GameObject.FindGameObjectWithTag("newMap");
            map.tag = "Map";
            pacStudentController.tilemap = map.transform.GetChild(0).GetComponent<Tilemap>();
        }
    }

    IEnumerator deadPlayer()
    {
        DontDestroyOnLoad(map);
        map.tag = "newMap";

        audioSource.Stop();
        backgroundMusic.Stop();
        timer.enabled = false;

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

        lives.RemoveLife();
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[5].length);
        SceneManager.LoadScene(1);
    }

    /*
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
    */

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
}
