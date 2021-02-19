using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
	public TextMeshProUGUI Toast;
	public float toastFadeRate;

	public void StartGame() 
	{
	    SceneManager.LoadScene("Level_1");
	}

	public void NotYetImplemented()
	{
		Toast.gameObject.SetActive(true);
		Toast.color = Color.white;
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
