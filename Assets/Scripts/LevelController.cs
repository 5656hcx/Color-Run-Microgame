using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

using datatype;

public class LevelController : MonoBehaviour
{
    private static Level[] progress;
    public Animator animator;
    public Image mask;
    public PlayerStatistics localPlayerData;

    private static int currentLevel;
    private static bool completed;

    private GameObject playerEntity;

    void Start()
    {
        playerEntity = GameObject.FindWithTag("Player");
        completed = false;
        if (progress == null)
        {
            progress = XMLHelper.Load<Level>(Level.path);
        }
        mask.gameObject.SetActive(false);
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        localPlayerData = GlobalControl.Instance.savedPlayerData;

        // Analytics - Level Reached
        // 
        AnalyticsEvent.Custom("Level_Reached", new Dictionary<string, object>
        {
            { "level_id", currentLevel },
        });
    }

    void Update()
    {
        if (completed == true)
        {
            FadeToNextLevel();
            localPlayerData.Gems = GemsUI.CurrentGemQuantity;
            GlobalControl.Instance.savedPlayerData = localPlayerData;
        }
    }

    public void FadeToNextLevel()
    {
        mask.gameObject.SetActive(true);
        animator.SetTrigger("FadeOutTrigger");

        if (completed)
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

        // Analytics - Level Completed
        // 
        AnalyticsEvent.Custom("Level_Completed", new Dictionary<string, object>
        {
            { "level_id", currentLevel },
            { "time_elapsed", Time.timeSinceLevelLoad },
            { "coin_collected", localPlayerData.Gems }
        });

        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public static void OnLevelCompleted()
    {
        completed = true;
    }

    public void Menu()
    {
        Debug.Log(playerEntity.transform.position);
        animator.SetTrigger("MenuExpandTrigger");
    }

    public void Home()
    {
        // Analytics - Quit Level
        // 
        AnalyticsEvent.Custom("Quit_Level", new Dictionary<string, object>
        {
            { "position_x", playerEntity.transform.position.x },
            { "position_y", playerEntity.transform.position.y },
            { "level_id", currentLevel }
        });

        currentLevel = SceneManager.sceneCountInBuildSettings;
        FadeToNextLevel();
    }

    public void Restart()
    {
        // Analytics - Replay Level
        // 
        AnalyticsEvent.Custom("Replay_Level", new Dictionary<string, object>
        {
            { "position_x", playerEntity.transform.position.x },
            { "position_y", playerEntity.transform.position.y },
            { "level_id", currentLevel }
        });

        currentLevel = currentLevel - 1;
        FadeToNextLevel();
    }

    public void Music()
    {
        // NOT YET IMPLEMENTED
    }
}
