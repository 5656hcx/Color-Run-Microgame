using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformController : PhysicsObject
{

    public float startPos;
    public float endPos;
    public bool moveUp;
    public bool moveVertically;

    public float verticalSpeed = 1.0f;

    void Awake()
    {
        
    }
     
    protected override void ComputeVelocity()
    {
        if (transform.position.y > endPos)
        {
            moveUp = false;
        }

        if (transform.position.y < startPos)
        {
            moveUp = true;
        }


        if (moveUp)
        {
            velocity.y = verticalSpeed;
        }
        else
        {
            velocity.y = -verticalSpeed;
        }


    }
}
