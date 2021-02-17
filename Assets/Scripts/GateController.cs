using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public PlayerController player;
    private Color color;
    private bool colorable;
    
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
        color.a = 1;
        colorable = false;
    }

    void Update()
    {
        if (colorable && Input.GetKeyDown(KeyCode.R))
        {
            player.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        colorable = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        colorable = false;
    }

}