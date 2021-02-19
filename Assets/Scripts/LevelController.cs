using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _currLevelIndex = 1;

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

    public void GoToLevel(int index)
    {
        if (index >= SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        _currLevelIndex = index;
        SceneManager.LoadScene(_currLevelIndex);
    }

    public void FadeToNextLevel()
    {
        animator.SetTrigger("FadeOutTrigger");
    }

    public void OnFadeComplete()
    {
        GoToLevel(_currLevelIndex + 1);
    }
}
