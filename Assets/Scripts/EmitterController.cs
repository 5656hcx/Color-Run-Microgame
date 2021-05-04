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

    private float gravity_right = 0;
    private float gravity_left = 0;

    public enum ColorMode { RANDOM, FIXED };

    public ColorMode colorMode = ColorMode.RANDOM;
    public Color color;

    void Update()
    {
        if(nexttime < Time.time) {

            nexttime = firetime + Time.time;

            clone = Instantiate(bullet, point.position, point.rotation);
            if (colorMode == ColorMode.RANDOM)
            {
                 clone.GetComponent<SpriteRenderer>().color = chooseColor();
            }
            else clone.GetComponent<SpriteRenderer>().color = color;

            Rigidbody2D rb2d = clone.GetComponent<Rigidbody2D>();
            if (count % 10 < 5)
            {
                rb2d.velocity = speed * Vector3.right;
                rb2d.AddForce(new Vector3(0, gravity_right, 0));
            }
            else
            {
                rb2d.velocity = speed * Vector3.left;
                rb2d.AddForce(new Vector3(0, gravity_left, 0));
            }

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
