using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    public void TriggerEvent()
    {
        if (gameEvent != null)
        {
            gameEvent.Raise();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (string.IsNullOrEmpty("Player") || collider.gameObject.CompareTag("Player"))
            TriggerEvent();
    }
}