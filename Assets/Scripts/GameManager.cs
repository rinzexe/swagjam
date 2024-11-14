using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
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
