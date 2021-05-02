using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerController : PhysicsObject {

    public float horizontalSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    // public CapsuleCollider2D PlayerCollider;

    private SpriteRenderer spriteRenderer;
    // private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        // PlayerCollider = GetComponent<CapsuleCollider2D>();
        // animator = GetComponent<Animator> ();
    }

    public void ResetPhysics()
    {
        this.velocity = Vector2.zero;
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

                // Analytics - Hit by Enemy
                // 
                AnalyticsEvent.Custom("Hit_By_Enemy", new Dictionary<string, object> {});
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
}