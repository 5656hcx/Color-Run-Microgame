using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
	public WallController Entrance;

    void OnTriggerEnter2D(Collider2D col)
    {
    	col.gameObject.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
    	Entrance.GetComponent<SpriteRenderer>().color = Color.grey;
    	gameObject.SetActive(false);
    }
}
