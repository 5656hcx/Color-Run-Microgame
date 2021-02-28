using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using datatype;

public class UIController : MonoBehaviour
{
	public TextMeshProUGUI toast;
	public GameObject buttonGroup;
	public Animator animator;
	public float toastFadeRate = 1;

	public Transform LevelPanel;
	public LevelEntry EntryPrefab;

	private Image dialog;
	private bool hideButtonGroup = false;

	void Start()
	{
		InitLevelEntries();
	}

	/* Animations - Start playing the game */

	public void StartPlaying(int index)
	{
		if (dialog != null) dialog.gameObject.SetActive(false);
		animator.SetInteger("targetLevel", index);
		animator.SetTrigger("StartPlaying");
		buttonGroup.SetActive(true);
	}

	private void StartPlayingAnimEnds()
	{
		SceneManager.LoadScene(animator.GetInteger("targetLevel"));
	}

	/* Animations - Show dialogs if any */

	public void ShowDialog(Image dialog)
	{
		if (this.dialog == null)
		{
			this.dialog = dialog;
			animator.SetBool("ShowDialog", true);
		}
		else if (this.dialog != dialog)
		{
			buttonGroup.SetActive(hideButtonGroup);
			this.dialog.gameObject.SetActive(false);
			this.dialog = dialog;
			this.dialog.gameObject.SetActive(true);
		}
		else animator.SetBool("ShowDialog", false);
	}

	private void ShowDialogAnimEnds()
	{
		dialog.gameObject.SetActive(!dialog.gameObject.activeSelf);
		if (!dialog.gameObject.activeSelf)
		{
			dialog = null;
			buttonGroup.SetActive(true);
		}
		else buttonGroup.SetActive(!hideButtonGroup);
	}

	// Ugly solution
	public void SetHideButtonGroup(bool flag)
	{
		hideButtonGroup = flag;
	}

	/* Animations - Show debug message */

	public void NotYetImplemented()
	{
		toast.gameObject.SetActive(true);
		toast.color = Color.white;
	}

	void Update()
	{
		if (toast.gameObject.activeSelf)
		{
			toast.color -= new Color(0, 0, 0, toastFadeRate * Time.deltaTime);
			if (toast.color.a <= 0) toast.gameObject.SetActive(false);
		}
	}

	private void InitLevelEntries()
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
			entry.clicked = new LevelEntry.OnClick(StartPlaying);
			entry.Init(progress[i].index, !progress[i].state);
			entry.transform.localScale = new Vector3(1, 1, 0);
			entry.transform.SetParent(LevelPanel);
		}
	}
}
