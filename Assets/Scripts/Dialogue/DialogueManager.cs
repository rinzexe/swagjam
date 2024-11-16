using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Settings")]
    [SerializeField] private float defaultTypewriterSpeed = 0.05f;
    [SerializeField] private KeyCode advanceKey = KeyCode.Space;

    private Queue<DialogueLine> currentLines = new Queue<DialogueLine>();
    private DialogueLine currentLine = null;
    private bool isDisplayingLine = false;
    private bool isTyping = false;
    private Coroutine typewriterCoroutine = null;
    private GameEvent onDialogueComplete;

    [SerializeField] public GameObject interationPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        interationPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(advanceKey) || Input.GetMouseButtonDown(0))
        {
            if (isDisplayingLine && currentLine != null)
            {
                Debug.Log("sex");
                if (isTyping)
                {
                    if (typewriterCoroutine != null)
                    {
                        StopCoroutine(typewriterCoroutine);
                    }
                    dialogueText.text = currentLine.text;
                    isTyping = false;
                }
                else
                {
                    AdvanceToNextLine();
                }
            }
        }
    }

    public void StartSequence(DialogueSequence sequence, GameEvent onDialogueComplete)
    {
        this.onDialogueComplete = onDialogueComplete;
        PlayerController.Instance.canMove = false;
        currentLines.Clear();
        currentLine = null;
        foreach (var line in sequence.lines)
        {
            currentLines.Enqueue(line);
        }

        dialoguePanel.SetActive(true);
        DisplayNextLine();
    }

    public void QueueSequence(DialogueSequence sequence)
    {
        foreach (var line in sequence.lines)
        {
            currentLines.Enqueue(line);
        }

        if (!isDisplayingLine)
        {
            dialoguePanel.SetActive(true);
            DisplayNextLine();
        }
    }

    private void DisplayNextLine()
    {
        Debug.Log("nextline");
        if (currentLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        isDisplayingLine = true;
        currentLine = currentLines.Dequeue();

        currentLine.onLineStart?.Invoke();

        characterNameText.text = currentLine.characterName;

        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }
        typewriterCoroutine = StartCoroutine(TypewriterEffect(currentLine));
    }

    private void AdvanceToNextLine()
    {
        currentLine.onLineEnd?.Invoke();
        currentLine.onLineComplete?.Raise();

        DisplayNextLine();
    }

    private IEnumerator TypewriterEffect(DialogueLine line)
    {
        isTyping = true;
        dialogueText.text = "";
        float typeSpeed = line.typewriterSpeed > 0 ? line.typewriterSpeed : defaultTypewriterSpeed;

        foreach (char c in line.text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;

            yield return new WaitForSeconds(line.displayDuration);
            AdvanceToNextLine();
    }

    private void EndDialogue()
    {
        Debug.Log("end");
        if (onDialogueComplete)
        {
            onDialogueComplete.Raise();
        }
        PlayerController.Instance.canMove = true;
        isDisplayingLine = false;
        currentLine = null;
        dialoguePanel.SetActive(false);
    }

    public void ShowText(string text, float duration = 3f)
    {
        DialogueLine tempLine = new DialogueLine
        {
            text = text,
            displayDuration = duration,
        };
        currentLines.Enqueue(tempLine);

        if (!isDisplayingLine)
        {
            dialoguePanel.SetActive(true);
            DisplayNextLine();
        }
    }
}
