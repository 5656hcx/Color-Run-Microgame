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
    public Animator animator_menu;

    public Image mask;
    public Image panel;
    public Button menu;
    public Sprite[] menuSprites = new Sprite[2];

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (progress == null)
        {
            progress = XMLHelper.Load<Level>(Level.path);
        }
        mask.gameObject.SetActive(false);
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

    public void MenuClicked()
    {
        if (menu.GetComponent<Image>().sprite == menuSprites[0])
        {
            menu.GetComponent<Image>().sprite = menuSprites[1];
            animator.SetTrigger("MenuExpandTrigger");
        }
        else
        {
            menu.GetComponent<Image>().sprite = menuSprites[0];
            animator.SetTrigger("MenuExpandTrigger");
        }
    }
}
