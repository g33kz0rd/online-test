using UnityEngine;

public class PlayerLookDirectionController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCameraContainer;

    [SerializeField]
    private GameObject playerModel;


    void Update()
    {
        playerModel.transform.localRotation = Quaternion.Lerp(playerModel.transform.localRotation, Quaternion.Euler(0, playerCameraContainer.transform.localEulerAngles.y, 0), 0.3f);
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(10, 10, 100, 30), playerCameraContainer.transform.localEulerAngles.ToString());
    }
}
