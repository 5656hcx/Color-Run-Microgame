using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {
        if (player.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

}
