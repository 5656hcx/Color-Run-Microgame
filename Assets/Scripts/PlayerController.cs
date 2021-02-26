using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public float horizontalSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    // public CapsuleCollider2D PlayerCollider;

    private SpriteRenderer spriteRenderer;
    // private Animator animator;

    // whether the player reaches the destination 
    private bool curStatus;

    // popups of tutorials
    public GameObject[] popUps;
    private int popUpIndex;


    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        // PlayerCollider = GetComponent<CapsuleCollider2D>();
        // animator = GetComponent<Animator> ();
        curStatus = false;
        popUpIndex = 0;
    }

    protected override void OnCollisionHit()
    {
        // ugly solution
        // discard it if better approach is found
        foreach (GameObject obj in colliders)
        {
            if (obj.tag == "Enemy")
            {
                GetComponent<SpriteRenderer>().color = obj.GetComponent<SpriteRenderer>().color;
                break;
            }
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp ("Jump")) 
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        if (move.x > minMoveDistance)
        {
            spriteRenderer.flipX = false;
        }
        else if (move.x < -minMoveDistance)
        {
            spriteRenderer.flipX = true;
        }

        //animator.SetBool ("grounded", grounded);
        //animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / horizontalSpeed);

        targetVelocity = move * horizontalSpeed;
    }

    public bool getStatus()
    {
        return curStatus;
    }

    public void reachDestination()
    {
        curStatus = true;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "popUps" && popUpIndex < popUps.Length)
        {
            popUps[popUpIndex].SetActive(true);
            col.enabled = false;
        }

    }

    protected override void HandlePopUps()
    {
        if (popUpIndex == 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
        else if (popUpIndex == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
        else if (popUpIndex == 2 && Input.GetKeyDown(KeyCode.R))
        {
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
        else if (popUpIndex == 3 && Input.GetKeyDown(KeyCode.Space))
        {
            popUps[popUpIndex].SetActive(false);
            //popUpIndex = 0;
        }
    }
}