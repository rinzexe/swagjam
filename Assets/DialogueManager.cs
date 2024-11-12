using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueData
{
    public string speaker;
    public string text;
}

[System.Serializable]
public class DialogueList
{
    public DialogueData[] dialogues;
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textPanel;
    public TextMeshProUGUI speakerPanel;

    void Start() {
        LoadDialogue();
    }
    void LoadDialogue()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Logs/swag");
        DialogueList dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);

        foreach (DialogueData d in dialogueList.dialogues)
        {
            textPanel.text = d.text;
            speakerPanel.text = d.speaker;
            Debug.Log($"{d.speaker}: {d.text}");
        }
    }
}