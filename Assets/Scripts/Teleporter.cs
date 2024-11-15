using UnityEngine;

public class Teleporter : MonoBehaviour
{


    [SerializeField]
    public void TeleportToScene(int scene)
    {
        if (scene == 0)
        {
            GameManager.Instance.firstFloorCamera.gameObject.SetActive(true);
            GameManager.Instance.outsideCamera.gameObject.SetActive(false);
            GameManager.Instance.topFloorCamera.gameObject.SetActive(false);
            PlayerController.Instance.gameObject.transform.position = GameManager.Instance.firstFloorSpawnPos;
        }
         else {
            GameManager.Instance.firstFloorCamera.gameObject.SetActive(false);
            GameManager.Instance.outsideCamera.gameObject.SetActive(false);
            GameManager.Instance.topFloorCamera.gameObject.SetActive(true);
            PlayerController.Instance.gameObject.transform.position = GameManager.Instance.topFloorSpawnPos;
        }
    }

    public void TeleportToFirstFloor()
    {
        TeleportToScene(0);
    }

    public void TeleportToTopFloor()
    {
        TeleportToScene(1);
    }
}
