using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
	public GameObject Dialog;
	public Animator TitleAnimator;
	public TextMeshProUGUI Toast;
	public float toastFadeRate;

	private bool showDialog = false;

	public void StartGame() 
	{
	    SceneManager.LoadScene(1);
	}

	public void NotYetImplemented()
	{
		Toast.gameObject.SetActive(true);
		Toast.color = Color.white;
	}

	public void ShowDialog()
	{
		showDialog = !showDialog;
		TitleAnimator.SetBool("showDialog", showDialog);
	}

	public void OnAnimationEnds()
	{
		Dialog.SetActive(showDialog);
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
