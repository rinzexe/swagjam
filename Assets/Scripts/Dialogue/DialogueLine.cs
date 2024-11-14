using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    public string text;
    [TextArea(3, 10)]
    public string description;
    public float displayDuration = 3f;
    public float typewriterSpeed = 0.05f;
    public bool waitForInput;
    public GameEvent onLineComplete;
    public UnityEvent onLineStart;
    public UnityEvent onLineEnd;
}