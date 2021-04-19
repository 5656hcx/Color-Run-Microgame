using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformController : PhysicsObject
{

    public float speed;
    public float waitTime;
    public Transform[] movePos;


    private int i;
    private Transform playerDefTransform;

    void Awake()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        
    }

    protected override void ComputeVelocity()
    {

        transform.position = Vector2.MoveTowards(transform.position, movePos[i].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {

            if (waitTime < 0.0f)
            {
                if (i == 1)
                {
                    i = 0;
                }
                else
                {
                    i = 1;
                }

                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.transform.parent = gameObject.transform;
        }
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.gameObject.transform.parent = playerDefTransform;
        }
    }
}
