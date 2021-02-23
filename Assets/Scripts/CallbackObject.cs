using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackObject : MonoBehaviour
{
	// I create this junk to receive animation callback
	// if better approach is found delete it immediately
	public UIController uic;

	public void AnimationEnds()
	{
		uic.OnAnimationEnds();
	}
}
