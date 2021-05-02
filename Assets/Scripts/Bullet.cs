using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.tag == "Player")
		{
			obj.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
		}
	}
}
