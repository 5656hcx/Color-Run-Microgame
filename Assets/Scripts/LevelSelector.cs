using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public Transform LevelPanel;
    public LevelEntry EntryPrefab;

	void Start()
	{
        for (int i=1; i<SceneManager.sceneCountInBuildSettings; i++)
        {
            LevelEntry entry = Instantiate(EntryPrefab);
            entry.transform.SetParent(LevelPanel);
            entry.Init(i, false);
        }
	}

    public void Refresh()
    {

    }
}
