using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEntry : MonoBehaviour
{
    public Sprite img_enabled;
    public Sprite img_disabled;
    public TextMeshProUGUI text;

    private Button btn;
    private int LevelIndex;

    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public void Init(int index, bool locked)
    {
        LevelIndex = index;
        btn.enabled = !locked;
        text.text = LevelIndex.ToString();
        text.gameObject.SetActive(!locked);
        GetComponent<Image>().sprite = locked ? img_disabled : img_enabled;
    }

    private void OnClick()
    {
        SceneManager.LoadScene(LevelIndex);
    }
}
