using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Vector3 firstFloorSpawnPos;
    public Vector3 topFloorSpawnPos;
    public Vector3 outsideSpawnPos;

    public GameObject firstFloorSpawnObject;
    public GameObject topFloorSpawnObject;

    public GameObject outsideSpawnObject;

    public Camera outsideCamera;
    public Camera firstFloorCamera;
    public Camera topFloorCamera;

    public float defaultTimeBeforeNextLog;
    public float timeBeforeNextLog = 5;

    int currentLog = 0;

    public CleaningTask cleaningTask;
    public RadarTask radarTask;
    public WindowTask windowTask;

    public DialogueSequence[] logs;

    private void Awake()
    {
        firstFloorSpawnPos = firstFloorSpawnObject.transform.position;
        topFloorSpawnPos = topFloorSpawnObject.transform.position;
        outsideSpawnPos = outsideSpawnObject.transform.position;
        // Singleton setup - ensures only one instance exists
        if (Instance == null)
        {
            Instance = this; // Set the static Instance to this object
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (timeBeforeNextLog < 0 &&
                radarTask.taskActive == false &&
                cleaningTask.taskActive == false &&
                windowTask.taskActive == false)
        {
            timeBeforeNextLog = defaultTimeBeforeNextLog;
            DialogueManager.Instance.StartSequence(logs[currentLog], null);
            currentLog++;
        }
        timeBeforeNextLog -= Time.deltaTime;
    }
}
