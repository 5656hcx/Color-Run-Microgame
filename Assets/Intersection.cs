using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    private Vector4 originColor;

	public bool IsIntersect()
    {
        bool isIntersect;
        Transform moveOrthogon = GameObject.Find("square").transform;
        Vector3 moveOrthogonPos = moveOrthogon.position;

        Vector3 smallOrthogonPos = this.gameObject.transform.position;

        float x1 = this.gameObject.GetComponent<Renderer>().bounds.size.x;
        float x2 = GameObject.Find("square").GetComponent<Renderer>().bounds.size.x;
        
        float halfSum_X = (x1+x2)*0.5f;
        float distance_X = Mathf.Abs(moveOrthogonPos.x - smallOrthogonPos.x);
       
        if (distance_X <= halfSum_X)
        {
            isIntersect = true;
            Debug.Log("is intersect");
        }
        else
        {
            isIntersect = false;
            Debug.Log("not intersect");
        }
        return isIntersect;
    }

    public void reColor(GameObject obj){
        if(Input.GetKeyDown(KeyCode.R)){   
            Vector4 color = this.gameObject.GetComponent<SpriteRenderer>().color;
            color[3] = 1;
            obj.GetComponent<Renderer>().material.color = color;
        }
    }

    public void backToOriginColor(GameObject obj){
          
        obj.GetComponent<Renderer>().material.color = originColor;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        originColor = GameObject.Find("square").GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        bool intersect = IsIntersect();
        GameObject obj = GameObject.Find("square");
        if(intersect){
            reColor(obj);
        }
        if(Input.GetKeyDown(KeyCode.B)){
            backToOriginColor(obj);
        }
    }

}
