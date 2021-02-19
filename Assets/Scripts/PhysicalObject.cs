using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    protected const float MIN_MOVE_DISTANCE = 0.001f;

    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected Vector2 velocity;
    protected Vector2 targetVelocity;

    public float horizontalSpeed = 4f;
    public float gravityModifier = 1f;

    private readonly List<RaycastHit2D> raycastHit2DList = new List<RaycastHit2D>();

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        contactFilter = new ContactFilter2D
        {
            useTriggers = false,
            useLayerMask = true,
            layerMask = Physics2D.GetLayerCollisionMask(gameObject.layer)
        };
    }

    void Update()
    {
        velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        Vector2 pos = velocity * Time.deltaTime * horizontalSpeed;

        Movement(pos * Vector2.up);

        Movement(pos * Vector2.right);
    }

    protected virtual void ComputeVelocity()
    {
    
    }

    private void Movement(Vector2 move)
    {
        if (move == Vector2.zero) return;

        float distance = move.magnitude;
        Vector2 direction = move.normalized;
                  
        Vector2 updateDeltaPosition = Vector2.zero;

        rb2d.Cast(direction, contactFilter, raycastHit2DList, distance);
        
        Vector2 finalDirection = direction;
        float finalDistance = distance;
        
        foreach (var hit in raycastHit2DList)
        {
                Vector2 currentNormal = hit.normal;
                if (currentNormal.y != 0) currentNormal.x = 0; 

                 float moveDistance = hit.distance;
                 

                 float projection = Vector2.Dot(currentNormal, direction);

                 if (projection >= 0)
                 {
                     moveDistance = distance;
                 }
                 
                 if (moveDistance < finalDistance)
                 {
                     finalDistance = moveDistance;
                 }
        }
        
        updateDeltaPosition += finalDirection * finalDistance;

        rb2d.position += updateDeltaPosition;
    }
}