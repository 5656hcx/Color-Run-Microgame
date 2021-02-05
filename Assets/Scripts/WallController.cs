using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{

    public bool IsIntersect()
    {
        bool isIntersect;
        Transform moveOrthogon = GameObject.Find("character").transform;
        Vector3 moveOrthogonPos = moveOrthogon.position;

        Vector3 smallOrthogonPos = this.gameObject.transform.position;

        float x1 = this.gameObject.GetComponent<Renderer>().bounds.size.x;
        float x2 = GameObject.Find("character").GetComponent<Renderer>().bounds.size.x;

        float halfSum_X = (x1 + x2) * 0.5f;
        float distance_X = Mathf.Abs(moveOrthogonPos.x - smallOrthogonPos.x);

        if (distance_X <= halfSum_X + 0.03)
        {
            isIntersect = true;
            Debug.Log("is intersect");
        }
        else
        {
            isIntersect = false;
            //Debug.Log("not intersect");
        }
        return isIntersect;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        bool intersect = IsIntersect();
        GameObject obj = GameObject.Find("character");

        Color colorOfCharacter = obj.GetComponent<SpriteRenderer>().color;
        Color colorOfWall = this.gameObject.GetComponent<SpriteRenderer>().color;

        if (intersect && colorOfCharacter.Equals(colorOfWall))
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
       
    }
}
