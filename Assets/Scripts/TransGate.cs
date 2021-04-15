using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransGate : MonoBehaviour
{ 
    private bool trigger;
    private GameObject player;

    [Header("References")]
    public TransGate target;
    public GameObject gateParticleSystem;

    void Update()
    {
        if (trigger && Input.GetKeyDown(KeyCode.G))
        {
            player.SetActive(false);
            gateParticleSystem.SetActive(true);
            gateParticleSystem.transform.position = player.transform.position;
            Invoke("DeactivateGemGameObject", gateParticleSystem.GetComponent<ParticleSystem>().main.duration);
        }
    }

    void DeactivateGemGameObject()
    {
        player.transform.position = target.transform.position + Vector3.right;
        player.SetActive(true);
        gateParticleSystem.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            trigger = true;
            player = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            trigger = false;
        }
    }

}