using UnityEngine;

public class TaskScreen : MonoBehaviour
{
    public GameObject taskScreenPanel;
    void Start() {
        taskScreenPanel.SetActive(false);
    }
    public void OpenTaskScreen() {
        taskScreenPanel.SetActive(true);
    }
}
