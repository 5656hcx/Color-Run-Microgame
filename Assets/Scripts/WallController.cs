using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public PlayerController player;
    private Color color;

    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (player.GetComponent<SpriteRenderer>().color == color)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

}
