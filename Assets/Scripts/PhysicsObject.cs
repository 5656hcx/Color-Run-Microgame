using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected bool grounded;
    protected Vector2 velocity;
    protected Vector2 targetVelocity;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected List<RaycastHit2D> hitBuffer = new List<RaycastHit2D>(16);
    
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected bool[] collisions = new bool[4]; // { top,bot,left,right }
    protected HashSet<GameObject> colliders = new HashSet<GameObject>();


    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start() 
    {
        contactFilter.useTriggers = false;
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }
    
    void Update() 
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity ();
    }

    protected virtual void OnCollisionHit()
    {

    }

    protected virtual void ComputeVelocity()
    {
    
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        for (int i=0; i<4; i++)
        {
            collisions[i] = false;
        }
        grounded = false;
        colliders.Clear();

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement (move, false);

        move = Vector2.up * deltaPosition.y;

        Movement (move, true);

        if (colliders.Count > 0) OnCollisionHit();
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance) 
        {
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);

            for (int i = 0; i < count; i++) 
            {
                Vector2 currentNormal = hitBuffer[i].normal;
                colliders.Add(hitBuffer[i].collider.gameObject);

                if (currentNormal.y < -minGroundNormalY)
                {
                    // from top
                    collisions[0] = true;
                }
                else if (currentNormal.y > minGroundNormalY)
                {
                    // from bot
                    collisions[1] = true;
                    grounded = true;
                    if (yMovement) 
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                if (currentNormal.x > minGroundNormalY)
                {
                    // from left
                    collisions[2] = true;
                }
                else if (currentNormal.x < -minGroundNormalY)
                {
                    // from right
                    collisions[3] = true;
                }

                float projection = Vector2.Dot (velocity, currentNormal);
                if (projection < 0) 
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}
