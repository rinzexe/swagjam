using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Vector3 firstFloorSpawnPos;
    public Vector3 topFloorSpawnPos;

    public GameObject firstFloorSpawnObject;
    public GameObject topFloorSpawnObject;

    public Camera outsideCamera;
    public Camera firstFloorCamera;
    public Camera topFloorCamera;

    public float timeBeforeNextLog = 10000000000;

    int currentLog = 0;

    public DialogueSequence[] logs;

    private void Awake()
    {
        firstFloorSpawnPos = firstFloorSpawnObject.transform.position;
        topFloorSpawnPos = topFloorSpawnObject.transform.position;
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
        if (timeBeforeNextLog < 0)
        {
            timeBeforeNextLog = 10;
            DialogueManager.Instance.StartSequence(logs[currentLog], null);
        }
        timeBeforeNextLog -= Time.deltaTime;
    }
}
