using UnityEngine;

public class ClosePanelQuickScript : MonoBehaviour
{
    public GameEvent gameEvent;
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            gameEvent.Raise();
        }
    }
}
