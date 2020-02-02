/*#### faz as UI's do jogo ficaram retas a camera*/
using UnityEngine;

public class CameraFacingUI : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
