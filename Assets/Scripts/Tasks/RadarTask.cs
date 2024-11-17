using UnityEngine;
using UnityEngine.UI;
using System;

public class RadarTask : Trigger
{
    public GameObject taskPanel;

    public GameObject inputPanel;
    public GameObject imagePanel;

    public bool taskActive = false;

    Vector2 goal;

    public DialogueSequence tooEarlySequence;

    void Awake()
    {
        taskPanel.SetActive(false);
    }

    public void StartTask()
    {
        if (TaskManager.Instance.radarCooldown < 0)
        {
            goal = new Vector2(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
            PlayerController.Instance.canMove = false;
            taskPanel.SetActive(true);
            taskActive = true;
        }
        else
        {
            DialogueManager.Instance.StartSequence(tooEarlySequence, null);
        }
    }

    void LateUpdate()
    {
        if (taskActive == true)
        {
            RectTransform rectTransform = inputPanel.GetComponent<Image>().rectTransform;
            Vector2 localPoint;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                Input.mousePosition,
                null,
                out localPoint))
            {
                return;
            }

            Vector2 normalizedPoint = Rect.PointToNormalized(
                rectTransform.rect,
                localPoint
            );

            float distance = Vector2.Distance(normalizedPoint, goal);

            imagePanel.GetComponent<Image>().material.SetFloat("_Distance", distance * 10);

            if (distance < 0.005)
            {
                CompleteTask();
            }
        }
    }

    public void CompleteTask()
    {
        taskActive = false;
        taskPanel.SetActive(false);
        TaskManager.Instance.CompleteRadar();
    }
}
