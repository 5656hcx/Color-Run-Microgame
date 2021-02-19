using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public WallController Entrance;

    void OnTriggerEnter2D(Collider2D col)
    {
        SpriteRenderer sr = col.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = GetComponent<SpriteRenderer>().sprite;
        sr.color = Color.white;
        Entrance.GetComponent<SpriteRenderer>().color = Color.grey;
        gameObject.SetActive(false);

        //change player's status
        PlayerController player = FindObjectOfType<PlayerController>();
        player.reachDestination();
    }
}
