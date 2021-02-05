using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateIntersectionController : MonoBehaviour
{
    public PlayerController player;
    private Color originColor;

    public bool IsIntersect()
    {
        Vector3 moveOrthogonPos = player.transform.position;
        Vector3 smallOrthogonPos = this.transform.position;

        float x1 = this.GetComponent<Renderer>().bounds.size.x;
        float x2 = player.GetComponent<Renderer>().bounds.size.x;

        float halfSum_X = (x1 + x2) * 0.5f;
        float distance_X = Mathf.Abs(moveOrthogonPos.x - smallOrthogonPos.x);

        if (distance_X <= halfSum_X + 0.03)
        {
        	// Debug.Log("is intersect");
            return true;
        }
        else
        {
        	// Debug.Log("not intersect");
            return false;
        }
    }

    public void ReColor()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Color color = this.GetComponent<SpriteRenderer>().color;
            color.a = 1;
            player.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void BackToOriginColor()
    {
        player.GetComponent<SpriteRenderer>().color = originColor;
    }
    
    void Start()
    {
        originColor = player.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (IsIntersect())
        {
            ReColor();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            BackToOriginColor();
        }
    }

}