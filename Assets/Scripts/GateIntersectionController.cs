using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateIntersectionController : MonoBehaviour
{
   
    private Vector4 originColor;

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

        if (distance_X <= halfSum_X+0.03)
        {
            isIntersect = true;
            //Debug.Log("is intersect");
        }
        else
        {
            isIntersect = false;
            //Debug.Log("not intersect");
        }
        return isIntersect;
    }

    public void reColor(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           
            Vector4 color = this.gameObject.GetComponent<SpriteRenderer>().color;
            color[3] = 1;
            obj.GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void backToOriginColor(GameObject obj)
    {

        obj.GetComponent<SpriteRenderer>().color = originColor;

    }
    // Start is called before the first frame update
    void Start()
    {
        originColor = GameObject.Find("character").GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        bool intersect = IsIntersect();
        GameObject obj = GameObject.Find("character");
        if (intersect)
        {
            //Debug.Log("is intersect");
            reColor(obj);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Debug.Log("not intersect");
            backToOriginColor(obj);
        }
    }

}