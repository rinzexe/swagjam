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

    int progress = 0;

    void Update()
    {

    }
}
