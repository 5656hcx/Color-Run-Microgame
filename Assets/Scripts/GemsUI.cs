using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GemsUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text gemQuantity;
    public static int CurrentGemQuantity;
    public PlayerStatistics localPlayerData;
    void Start()
    {
        localPlayerData = GlobalControl.Instance.savedPlayerData;
        CurrentGemQuantity = localPlayerData.Gems;
    }

    // Update is called once per frame
    void Update()
    {
        gemQuantity.text = CurrentGemQuantity.ToString();
    }
}
