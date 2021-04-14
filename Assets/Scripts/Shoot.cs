using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	int Speed = 5;
	public GameObject Bullet;
	public Transform point;
    private GameObject clone;
    private bool check;
    int count = 0;

    float firetime = 0.5f;
    float nexttime = 0.0f;

    public GameObject gateColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	// if(Input.GetKeyDown(KeyCode.J)){
        if(nexttime < Time.time){

            nexttime = firetime + Time.time;
			
			clone = Instantiate(Bullet, point.position, point.rotation);
            if(count %10 < 5){
			    clone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.right*Speed);
                 
            }else{
                clone.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(Vector3.left*Speed);
                 
            }

            clone.GetComponent<SpriteRenderer>().color = chooseColor();
            count++;
              

		} 

       
        Destroy(clone, 1); 

    }




    Color chooseColor(){

        GameObject obj;
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

        return obj.GetComponent<SpriteRenderer>().color;
    } 
}
