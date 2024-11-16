using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType;

    bool isActive = false;

    public enum TriggerType
    {
        OnTrigger,
        OnInteract
    }

    [SerializeField] private GameEvent gameEvent;

    void Update()
    {
        if (triggerType == TriggerType.OnInteract && Input.GetKeyDown(KeyCode.E) && isActive)
        {
            isActive = false;
            DialogueManager.Instance.interationPanel.SetActive(false);
            gameEvent.Raise();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (triggerType == TriggerType.OnInteract && collider.CompareTag("Player"))
        {
            isActive = true;
            DialogueManager.Instance.interationPanel.SetActive(true);
        }

        if (triggerType == TriggerType.OnTrigger && collider.CompareTag("Player"))
            gameEvent.Raise();
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (triggerType == TriggerType.OnInteract && collider.CompareTag("Player"))
        {
            isActive = false;
            DialogueManager.Instance.interationPanel.SetActive(false);
        }
    }
}