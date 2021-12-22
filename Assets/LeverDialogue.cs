using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LeverDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    private StarterAssets.ThirdPersonController tpc;
    private float DialogueTimer;
    private bool DialoguePressed;

    public bool LastLine;

    public GameObject StartButtons;
    public Button YesButton;

    public GameObject DownArrow;

    Minigame minigame;

    // Start is called before the first frame update
    void Start()
    {
        minigame = FindObjectOfType<Minigame>();
        tpc = FindObjectOfType<StarterAssets.ThirdPersonController>();

        textComponent.text = string.Empty;

        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (DialoguePressed)
        {
            DialogueTimer += Time.deltaTime;
            if (DialogueTimer >= 0.25)
            {
                DialoguePressed = false;
                DialogueTimer = 0;
            }
        }

        if (textComponent.text == lines[index] && index != 2)
            DownArrow.SetActive(true);
        if (textComponent.text != lines[index])
            DownArrow.SetActive(false);

        if (tpc._playerInput.actions["DialogueNext"].ReadValue<float>() == 1f && !DialoguePressed)
        {
            if (index == 0)
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
                DialoguePressed = true;
            }

            if (index == 1 && !DialoguePressed)
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
                DialoguePressed = true;
            }

            if (index == 2 && !DialoguePressed)
            {
                if (textComponent.text == lines[index])
                {
                    StartButtons.SetActive(true);
                    YesButton.Select();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                    StartButtons.SetActive(true);
                    YesButton.Select();
                }
                DialoguePressed = true;
                DownArrow.SetActive(false);
            }

            if (index == 3 && !DialoguePressed)
            {
                if (textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
                DialoguePressed = true;
            }

            DialoguePressed = true;
        }
    }

    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
        tpc.isDialogue = true;
        DialoguePressed = true;
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            tpc.isDialogue = false;
            tpc.DialogueDone = true;
            DownArrow.SetActive(false);
        }
    }

    public void YesStart()
    {
        LastLine = true;

        Debug.Log("Minigame Start!");

        if (textComponent.text == lines[index])
        {
            NextLine();
            LastLine = false;
            StartButtons.SetActive(false);
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }

        DialoguePressed = true;
        minigame.StartMiniGame();
    }

    public void NoStart()
    {
        DownArrow.SetActive(false);
        StartButtons.SetActive(false);
        gameObject.SetActive(false);
        tpc.isDialogue = false;
        tpc.DialogueDone = true;

    }
}
