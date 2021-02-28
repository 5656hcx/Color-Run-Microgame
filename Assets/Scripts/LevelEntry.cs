using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelEntry : MonoBehaviour
{
    public Sprite img_enabled;
    public Sprite img_disabled;
    public TextMeshProUGUI text;
    public Button btn;

    public delegate void OnClick(int index);
    public OnClick clicked;

    public void Init(int index, bool locked)
    {
        btn.enabled = !locked;
        btn.onClick.AddListener(delegate{clicked.Invoke(index);});
        text.text = index.ToString();
        text.gameObject.SetActive(!locked);
        GetComponent<Image>().sprite = locked ? img_disabled : img_enabled;
    }
}
