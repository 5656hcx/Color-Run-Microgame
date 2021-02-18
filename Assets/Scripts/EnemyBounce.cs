using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounce : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 150*Time.deltaTime, Space.Self);    
    }
    
    void OnCollisionEnter2D(Collision2D other){
        GetComponent<SpriteRenderer>().color = RandomColor();
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 9), ForceMode2D.Impulse);

        if(other.gameObject.CompareTag ("Player")){
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = RandomColor();
        }
    
    }

    void OnCollisionExit2D(Collision2D other){
        float speed = 5;   
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(-2,-9, 0), speed * Time.deltaTime);

    }

    Color RandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r,g,b);
        return color;
    }
}
 