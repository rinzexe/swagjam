using UnityEngine;
using System;

public class CleaningTask : MonoBehaviour
{

    public bool taskActive = false;

    public GameObject trashcan;
    public GameObject books;
    public GameObject socks;

    public void StartTask()
    {
        if (TaskManager.Instance.cleaningCooldown < 0)
        {
            trashcan.SetActive(true);
            books.SetActive(true);
            socks.SetActive(true);
            taskActive = true;
        }
    }

    public void CompleteTask()
    {
        taskActive = false;
        TaskManager.Instance.CompleteCleaning();
    }

    public void Update() {
        if (TaskManager.Instance.cleaningCooldown < 0 && taskActive == false) {
            StartTask();
        }
        else if (taskActive == true) {
            if (trashcan.activeInHierarchy == false && books.activeInHierarchy == false && socks.activeInHierarchy == false) {
                CompleteTask();
            }
        }
    }
}
