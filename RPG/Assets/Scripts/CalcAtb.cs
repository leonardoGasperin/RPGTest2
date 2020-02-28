/*####Adminsitra estatos dos personagens*/
using GameDevTV.Utils;
using System;
using UnityEngine;

public class CalcAtb : MonoBehaviour, ISaveable
{
    //referencias e variaveis
    [Range(1, 99)][SerializeField] int startLevel = 1;//nivel inicial
    [SerializeField] CharacterClass characterClass;//classes
    [SerializeField] Progression progression = null;//referenciado arquivo de progrecao
    [SerializeField] GameObject lvlUpParticlesFx;//efeito de evento de evoluir
    [SerializeField] bool useMods = false;//uso de modificadores
    public event Action hpOnLvlUp;//evento de evolucao
    int atbPoints = 0;//pontos para distribuir
    public float[] atb;//lista de pontos distribuidos nos atributos

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
            atbPoints += 5;//soma 5 pontos para ser distribuido
            GameObject.Find("UIManager").GetComponentInChildren<AttributesUI>().RemainingPoints(atbPoints);//envia valor para UI
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
        if ((int)st < 3 && (int)st >= 0)
            return ( GetBaseStats(st) + GetAddModifier(st)) * (1 + GetPercentMod(st) / 100);
        else
            return (atb[(int)st] +GetBaseStats(st)) /*+ GetAddModifier(st))*/ * (1 + GetPercentMod(st) / 100);
    }

    //recebe estados base
    private float GetBaseStats(Stats st)
    {
        return progression.GetStats(st, characterClass, GetLevel());
    }

    //adiciona pontos ao atributo recebido de valor 1
    public void AddStats(int st, int value)
    {
        //se ainda tem pontos para distribuir
        if (atbPoints > 0)
        {
            atb[st] += value;//adiciona +1 ao atributo
            atbPoints--;//diminui um ponto nos pontos restantes
            GameObject.Find("UIManager").GetComponentInChildren<AttributesUI>().RemainingPoints(atbPoints);//atualiza UI
        }
    }

    //recebe lvl
    public int GetLevel()
    {
        return currentLvl.value;
    }

    //pega valor do HP
    public int GetHp()
    {
        return (int)(GetStats(Stats.Health) + ((GetStats(Stats.Pro) + atb[4]) * 0.75f));
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

    //struct para salvar arrays de atributos distribuidos
    [System.Serializable]
    struct AttributesFile
    {
        public float str;
        public float pro;
        public float agi;
        public float dex;
        public float intt;
        public float eru;
    }

    public object CaptureState()
    {
        AttributesFile data = new AttributesFile();

        data.str = atb[3];
        data.pro = atb[4];
        data.agi = atb[5];
        data.dex = atb[6];
        data.intt = atb[7];
        data.eru = atb[8];

        return data;
    }

    public void RestoreState(object state)
    {

        AttributesFile data = (AttributesFile)state;
        atb[0] = 0;
        atb[1] = 0;
        atb[2] = 0;
        atb[3] = data.str;
        atb[4] = data.pro;
        atb[5] = data.agi;
        atb[6] = data.dex;
        atb[7] = data.intt;
        atb[8] = data.eru;
    }
}
