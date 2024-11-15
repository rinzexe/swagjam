using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public enum Scene
    {
        FirstFloor,
        TopFloor
    }


    [SerializeField]
    public void TeleportToScene(Scene scene)
    {
        switch (scene)
        {
            case Scene.FirstFloor:
                GameManager.Instance.firstFloorCamera.gameObject.SetActive(true);
                GameManager.Instance.outsideCamera.gameObject.SetActive(false);
                GameManager.Instance.topFloorCamera.gameObject.SetActive(false);
                PlayerController.Instance.gameObject.transform.position = GameManager.Instance.firstFloorSpawnPos;
                break;
            case Scene.TopFloor:
                GameManager.Instance.firstFloorCamera.gameObject.SetActive(false);
                GameManager.Instance.outsideCamera.gameObject.SetActive(false);
                GameManager.Instance.topFloorCamera.gameObject.SetActive(true);
                PlayerController.Instance.gameObject.transform.position = GameManager.Instance.topFloorSpawnPos;
                break;
        }
    }

    public void TeleportToFirstFloor()
    {
        TeleportToScene(Scene.FirstFloor);
    }

    public void TeleportToTopFloor()
    {
        TeleportToScene(Scene.TopFloor);
    }
}
