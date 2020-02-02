/*####Mosta dados do HUD do jogador*/
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour
{
    [SerializeField] Text hpText;//referencia do texto do hp
    [SerializeField] Text xpText;//referencia do texto do xp
    [SerializeField] Text currentLvL;//referencia do texto do lvl atual

    //referencias
    XP xp;
    Health health;
    CalcStats baseStats;
    
    private void Awake()
    {
        //inicializao de variaveis
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        xp = GameObject.FindGameObjectWithTag("Player").GetComponent<XP>();
        baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CalcStats>();
    }

    //update das informacoes
    private void Update()
    {
        hpText.text = string.Format("{0:0}/{1:0} ({2:0}%)", health.CaptureState(), baseStats.GetStats(Stats.Health), health.GetPercentge());
        xpText.text = string.Format(" {0:0}/{1}", xp._xp, baseStats.GetStats(Stats.LvlUp));
        currentLvL.text = string.Format(" {0:0}", baseStats.CalcLvl());
    }
}
