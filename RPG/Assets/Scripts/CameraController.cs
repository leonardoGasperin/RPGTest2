using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform tr = null;
    [SerializeField] CinemachineVirtualCamera cam = null;

    public void Orbitation(float y, float x)
    {
        tr.eulerAngles += new Vector3(tr.rotation.x * x * -10, tr.rotation.y * y, 0);
    }

    public void Zoom(float zoom)
    {
        float zoomMin = 4, zoomMax = 13;
        float camZoom = cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
        if (camZoom >= zoomMin && camZoom <= zoomMax)
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance += zoom;
        if (camZoom < zoomMin)
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = zoomMin;
        if (camZoom > zoomMax)
            cam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = zoomMax;
    }

    public void LookAtPlayer(bool isMov, bool isClk, Transform target)
    {
        
        if (isMov && !isClk)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(new Vector3(lookPos.x, -2, lookPos.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
        }
            
    }
}
