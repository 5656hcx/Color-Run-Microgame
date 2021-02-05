using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamera : MonoBehaviour
{
	public Transform focus;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	Vector3 pos = focus.position;
    	pos.z = -1;
    	transform.position = pos;
    }
}
