using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] public GameEvent gameEvent;
    [SerializeField] private UnityEvent response;
    
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
    
    public void OnEventRaised()
    {
        response.Invoke();
    }
}