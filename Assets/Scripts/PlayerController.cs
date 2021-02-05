using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private float horizontal;
    private float move;
    private bool flag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flag = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!flag)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                flag = true;
            }
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (flag)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                flag = false;
            }
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

}
