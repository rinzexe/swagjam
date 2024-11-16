using UnityEngine;

public class TaskListScreen : MonoBehaviour
{
    public GameObject taskListPanel;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            taskListPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            taskListPanel.SetActive(false);
        }
    }
}
