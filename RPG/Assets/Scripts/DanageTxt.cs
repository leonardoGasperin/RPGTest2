/*#### faz com que apareca o valor do dano do golpe na tela*/
using UnityEngine;
using UnityEngine.UI;

public class DanageTxt : MonoBehaviour
{
    [SerializeField] Text text = null;//referencia do texto

    //destroi a UI
    public void DestroyTxt()
    {
        Destroy(gameObject);
    }

    //escreve o valor do dano
    public void SetValue(float amount)
    {
        text.text = string.Format("{0:0}", amount);
    }
}
