using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinPicker : MonoBehaviour
{
    public Button next, prev;
    public Button unlock, equip;
    public TMP_Text unlockText, equipText;
    public Image showcase;

    public UIController uic;

    private int count, selected, equipped;
    private Sprite[] skins;
    private Skinner[] skinners;

    private SkinHelper sh;

    void OnApplicationQuit()
    {
        sh.SaveSkinners();
    }

    void OnDestroy()
    {
        sh.SaveSkinners();
    }

    void Start()
    {
        sh = SkinHelper.GetInstance();
        skinners = sh.GetSkinners();
        skins = sh.GetSkins();
        count = skins.Length;
        Locate();
        Refresh();

        next.onClick.AddListener(() => {
            selected = (selected + 1) % count;
            Refresh();
        });

        prev.onClick.AddListener(() => {
            selected = (selected == 0 ? count : selected) - 1;
            Refresh();
        });

        unlock.onClick.AddListener(() => {
            if (uic.Purchase(skinners[selected].price))
            {
                skinners[selected].unlock = true;
                unlockText.text = "Unlocked";
                unlock.interactable = false;
                equipText.text = "Equip";
                equip.interactable = true;
            }
        });

        equip.onClick.AddListener(() => {
            skinners[equipped].equip = false;
            skinners[selected].equip = true;
            equipped = selected;
            equipText.text = "Equipped";
            equip.interactable = false;
        });
    }

    private void Locate()
    {
        if (skinners == null || skinners.Length < 2)
        {
            equipped = 0;
        }

        for (int i=0; i<skinners.Length; i++)
        {
            if (skinners[i].equip)
            {
                equipped = i;
                break;
            }
        }
        selected = equipped;
    }

    private void Refresh()
    {
        showcase.sprite = skins[selected];
        if (skinners[selected].unlock)
        {
            unlock.interactable = false;
            unlockText.text = "Unlocked";
        }
        else
        {
            unlock.interactable = true;
            unlockText.text = "" + skinners[selected].price + " GEM";
        }

        if (skinners[selected].equip)
        {
            equipText.text = "Equipped";
            equip.interactable = false;
        }
        else
        {
            equipText.text = "Equip";
            if (skinners[selected].unlock)
            {
                equip.interactable = true;
            }
            else equip.interactable = false;
        }
    }
}