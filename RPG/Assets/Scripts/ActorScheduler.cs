/*#### Basicamente um gerente de açoes de character
 #### toda açao tomada pelo jogador ou IA sempre sera checado
 #### se é a mesma açao anterior ou se é nova
 #### caso seja nova entao ira cancelar a anterior e substitui pela nova*/
using UnityEngine;

/*namespace RPG.Manager
{
    //criar pasta Manager caso nao tenha
}*/

public class ActorScheduler : MonoBehaviour
{
    IAction currentAct;//chama açao atual

    //checa açao
    public void StartAction(IAction action)
    {
        if (currentAct == action) return;//se for a mesma açao sai
        else if(currentAct != null)//caso contrario e tambem nao sendo null 
        {
            currentAct.Cancel();//cancela açao
            Debug.Log("Canceling " + currentAct);
        }
        currentAct = action;//e substitui por achao nova
    }

    public void CancelAction()
    {
        StartAction(null);
    }
}
