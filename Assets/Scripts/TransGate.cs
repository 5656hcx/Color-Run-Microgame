using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransGate : MonoBehaviour
{ 
	public GameObject target;
    public PlayerController player;
    private bool trigger;
    Collider2D col;


    [Header("References")]
    public GameObject gemVisuals;
    public GameObject collectedParticleSystem;
    public BoxCollider2D gemCollider2D;

    private float durationOfCollectedParticleSystem;

    void Start()

    {

        // durationOfCollectedParticleSystem = collectedParticleSystem.GetComponent<ParticleSystem>().main.duration;

    }

    void Update()

    {
        if (trigger && Input.GetKeyDown(KeyCode.G))
        {
            durationOfCollectedParticleSystem = collectedParticleSystem.GetComponent<ParticleSystem>().main.duration;
            
            collectedParticleSystem.SetActive (true);
            Invoke ("DeactivateGemGameObject", durationOfCollectedParticleSystem);
   
           gemVisuals.SetActive(false);
        }
        
    }


    void DeactivateGemGameObject()
    {
        col.gameObject.transform.position = target.transform.position + new Vector3(1.5f, 0, 0);
        gemVisuals.SetActive(true);
        print(target.transform.position[0]);
        collectedParticleSystem.SetActive (false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        // Vector3 tempVec =(collision.gameObject.transform.position - this.transform.position)*1.5f;

        if (collision.gameObject.name== "Player") {

            print(collision.gameObject.transform.position[0]);
            trigger = true;
            col = collision;
        }
       
    }

    void OnTriggerExit2D(Collider2D col)
    {
        trigger = false;

    }

}