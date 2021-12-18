using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    private StarterAssets.ThirdPersonController tpc;
    private float DialogueTimer;
    private bool DialoguePressed;

    public GameObject DownArrow;

    // Start is called before the first frame update
    void Start()
    {
        tpc = FindObjectOfType<StarterAssets.ThirdPersonController>(); 

        textComponent.text = string.Empty;

        if (!SavedData.StartDialogue)
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (SavedData.StartDialogue == true)
        {
            gameObject.SetActive(false);
            tpc.isDialogue = false;
            tpc.DialogueDone = true;
        }

        if (DialoguePressed)
        {
            DialogueTimer += Time.deltaTime;
            if (DialogueTimer >= 0.25)
            {
                DialoguePressed = false;
                DialogueTimer = 0;
            }
        }

        if (textComponent.text == lines[index])
            DownArrow.SetActive(true);
        if (textComponent.text != lines[index])
            DownArrow.SetActive(false);

        if (tpc._playerInput.actions["DialogueNext"].ReadValue<float>() == 1f && !DialoguePressed)
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
    }

    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
        tpc.isDialogue = true;
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
            DownArrow.SetActive(false);
            tpc.isDialogue = false;
            tpc.DialogueDone = true;
            SavedData.StartDialogue = true;
        }
    }
}
