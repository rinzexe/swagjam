using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueSequence dialogueSequence;
    [SerializeField] private bool triggerOnStart;
    [SerializeField] private bool triggerOnInteract;
    [SerializeField] private GameEvent onDialogueComplete;

    private void Start()
    {
        if (triggerOnStart)
            TriggerDialogue();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (triggerOnInteract && collider.CompareTag("Player"))
            TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        Debug.Log("Triggerd");
        DialogueManager.Instance.StartSequence(dialogueSequence);
    }
}