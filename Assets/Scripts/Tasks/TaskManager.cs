using UnityEngine;
using UnityEngine.UI;


public class TaskManager : MonoBehaviour
{
    public float windowTimeReward = 100;
    public float asteroidsTimeReward = 120;
    public float radarTimeReward = 200;
    public float cleaningTimeReward = 60;

    public float defaultWindowCooldown = 0;
    public float defaultAsteroidsCooldown = 200;
    public float defaultRadarCooldown = 300;
    public float defaultCleaningCooldown = 400;

    public float windowCooldown;
    public float asteroidsCooldown;
    public float radarCooldown;
    public float cleaningCooldown;

    public GameObject taskStatusPanel;
    public GameObject taskPanel;

    public static TaskManager Instance;

    void Awake()
    {
        taskPanel.SetActive(false);
        windowCooldown = defaultWindowCooldown;
        asteroidsCooldown = defaultAsteroidsCooldown;
        radarCooldown = defaultRadarCooldown;
        cleaningCooldown = defaultCleaningCooldown;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void Update()
    {
        windowCooldown -= Time.deltaTime;
        asteroidsCooldown -= Time.deltaTime;
        radarCooldown -= Time.deltaTime;
        cleaningCooldown -= Time.deltaTime;

        if (windowCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 0, 0);
        }
        if (asteroidsCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 0, 0);
        }
        if (radarCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(2).GetComponent<Image>().color = new Color(255, 0, 0);
        }
        if (cleaningCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(3).GetComponent<Image>().color = new Color(255, 0, 0);
        }
    }

    public void CompleteWindows()
    {
        windowCooldown = defaultWindowCooldown;
        GameManager.Instance.timeBeforeNextLog -= windowTimeReward;
        taskStatusPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }

    public void CompleteAsteroids()
    {
        asteroidsCooldown = defaultAsteroidsCooldown;
        GameManager.Instance.timeBeforeNextLog -= asteroidsTimeReward;
        taskStatusPanel.transform.GetChild(1).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }

    public void CompleteRadar()
    {
        radarCooldown = defaultRadarCooldown;
        GameManager.Instance.timeBeforeNextLog -= radarTimeReward;
        taskStatusPanel.transform.GetChild(2).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }

    public void CompleteCleaning()
    {
        cleaningCooldown = defaultCleaningCooldown;
        GameManager.Instance.timeBeforeNextLog -= cleaningTimeReward;
        taskStatusPanel.transform.GetChild(3).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }
}
