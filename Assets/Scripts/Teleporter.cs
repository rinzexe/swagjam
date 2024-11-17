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
         else if (scene == 1) {
            GameManager.Instance.firstFloorCamera.gameObject.SetActive(false);
            GameManager.Instance.outsideCamera.gameObject.SetActive(false);
            GameManager.Instance.topFloorCamera.gameObject.SetActive(true);
            PlayerController.Instance.gameObject.transform.position = GameManager.Instance.topFloorSpawnPos;
        }
                 else {
            GameManager.Instance.firstFloorCamera.gameObject.SetActive(false);
            GameManager.Instance.outsideCamera.gameObject.SetActive(true);
            GameManager.Instance.topFloorCamera.gameObject.SetActive(false);
            PlayerController.Instance.gameObject.transform.position = GameManager.Instance.outsideSpawnPos;
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

        public void TeleportToOutside()
    {
        TeleportToScene(3);
    }
}
