/*#### Demonstra barra de vida do alvo atualizada*/
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] RectTransform foreground = null;//frente da UI
    [SerializeField] Canvas rootC = null;//canvas

    Health hpComp = null;//referencia da quantia do HP

    //inicializa;áo de variaveis
    private void Awake()
    {
        hpComp = GetComponentInParent<Health>();
    }

    void Update()
    {
        //atualiza a barra
        //se o percentual do HP for aprocimadamente entre 0 e 1
        if (Mathf.Approximately(hpComp.GetFraction(), 1) || Mathf.Approximately(hpComp.GetFraction(), 0))
        {
            if (hpComp.Died())//se tiver morrido destroi a barra
                Destroy(gameObject);
            
            rootC.enabled = false;//desativa a barra
        }
        else//caso contrario
            rootC.enabled = true;//ativa abarra

        foreground.localScale = new Vector3(hpComp.GetFraction(), 1, 1);//atualiza a barra referente ao porcentual do HP do alvo
    }
}
