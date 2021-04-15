using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterController : MonoBehaviour
{
    public int speed = 5;
    public Transform point;
    public GameObject bullet;

    private int count = 0;
    private GameObject clone;

    private float firetime = 0.5f;
    private float nexttime = 0.0f;


    void Update()
    {
        if(nexttime < Time.time) {

            nexttime = firetime + Time.time;

            clone = Instantiate(bullet, point.position, point.rotation);
            clone.tag = "Enemy";
            clone.GetComponent<SpriteRenderer>().color = chooseColor();
            clone.GetComponent<Rigidbody2D>().velocity = speed * (count % 10 < 5 ? Vector3.right : Vector3.left);
            count++;
        }

        Destroy(clone, 1); 
    }

    Color chooseColor()
    {
        int iter = 0, max_iter = 10;
        GameObject obj = null;
        
        do {
            int n = Random.Range(0, 4);
            if(n == 0){
                obj = GameObject.Find("Red Gate");
            }else if(n == 1){
                obj = GameObject.Find("Blue Gate");
            }else if(n == 2){
                obj = GameObject.Find("Green Gate");
            }else if(n == 3){
                obj = GameObject.Find("Yellow Gate");
            }else{
                obj = GameObject.Find("White Gate");
            }
        } while (obj == null && iter++ < max_iter);

        return iter++ > max_iter ? Color.white : obj.GetComponent<SpriteRenderer>().color;
    } 
}
