using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using datatype;

public class LevelController : MonoBehaviour
{
    private static Level[] progress;
    private PlayerController player;
    public Animator animator;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (progress == null)
        {
            progress = XMLHelper.Load<Level>(Level.path);
        }
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
        if (SceneManager.GetActiveScene().buildIndex < progress.Length)
        {
            progress[SceneManager.GetActiveScene().buildIndex].state = true;
        }
        XMLHelper.Save<Level>(ref progress, Level.path);
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
