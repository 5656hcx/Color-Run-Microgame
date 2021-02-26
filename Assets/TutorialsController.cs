using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsController : MonoBehaviour
{
    // popups of tutorials
    public GameObject[] popUps;
    private static int popUpIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hi");
        popUps[popUpIndex].SetActive(true);
        GetComponent<BoxCollider2D>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (popUpIndex == 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
        else if (popUpIndex == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
        else if (popUpIndex == 2 && Input.GetKeyDown(KeyCode.R))
        {
            popUps[popUpIndex].SetActive(false);
            popUpIndex++;
        }
        else if (popUpIndex == 3 && Input.GetKeyDown(KeyCode.Space))
        {
            popUps[popUpIndex].SetActive(false);
            //popUpIndex = 0;
        }
    }
}
