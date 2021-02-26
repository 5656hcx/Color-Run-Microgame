using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
	public GameObject ButtonGroup;
	public Animator TitleAnimator;
	public TextMeshProUGUI Toast;
	public float toastFadeRate;

	private GameObject dialog;
	private bool showDialog = false;
	private bool hideButtonGroup = false;

	public void StartGame() 
	{
	    SceneManager.LoadScene(1);
	}

	public void NotYetImplemented()
	{
		Toast.gameObject.SetActive(true);
		Toast.color = Color.white;
	}

	public void ShowDialog(GameObject obj)
	{
		if (showDialog)
		{
			if (dialog == obj)
			{
				showDialog = false;
				TitleAnimator.SetBool("showDialog", showDialog);
			}
			else
			{
				dialog.SetActive(false);
				dialog = obj;
				dialog.SetActive(true);
			}
		}
		else
		{
			dialog = obj;
			showDialog = true;
			TitleAnimator.SetBool("showDialog", showDialog);
		}
	}

	public void SetHideButtonGroup(bool flag)
	{
		ButtonGroup.SetActive(!flag);
		hideButtonGroup = flag;
	}

	public void OnAnimationEnds()
	{
		dialog.SetActive(showDialog);
		if (hideButtonGroup)
		{
			ButtonGroup.SetActive(!showDialog);
		}
		else
		{
			ButtonGroup.SetActive(true);
		}
		hideButtonGroup = false;
	}

	void Update()
	{
		if (Toast.gameObject.activeSelf)
		{
			Toast.color -= new Color(0, 0, 0, toastFadeRate * Time.deltaTime);
			if (Toast.color.a <= 0) Toast.gameObject.SetActive(false);
		}
	}
}
