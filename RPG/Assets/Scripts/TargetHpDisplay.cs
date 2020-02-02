/*####Classe para mostrar na tela UI com informaçoes de vida do alvo*/
using UnityEngine;
using UnityEngine.UI;

public class TargetHpDisplay : MonoBehaviour
{
    //referencias e variaveis
    [SerializeField] Text hpLabel;
    [SerializeField] Text hpText;
    [SerializeField] Image back;

    Combat health;

    private void Awake()
    {
        //inicializacao de variaveis
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();//encontra o alvo do player
    }

    private void Update()
    {
        //se nao ouver alvo
        if (health.GetTarget() == null)
        {//esconde UI
            back.enabled = false;
            hpText.enabled = false;
            hpLabel.enabled = false;
            return;
        }
        //caso contrario mostra e atualiza vida do alvo
        back.enabled = true;
        hpText.enabled = true;
        hpLabel.enabled = true;
        hpText.text = string.Format("{0:0}/{1:0}%", health.GetTarget().CaptureState(), health.GetTarget().GetPercentge());
    }
}
