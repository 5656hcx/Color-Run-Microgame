using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public PlayerController player;
    private BoxCollider2D selfCollider;

    void Start()
    {
        selfCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Color colorOfPlayer = player.GetComponent<SpriteRenderer>().color;
        Color colorOfWall = this.GetComponent<SpriteRenderer>().color;

        if (IsIntersect() && colorOfPlayer.Equals(colorOfWall))
        {
            selfCollider.isTrigger = true;
        }
        else
        {
            selfCollider.isTrigger = false;
        }
    }

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
}
