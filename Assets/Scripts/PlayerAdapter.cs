using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdapter
{
	private PlayerAdapter() {}

	public static PhysicsObject player;

	public static bool SetVelocity(Vector2 vec)
	{
		if (player == null) return false;
		player.SetVelocity(vec);
		return true;
	}
}
