/*#### Script para controle da camera com o mouse
  #### contendo as opções de orbitação da camera e zoom in/out*/
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //referencias
    [SerializeField] Transform tr = null;
    [SerializeField] CinemachineVirtualCamera cam = null;

    //rotaciona as coordenadas x e y referente aos valores recebidos
    public void Orbitation(float y, float x)
    {
        tr.eulerAngles += new Vector3(tr.rotation.x * x * -10, tr.rotation.y * y, 0);
    }

    //da zoom in/out dentro dos limites minimo e maximo
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

    //segue jogador quando orbitation nao esta sendo ativada
    public void LookAtPlayer(bool isMov, bool isClk, Transform target)
    {
        //se tiver se movendo e nao orbitando
        if (isMov && !isClk)
        {
            var lookPos = target.position - transform.position;//para onde olhar
            lookPos.y = 0;//nao mover em y
            var rotation = Quaternion.LookRotation(new Vector3(lookPos.x, -2, lookPos.z));//como deve rotacionar
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);//rotaciona para personagem de forma suave
        }
            
    }
}
