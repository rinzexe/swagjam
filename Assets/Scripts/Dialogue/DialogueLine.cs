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
        [TextArea(3, 10)]
    public string text;
    public float typewriterSpeed = 0.05f;
    public bool waitForInput;
    public GameEvent onLineComplete;
    public UnityEvent onLineStart;
    public UnityEvent onLineEnd;
}