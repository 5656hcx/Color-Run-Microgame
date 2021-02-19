using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;

    private PlayerController player;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<PlayerController>();


        if (!player.getStatus())
        {
            // player doesn't reach the destination
            return;
        }

        
        FadeToNextLevel();
    }

    public void FadeToNextLevel()
    {
        animator.SetTrigger("FadeOutTrigger");
    }



    public void OnFadeComplete()
    {
        Debug.Log("Load next level");
        //_nextLevelIndex++;
        //string nextLevelName = "Level" + _nextLevelIndex;
        //SceneManager.LoadScene(nextLevelName);
    }
}
