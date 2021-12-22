using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    private StarterAssets.ThirdPersonController tpc;
    private float DialogueTimer;
    private bool DialoguePressed;

    public bool LastLine;

    public GameObject SaveButtons;
    public Button YesButton;

    SaveNPC Save;

    public GameObject DownArrow;

    // Start is called before the first frame update
    void Start()
    {
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

        if (textComponent.text == lines[index] && index != 1)
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
                    SaveButtons.SetActive(true);
                    YesButton.Select();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                    SaveButtons.SetActive(true);
                    YesButton.Select();
                }
                DialoguePressed = true;
                DownArrow.SetActive(false);
            }

            if (index == 2 && !DialoguePressed)
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

    public void YesSave()
    {
        LastLine = true;

        Save = tpc.TheSave.GetComponent<SaveNPC>();
        Save.Saving = true;
        Debug.Log("Game Saved!");

        if (textComponent.text == lines[index])
        {
            NextLine();
            LastLine = false;
            SaveButtons.SetActive(false);
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }

        DialoguePressed = true;
    }

    public void NoSave()
    {
        DownArrow.SetActive(false);
        SaveButtons.SetActive(false);
        gameObject.SetActive(false);
        tpc.isDialogue = false;
        tpc.DialogueDone = true;
        
    }
}
