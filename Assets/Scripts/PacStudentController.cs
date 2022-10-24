using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    Vector3Int lastInput; //Vector3.*direction* will be used.
    [HideInInspector] public Vector3Int movingDirection;
    public Tilemap tilemap;
    public Animator animator;
    public GameObject smokes;
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    [HideInInspector] public Vector3 currentInput;
    bool isMoving = false;
    public float speed;

    public void Start()
    {
        currentInput = transform.position;
        lastInput = Vector3Int.zero;
        audioSource.clip = null;
        audioSource.Stop();
        animator.enabled = false;
    }

    void Update()
    {
        if(Input.anyKeyDown && !audioSource.isPlaying)
        {
            animator.enabled = true;
            audioSource.enabled = true;
            audioSource.loop = true;
            audioSource.Play();
        }
        //Get inputs and update lastInput
        if(Input.GetKeyDown(KeyCode.W))
        {
            lastInput = Vector3Int.up;
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Vector3Int.left;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Vector3Int.down;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Vector3Int.right;
        }

        //when the player reaches the next tile, check if lastInput is a valid tile to move to. If so, update target, so the player moves there
        if(transform.position == currentInput && isMoving)
        {
            Vector3Int nextPos = new Vector3Int((int)(currentInput.x - 0.5f) + lastInput.x, (int)(currentInput.y - 0.5f) + lastInput.y);
            if(tilemap.GetTile(nextPos) == null || tilemap.GetTile(nextPos).name == "Pellet" || tilemap.GetTile(nextPos).name == "PowerPellet" || tilemap.GetTile(nextPos).name == "PacValid") //tile in direction is valid
            {
                currentInput += lastInput;
                movingDirection = lastInput;
            }
            else
            {
                nextPos = new Vector3Int((int)(currentInput.x - 0.5f) + movingDirection.x, (int)(currentInput.y - 0.5f) + movingDirection.y); //this time, check if tile in direction of travel is valid
                if(tilemap.GetTile(nextPos) == null || tilemap.GetTile(nextPos).name == "Pellet" || tilemap.GetTile(nextPos).name == "PowerPellet") //tile in direction is valid
                {
                    currentInput += movingDirection; //"KEEP MOVING FORWARD" (Meet The Robinsons, 2007)
                }
                else // player must stop
                {
                    isMoving = false;
                    animator.Play("Thud", 0);
                    animator.speed = 0;
                    smokes.SetActive(false);

                    audioSource.clip = audioClips[1];
                    audioSource.loop = false;
                    audioSource.Play();
                }
            }
        }
        else if (isMoving)
        {
            animator.speed = 1;
            animator.Play("Rolling", 0);
            smokes.SetActive(true);

            if(movingDirection == Vector3Int.up)
            {
                animator.Play("Up", 1);
            }
            if(movingDirection == Vector3Int.left)
            {
                animator.Play("Left", 1);
            }
            if(movingDirection == Vector3Int.down)
            {
                animator.Play("Down", 1);
            }
            if(movingDirection == Vector3Int.right)
            {
                animator.Play("Right", 1);
            }
            transform.position = Vector3.MoveTowards(transform.position, currentInput, speed * Time.deltaTime);
        }
        else if (movingDirection != lastInput)
        {
            isMoving = true;

            audioSource.clip = audioClips[0];
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
