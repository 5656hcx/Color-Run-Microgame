using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EscapeMode { POSITION, INPUT }

public class TutorialController_v2 : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public BoxCollider2D collider2d;
    public Canvas canvas;

    public EscapeMode escapeMode;
    public string text = "Tip";
    public Vector2 effectArea = Vector2.one;
    public Vector2 textOffset = Vector2.zero;

    public KeyCode[] inputButtonCodes;
    public string[] inputButtonNames;

    public bool isPersistent = false;
    public bool isAnyKeyMode = false;
    public bool isAutoHidden = false;

    private bool insideArea;
    private int totalActionCount;
    private HashSet<KeyCode> codes;
    private HashSet<string> names;

    void Start()
    {
        insideArea = false;
        canvas.gameObject.SetActive(false);
        codes = new HashSet<KeyCode>();
        names = new HashSet<string>();

        if (isAnyKeyMode)
        {
            totalActionCount = 1;
        }
        else
        {
            totalActionCount = inputButtonNames.Length;
            totalActionCount += inputButtonCodes.Length;
        }
    }

    void OnValidate()
    {
        textMesh.text = text;
        collider2d.size = effectArea;
        canvas.transform.localPosition = textOffset;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            insideArea = true;
            canvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            insideArea = false;
            if (!isPersistent)
            {
                if (escapeMode == EscapeMode.POSITION)
                {
                    gameObject.SetActive(false);
                }
                else if (isAutoHidden)
                {
                    canvas.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        if (!isPersistent && insideArea && escapeMode == EscapeMode.INPUT)
        {
            foreach (string button in inputButtonNames)
            {
                if (Input.GetButtonDown(button))
                {
                    names.Add(button);
                }
            }

            foreach (KeyCode key in inputButtonCodes)
            {
                if (Input.GetKeyDown(key))
                {
                    codes.Add(key);
                }
            }

            if (names.Count + codes.Count >= totalActionCount)
            {
                
                gameObject.SetActive(false);
                //canvas.gameObject.SetActive(false); // disable the text
            }
        }
    }
}
