using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemsUI : MonoBehaviour
{
    public static int CurrentGemQuantity;
    public PlayerStatistics localPlayerData;

    private TextMeshProUGUI gemQuantity;

    void Start()
    {
        gemQuantity = GetComponent<TextMeshProUGUI>();
        localPlayerData = GlobalControl.Instance.savedPlayerData;
        CurrentGemQuantity = localPlayerData.Gems;
    }

    void Update()
    {
        gemQuantity.text = CurrentGemQuantity.ToString();
    }
}
