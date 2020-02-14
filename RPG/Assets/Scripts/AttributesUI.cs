/*#### Script para comportamento da UI de atributos, onde ira mostrar atualizado os pontos de atributos do jogador
  #### e de alterar valores a cada nivel*/
using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour
{
    //referencia de objetos botões e textos
    [SerializeField] GameObject characterC = null;
    [SerializeField] Button str;
    [SerializeField] Button pro;
    [SerializeField] Button agi;
    [SerializeField] Button dex;
    [SerializeField] Button intt;
    [SerializeField] Button eru;
    [SerializeField] Text strV;
    [SerializeField] Text proV;
    [SerializeField] Text agiV;
    [SerializeField] Text dexV;
    [SerializeField] Text intV;
    [SerializeField] Text eruV;
    [SerializeField] Text pointTxt;
    [SerializeField] Text pointV;

    private void Start()
    {
        //inicialização
        RemainingPoints(0);
    }

    //esta ativando a atualização de valores na UI somente enquanto a UI ativa
    private void Update()
    {
        if (gameObject.activeSelf)
            ShowStatsPoint();
        return;
    }

    //ativa botoes e texto para atribuição de pontos quando tiver pontos sobrando
    public void RemainingPoints(int points)
    {
        if (points > 0)
        {
            str.gameObject.SetActive(true);
            pro.gameObject.SetActive(true);
            agi.gameObject.SetActive(true);
            dex.gameObject.SetActive(true);
            intt.gameObject.SetActive(true);
            eru.gameObject.SetActive(true);
            pointTxt.enabled = true;
            pointV.text = points + "";
            pointV.enabled = true;
        }
        else
        {
            str.gameObject.SetActive(false);
            pro.gameObject.SetActive(false);
            agi.gameObject.SetActive(false);
            dex.gameObject.SetActive(false);
            intt.gameObject.SetActive(false);
            eru.gameObject.SetActive(false);
            pointTxt.enabled = false;
            pointV.enabled = false;
        }
    }
    public void AddPoints(int st)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CalcAtb>().AddStats(st, 1);
    }

    //somente para o botao de fechar
    public void CloseWindow()
    {
        characterC.SetActive(!characterC.activeSelf);
    }

    //mostra os valores atualizados dos atributos
    private void ShowStatsPoint()
    {
        CalcAtb sts = GameObject.FindGameObjectWithTag("Player").GetComponent<CalcAtb>();
        strV.text = string.Format("{0:0}", sts.GetStats(Stats.Str));
        proV.text = string.Format("{0:0}", sts.GetStats(Stats.Pro));
        agiV.text = string.Format("{0:0}", sts.GetStats(Stats.Agi));
        dexV.text = string.Format("{0:0}", sts.GetStats(Stats.Dex));
        intV.text = string.Format("{0:0}", sts.GetStats(Stats.Int));
        eruV.text = string.Format("{0:0}", sts.GetStats(Stats.Eru));
    }
}
