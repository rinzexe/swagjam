using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private TriggerType triggerType;

    public enum TriggerType
    {
        OnTrigger,
        OnInteract
    }

    public void TriggerEvent()
    {
        if (gameEvent != null)
        {
            gameEvent.Raise();
        }
    }

    void Update()
    {
        if (triggerType == TriggerType.OnInteract && DialogueManager.Instance.interationPanel.activeInHierarchy == true)
        {
            DialogueManager.Instance.interationPanel.SetActive(false);
            gameEvent.Raise();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (triggerType == TriggerType.OnInteract && collider.CompareTag("Player"))
            DialogueManager.Instance.interationPanel.SetActive(true);

        if (triggerType == TriggerType.OnTrigger && collider.CompareTag("Player"))
            gameEvent.Raise();
    }
}