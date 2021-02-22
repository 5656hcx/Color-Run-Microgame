using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private PlayerController player;
    public Animator animator;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (player.getStatus())
        {
            FadeToNextLevel();
        }
    }

    public void FadeToNextLevel()
    {
        animator.SetTrigger("FadeOutTrigger");
    }

    public void OnFadeComplete()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        if (index >= SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        SceneManager.LoadScene(index);
    }
}
