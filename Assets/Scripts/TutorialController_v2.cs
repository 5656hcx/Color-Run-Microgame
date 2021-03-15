using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EscapeMode { POSITION, INPUT }

public abstract class TutorialStateReceiver : MonoBehaviour
{
    public virtual void OnAnotherTutorialComplete() {}
}

public class TutorialController_v2 : TutorialStateReceiver
{
    public TextMeshProUGUI textMesh;
    public BoxCollider2D collider2d;
    public Canvas canvas;

    public EscapeMode escapeMode;
    public string text = "Tip";
    public Vector2 effectArea = Vector2.one;
    public Vector2 textOffset = Vector2.zero;

    public TutorialStateReceiver[] receivers;
    public KeyCode[] inputButtonCodes;
    public string[] inputButtonNames;

    public bool isPersistent = false;
    public bool isAnyKeyMode = false;
    public bool isAutoHidden = false;

    public bool showCompleteText = false;
    public string completeText = "Gotcha!";

    private bool isPlayerInside;
    private int totalActionCount;
    private HashSet<KeyCode> codes;
    private HashSet<string> names;
    private Animator animator;

    void Start()
    {
        isPlayerInside = false;
        canvas.gameObject.SetActive(false);
        codes = new HashSet<KeyCode>();
        names = new HashSet<string>();
        animator = GetComponent<Animator>();

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
            isPlayerInside = true;
            canvas.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayerInside = false;
            if (!isPersistent)
            {
                if (escapeMode == EscapeMode.POSITION)
                {
                    OnTutorialComplete();
                }
                else if (isAutoHidden && !animator.GetBool("TutorialComplete"))
                {
                    canvas.gameObject.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        if (!isPersistent && isPlayerInside && escapeMode == EscapeMode.INPUT)
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
                OnTutorialComplete();
                // canvas.gameObject.SetActive(false); // disable the text
            }
        }
    }

    private void OnTutorialComplete()
    {
        if (showCompleteText)
        {
            textMesh.text = completeText;
        }
        foreach (TutorialStateReceiver rec in receivers)
        {
            rec.OnAnotherTutorialComplete();
        }
        animator.SetBool("TutorialComplete", true);
    }

    private void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    public override void OnAnotherTutorialComplete()
    {
        OnTutorialComplete();
    }
}
