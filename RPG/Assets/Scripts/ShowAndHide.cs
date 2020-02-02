/*####administra respawn de itens*/
using System.Collections;
using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    //inicia rotina de retirada ddo objeto
    public void Initiate(GameObject obj, float t)
    {
        StartCoroutine(HideForSeconds(obj, t));
    }

    //ativa e desativa o item
    public void SetThisEnable(GameObject obj, bool set)
    {
        obj.SetActive(set);//ativa e desativa
        if (set)
            StopCoroutine("HideForSeconds");//para rotina
    }

    //rotina
    public IEnumerator HideForSeconds(GameObject obj, float t)
    {
        SetThisEnable(obj, false);//retira
        yield return new WaitForSeconds(t);//delay
        SetThisEnable(obj, true);//retorna
    }
}
