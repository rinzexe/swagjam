using UnityEngine;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Dialogue/Sequence")]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueLine> lines = new List<DialogueLine>();
}