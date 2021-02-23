using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject
{
	public float JumpSpeed = 10;

	protected override void ComputeVelocity()
	{
		if (grounded)
		{
			velocity.y = JumpSpeed;
		}
	}

	protected override void OnCollisionHit()
	{
		foreach (GameObject obj in colliders)
		{
			if (obj.tag == "Player")
			{
				obj.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;
				break;
			}

			if (obj.tag == "Ground" && grounded)
			{
				GetComponent<SpriteRenderer>().color = RandomColor();
				break;
			}
		}
	}

	/*
	void Update()
	{
        transform.Rotate(0, 0, 150*Time.deltaTime, Space.Self);    
	}

	void OnCollisionExit2D(Collision2D other)
	{
		float speed = 5;
		gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(-2,-9, 0), speed * Time.deltaTime);
	}
	*/

	protected Color RandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        return new Color(r,g,b);
    }
}
