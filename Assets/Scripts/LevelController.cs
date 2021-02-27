using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using datatype;

public class LevelController : MonoBehaviour
{
    private static Level[] progress;
    private PlayerController player;
    public Animator animator;
    public Image mask;

    private static int currentLevel;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (progress == null)
        {
            progress = XMLHelper.Load<Level>(Level.path);
        }
        mask.gameObject.SetActive(false);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
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
        mask.gameObject.SetActive(true);
        animator.SetTrigger("FadeOutTrigger");

        if (player.getStatus())
        {
            if (currentLevel < progress.Length)
            {
                progress[currentLevel].state = true;
            }
            XMLHelper.Save<Level>(ref progress, Level.path);
        }
    }

    public void OnFadeComplete()
    {
        int index = currentLevel + 1;
        if (index >= SceneManager.sceneCountInBuildSettings)
        {
            index = 0;
        }
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void Menu()
    {
        animator.SetTrigger("MenuExpandTrigger");
    }

    public void Home()
    {
        currentLevel = SceneManager.sceneCountInBuildSettings;
        FadeToNextLevel();
    }

    public void Restart()
    {
        currentLevel = currentLevel - 1;
        FadeToNextLevel();
    }

    public void Music()
    {
        // NOT YET IMPLEMENTED
    }
}
