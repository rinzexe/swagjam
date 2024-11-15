using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSequence dialogueSequence;
    [SerializeField] private TriggerType triggerType;

    public enum TriggerType
    {
        OnStart,
        OnTrigger,
        OnInteract
    }
    [SerializeField] private GameEvent onDialogueComplete;

    private void Start()
    {
        if (triggerType == TriggerType.OnStart)
            TriggerDialogue();
    }

    void Update()
    {
        if (triggerType == TriggerType.OnInteract && DialogueManager.Instance.interationPanel.activeInHierarchy == true)
        {
            DialogueManager.Instance.interationPanel.SetActive(false);
            TriggerDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (triggerType == TriggerType.OnInteract && collider.CompareTag("Player"))
            DialogueManager.Instance.interationPanel.SetActive(true);

        if (triggerType == TriggerType.OnTrigger && collider.CompareTag("Player"))
            TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartSequence(dialogueSequence, onDialogueComplete);
    }
}