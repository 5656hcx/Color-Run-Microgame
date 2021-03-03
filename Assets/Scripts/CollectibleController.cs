using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectibleController : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			OnItemCollected(col.gameObject);
		}
	}

	public virtual void OnItemCollected(GameObject obj)
	{

	}
}
