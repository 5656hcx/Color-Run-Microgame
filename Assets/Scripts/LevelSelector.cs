using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
	public string TitleBeginWith;
	public TextMeshProUGUI Title;
	public Sprite[] Thumbnails;
	public Image Thumbnail;

	private static int level_index = 1;
	private const string img_name = "Level_";

	void Start()
	{
		Title.text = TitleBeginWith + level_index;
		Thumbnail.sprite = Thumbnails[level_index - 1];
	}

    public void Play()
    {
    	SceneManager.LoadScene(level_index);
    }

    public void Next()
    {
    	level_index++;
    	if (level_index >= SceneManager.sceneCountInBuildSettings)
    	{
    		level_index = 1;
    	}
    	Start();
    }
}
