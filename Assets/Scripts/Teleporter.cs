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
<<<<<<< HEAD
         else if (scene == 1) {
=======
        else if (scene == 1)
        {
>>>>>>> dfd3ecfd1e9f98e6b2ab950029bee60d7e816ad6
            GameManager.Instance.firstFloorCamera.gameObject.SetActive(false);
            GameManager.Instance.outsideCamera.gameObject.SetActive(false);
            GameManager.Instance.topFloorCamera.gameObject.SetActive(true);
            PlayerController.Instance.gameObject.transform.position = GameManager.Instance.topFloorSpawnPos;
        }
<<<<<<< HEAD
                 else {
=======
        else
        {
>>>>>>> dfd3ecfd1e9f98e6b2ab950029bee60d7e816ad6
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

<<<<<<< HEAD
        public void TeleportToOutside()
    {
        TeleportToScene(3);
=======
    public void TeleportToTheOutside()
    {
        TeleportToScene(2);
>>>>>>> dfd3ecfd1e9f98e6b2ab950029bee60d7e816ad6
    }
}
