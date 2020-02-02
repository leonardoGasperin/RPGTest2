/*#### cria fader da tela para momentos especificos como loads e transicao de mapas*/
using System.Collections;
using UnityEngine;

/*namespace RPG.Manager
{
    //criar pasta Manager caso nao tenha
}*/

public class Fader : MonoBehaviour
{
    CanvasGroup cnvGroup;//chama o canvas
    Coroutine activeFade = null;

    private void Awake()
    {
        //inicializa variaveis
        cnvGroup = GetComponent<CanvasGroup>();
    }

    //da fade imediato
    public void FadeImediatamente()
    {
        cnvGroup.alpha = 1;
    }

    //entra no fade
    public Coroutine FadeOut(float timer)
    {
        return Fade(1, timer);
    }

    //sai do fade
    public Coroutine FadeIn(float timer)
    {
        return Fade(0, timer);
    }

    //inicia coroutine de fade
    public Coroutine Fade(float target, float timer)
    {
        if (activeFade != null)//se tiver algum fader ocorrendo para
            StopCoroutine(activeFade);
        activeFade = StartCoroutine(FadeRoutine(target, timer));
        return activeFade;
    }

    //faz fade
    public IEnumerator FadeRoutine(float target, float timer)
    {
        while (!Mathf.Approximately(cnvGroup.alpha, target))
        {
            cnvGroup.alpha = Mathf.MoveTowards(cnvGroup.alpha, target, Time.deltaTime / timer);
            yield return null;
        }
    }
}
