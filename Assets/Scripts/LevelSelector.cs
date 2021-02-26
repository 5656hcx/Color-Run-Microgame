using UnityEngine;
using UnityEngine.SceneManagement;

using datatype;

public class LevelSelector : MonoBehaviour
{
    public Transform LevelPanel;
    public LevelEntry EntryPrefab;

	void Start()
	{
		Level[] progress = XMLHelper.Load<Level>(Level.path);
		if (progress.Length == 0)
		{
			progress = new Level[SceneManager.sceneCountInBuildSettings - 1];
			for (int i=0; i<progress.Length; i++)
			{
				progress[i] = new Level(i+1);
			}
			progress[0].state = true;
			XMLHelper.Save<Level>(ref progress, Level.path);
		}

		if (progress.Length < SceneManager.sceneCountInBuildSettings - 1)
		{
			Level[] tmp = new Level[SceneManager.sceneCountInBuildSettings - 1];
			for (int i=0; i<tmp.Length; i++)
			{
				tmp[i] = i < progress.Length ? progress[i] : new Level(i+1);
			}
			progress = tmp;
			XMLHelper.Save<Level>(ref progress, Level.path);
		}

        for (int i=0; i<SceneManager.sceneCountInBuildSettings - 1; i++)
        {
            LevelEntry entry = Instantiate(EntryPrefab);
            entry.transform.SetParent(LevelPanel);
            entry.Init(progress[i].index, !progress[i].state);
        }
	}

    public void Refresh()
    {

    }
}
