using UnityEngine;
using UnityEngine.UI;


public class TaskManager : MonoBehaviour
{
    public float windowTimeReward = 100;
    // public float asteroidsTimeReward = 120;
    public float radarTimeReward = 200;
    public float cleaningTimeReward = 60;
    // public float wateringTimeReward = 150;

    public float defaultWindowCooldown = 0;
    // public float defaultAsteroidsCooldown = 200;
    public float defaultRadarCooldown = 300;
    public float defaultCleaningCooldown = 400;
    // public float defaultWaterCooldown = 100;

    public float windowCooldown;
    // public float asteroidsCooldown;
    public float radarCooldown;
    public float cleaningCooldown;
    // public float waterCooldown;

    public GameObject taskStatusPanel;
    public GameObject taskPanel;

    public static TaskManager Instance;

    public int tasksDone = 0;

    public GameObject terminationPaperInteractable;
    public GameObject terminationPaper;

    void Awake()
    {
        taskPanel.SetActive(false);
        // windowCooldown = defaultWindowCooldown;
        // asteroidsCooldown = defaultAsteroidsCooldown;
        // radarCooldown = defaultRadarCooldown;
        // cleaningCooldown = defaultCleaningCooldown;
        // waterCooldown = defaultWaterCooldown;

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

    bool terminated = false;

    public void Update()
    {
        if (tasksDone > 2 && terminated == false)
        {
            terminationPaper.SetActive(true);
            terminationPaperInteractable.SetActive(true);
            terminated = true;
        }

        windowCooldown -= Time.deltaTime;
        // asteroidsCooldown -= Time.deltaTime;
        radarCooldown -= Time.deltaTime;
        cleaningCooldown -= Time.deltaTime;
        // waterCooldown -= Time.deltaTime;


        if (windowCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 0, 0);
        }
        // if (asteroidsCooldown < 0)
        // {
        //     taskStatusPanel.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 0, 0);
        // }
        if (radarCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(2).GetComponent<Image>().color = new Color(255, 0, 0);
        }
        if (cleaningCooldown < 0)
        {
            taskStatusPanel.transform.GetChild(3).GetComponent<Image>().color = new Color(255, 0, 0);
        }
        // if (waterCooldown < 0)
        // {
        //     taskStatusPanel.transform.GetChild(4).GetComponent<Image>().color = new Color(255, 0, 0);
        // }
    }

    public void CompleteWindows()
    {
        tasksDone++;
        windowCooldown = defaultWindowCooldown;
        GameManager.Instance.timeBeforeNextLog -= windowTimeReward;
        taskStatusPanel.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }

    // public void CompleteAsteroids()
    // {
    //     asteroidsCooldown = defaultAsteroidsCooldown;
    //     GameManager.Instance.timeBeforeNextLog -= asteroidsTimeReward;
    //     taskStatusPanel.transform.GetChild(1).GetComponent<Image>().color = new Color(0, 255, 0);
    //     PlayerController.Instance.canMove = true;
    // }

    public void CompleteRadar()
    {
        tasksDone++;
        radarCooldown = defaultRadarCooldown;
        GameManager.Instance.timeBeforeNextLog -= radarTimeReward;
        taskStatusPanel.transform.GetChild(2).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }

    public void CompleteCleaning()
    {
        tasksDone++;
        cleaningCooldown = defaultCleaningCooldown;
        GameManager.Instance.timeBeforeNextLog -= cleaningTimeReward;
        taskStatusPanel.transform.GetChild(3).GetComponent<Image>().color = new Color(0, 255, 0);
        PlayerController.Instance.canMove = true;
    }

    // public void CompleteWater()
    // {
    //     tasksDone++;
    //     waterCooldown = defaultWaterCooldown;
    //     GameManager.Instance.timeBeforeNextLog -= wateringTimeReward;
    //     taskStatusPanel.transform.GetChild(4).GetComponent<Image>().color = new Color(0, 255, 0);
    //     PlayerController.Instance.canMove = true;
    // }
}
