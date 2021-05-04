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

	public Image title;
	private Sprite newTitle;
	private Sprite rawTitle;

	private Image dialog;
	private bool hideButtonGroup = false;

	private SettingManager sm;

	public Toggle toggleMusic;
	public Toggle toggleTips;

	void Start()
	{
		newTitle = title.sprite;
		rawTitle = title.sprite;
		LoadSetting();
		InitToggles();
		InitLevelEntries();
	}

	/* Animations - Start playing the game */

	public void StartPlaying(int index)
	{
		if (dialog != null) dialog.gameObject.SetActive(false);
		animator.SetInteger("targetLevel", index);
		animator.SetTrigger("StartPlaying");
		buttonGroup.SetActive(true);
		title.sprite = rawTitle;
	}

	private void StartPlayingAnimEnds()
	{
		SceneManager.LoadScene(animator.GetInteger("targetLevel"));
	}

	/* Animations - Show dialogs if any */

	public void ShowDialog(Image dialog)
	{
		if (this.dialog == null) this.dialog = dialog;
		if (this.dialog == dialog)
		{
			if (animator.GetBool("ShowDialog"))
			{
				this.dialog.gameObject.SetActive(false);
			}
			animator.SetBool("ShowDialog", !animator.GetBool("ShowDialog"));
		}
		else
		{
			if (animator.GetBool("ShowDialog"))
			{
				buttonGroup.SetActive(!hideButtonGroup);
				this.dialog.gameObject.SetActive(false);
				this.dialog = dialog;
				this.dialog.gameObject.SetActive(true);
				ChangeTitle();
			}
			else
			{
				this.dialog = dialog;
				animator.SetBool("ShowDialog", true);
			}
		}
	}

	private void ShowDialogAnimEnds()
	{
		dialog.gameObject.SetActive(animator.GetBool("ShowDialog"));
		buttonGroup.SetActive(dialog.gameObject.activeSelf ? !hideButtonGroup : true);
	}

	// Ugly solution
	public void SetHideButtonGroup(bool flag)
	{
		hideButtonGroup = flag;
	}

	/* Animations - Show debug message */

	public void SetupToast(string text, float yOffset)
	{
		toast.text = text;
		toast.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yOffset);
		toast.color = Color.white;
		toast.gameObject.SetActive(true);
	}

	public void NotYetImplemented()
	{
		SetupToast("NOT_YET_IMPLEMENTED", 80);
	}

	void Update()
	{
		if (toast.gameObject.activeSelf)
		{
			toast.color -= new Color(0, 0, 0, toastFadeRate * Time.deltaTime);
			if (toast.color.a <= 0)
			{
				toast.gameObject.SetActive(false);
				toast.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 80);
			}
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
			entry.transform.SetParent(LevelPanel);
			entry.transform.localScale = new Vector3(1, 1, 0);
			entry.Init(progress[i].index, !progress[i].state);
			entry.clicked = new LevelEntry.OnClick(StartPlaying);
		}
	}

	public void SetTitle(Sprite sprite)
	{
		newTitle = sprite;
	}

	private void ChangeTitle() {
		title.sprite = animator.GetBool("ShowDialog") ? newTitle : rawTitle;
	}

	// May need to separate purchase from UI classes
	// 
	public void Purchase(int price)
	{
		if(price <= GemsUI.CurrentGemQuantity)
		{
			GemsUI.CurrentGemQuantity -= price;
			// Life++
			GlobalControl.Instance.savedPlayerData.Gems = GemsUI.CurrentGemQuantity;
		}
		else
		{
			SetupToast("No enough GEMS!", 140);
		}
	}

	public void LoadSetting()
	{
		if (sm == null)
		{
			sm = SettingManager.GetInstance();
			sm.ApplySetting();
		}
		toggleMusic.isOn = sm.music;
		toggleTips.isOn = sm.tips;
	}

	private void InitToggles()
	{
		toggleMusic.onValueChanged.AddListener((bool value) => {
			sm.music = value;
			sm.ApplySetting();
		});
		/* temporarily disabled
		toggleTips.onValueChanged.AddListener((bool value) => {
			sm.tips = value;
			sm.ApplySetting();
		});
		*/
	}
}
