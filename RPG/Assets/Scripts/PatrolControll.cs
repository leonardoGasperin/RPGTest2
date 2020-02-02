/*#### controle de patrulha da ia*/
using UnityEngine;

/*namespace RPG.Manager
{
    //criar pasta Manager caso nao tenha
}*/

public class PatrolControll : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        const float radius = 0.3f;//raio
        for (int i = 0; i < transform.childCount; i++)
        {//para cada ponto de patrulha desenhe uma esfera barnca
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(GetWP(i), radius);
            if (i + 1 >= transform.childCount)
            {//e de ponto a ponto ligue uma linha amarela
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(GetWP(i), GetWP(0));
                break;
            }
            //liga uma linha do ponto finao ao ponto inicial
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(GetWP(i), GetWP(i + 1));
        }
    }
    //pega a pocisao do procimo ponto
    public Vector3 GetWP(int i)
    {
        return transform.GetChild(i).position;
    }
}
