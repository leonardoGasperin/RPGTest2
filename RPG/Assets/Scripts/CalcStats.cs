/*####Adminsitra estatos dos personagens*/
using GameDevTV.Utils;
using System;
using UnityEngine;

public class CalcStats : MonoBehaviour
{
    //referencias e variaveis
    [Range(1, 99)][SerializeField] int startLevel = 1;//nivel inicial
    [SerializeField] CharacterClass characterClass;//classes
    [SerializeField] Progression progression = null;//referenciado arquivo de progrecao
    [SerializeField] GameObject lvlUpParticlesFx;//efeito de evento de evoluir
    [SerializeField] bool useMods = false;//uso de modificadores
    public event Action hpOnLvlUp;//evento de evolucao
    int atbPoints = 0;
    public float[] atb;

    LazyValue<int> currentLvl;//lvl atual

    XP exp;//experiancia

    private void Awake()
    {
        //inicializacao de variaveis
        exp = GetComponent<XP>();
        currentLvl = new LazyValue<int>(CalcLvl);
    }

    private void Start()
    {
        //inciializacao de funcoes
        currentLvl.ForceInit();
        //GameObject.FindGameObjectWithTag("UIPanel").GetComponent<UIController>().RemainingPoints(atbPoints);
    }

    //adicionador de eventos
    private void OnEnable()
    {
        if (exp != null)
        {
            exp.OnXpGain += UpdateLvl;
        }
    }

    //retirador de eventos
    private void OnDisable()
    {
        if (exp != null)
        {
            exp.OnXpGain -= UpdateLvl;
        }
    }

    //evento de update de lvl
    private void UpdateLvl()
    {
        int newLvl = CalcLvl();//recebe o calculo do lvl

        if (newLvl > currentLvl.value)
        {//caso o lvl calculado eh maior que o lvl atual
            currentLvl.value = newLvl;//troca o lvl
            LevelUpEfx();//faz efeito
            hpOnLvlUp();//muda hp
            atbPoints += 5;
            GameObject.Find("Player UI").GetComponent<UIController>().RemainingPoints(atbPoints);
            //AddStats();
            ///TODO adicionar alteraçao de estatos
        }
    }

    //efeito do evolucao de lvl
    private void LevelUpEfx()
    {
        Instantiate(lvlUpParticlesFx, transform);
        print("LVL UP!");
    }

    //recebe estatos
    public float GetStats(Stats st)
    {
        //if(gameObject.tag == "Player")
            //Debug.LogError(atb[(int)st] +" "+ st.ToString());
        return (atb[(int)st] + GetBaseStats(st) + GetAddModifier(st)) * (1 + GetPercentMod(st) / 100);
    }

    //recebe estados base
    private float GetBaseStats(Stats st)
    {
        return progression.GetStats(st, characterClass, GetLevel());
    }

    public void AddStats(int st, int value)
    {
        if (atbPoints > 0)
        {
            atb[st] += value;
            atbPoints--;
        }
        else
            GameObject.FindGameObjectWithTag("UIPanel").GetComponent<UIController>().RemainingPoints(atbPoints);
    }

    //recebe lvl
    public int GetLevel()
    {
        return currentLvl.value;
    }

    //recebe modificadores
    private float GetAddModifier(Stats stat)
    {
        float total = 0;//total do modificador
        foreach(IAddModifier findMod in GetComponents<IAddModifier>())
        {//para cada modificador no dicionario
            foreach(float mod in findMod.GetAddMods(stat))
            {//caso tenha o modificador
                total += mod;//adiciona na recerencia
            }
        }
        return total;//retorna valor do modificador
    }

    //modificador de porcentagem
    private float GetPercentMod(Stats stat)
    {
        //mesma coisa que o GetAddModifier()
        if (!useMods) return 0;//checa se pode receber modificador

        float total = 0;
        foreach (IAddModifier findMod in GetComponents<IAddModifier>())
        {//para cada modificador
            foreach (float mod in findMod.GetPercentMods(stat))
            {//para cada valor do modificador
                total += mod;
            }
        }
        return total;
    }

    //calculo do lvl
    public int CalcLvl()
    {
        XP xP = GetComponent<XP>();//referencia do xp

        if (xP == null) return startLevel;//checa se tem componente

        float currentXP = xP._xp;//xp atual

        int lastLvl = progression.GetLevelUp(Stats.LvlUp, characterClass);//recebe valor maximo de xp atual
        
        for (int lvl = 1; lvl <= lastLvl; lvl++)
        {//para cada lvl na lista
            float comparisson = progression.GetStats(Stats.LvlUp, characterClass, lvl);//recebe o valor maximo

            if (comparisson > currentXP)//caso o valor comparado for maior que o xp atual
                return lvl;//retorna lvl do index atual
        }        
        return lastLvl + 1;//caso nao retorna lvl maximo
    }
}
