using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
	public Image dialog;
	public TextMeshProUGUI toast;
	public GameObject buttonGroup;
	public Animator animator;
	public float toastFadeRate = 1;

	private bool hideButtonGroup = false;

	/* Animations - Start playing the game */

	public void StartPlaying()
	{
		if (dialog != null) dialog.gameObject.SetActive(false);
		animator.SetTrigger("StartPlaying");
	}

	private void StartPlayingAnimEnds()
	{
		SceneManager.LoadScene(1);
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

	public void SetHideButtonGroup(bool flag)
	{
		//buttonGroup.SetActive(!flag);
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
}
