/*#### administra pontos de dano(vida) do personagem e estatos da vida*/
using GameDevTV.Utils;
using UnityEngine;
using UnityEngine.Events;

/*namespace RPG.Combat
{
    //criar pasta Combat caso nao tenha
}*/

public class Health : MonoBehaviour, ISaveable
{
    //eventos
    [SerializeField] TakeDmgEvent takeDmg;//rece dano
    [SerializeField] UnityEvent onDie;//morrer

    LazyValue<float> hp;//pontos de vida
    bool died = false;//estado de vida

    //mostra evento na engine
    [System.Serializable]
    public class TakeDmgEvent : UnityEvent<float>
    {
    }

    private void Awake()
    {
        //inicializa variaveis
        hp = new LazyValue<float>(GetInitialHp);
    }

    private void Start()
    {
        hp.ForceInit();
    }

    //adiciona evento
    private void OnEnable()
    {
        GetComponent<CalcAtb>().hpOnLvlUp += UpdateHp;
    }

    //retira evento
    private void OnDisable()
    {
        GetComponent<CalcAtb>().hpOnLvlUp -= UpdateHp;
    }

    //receve valor da vida inicial
    private float GetInitialHp()
    {
        return GetComponent<CalcAtb>().GetHp();
    }

    //retorna estado da vida
    public bool Died()
    {
        return died;
    }
    
    //modifica a vida pelo valor recebido
    public void Damge(GameObject target, float dmg)
    {
        this.hp.value = Mathf.Max(hp.value - dmg, 0);//pega o maior valo entre o max de vida atual - dmg e 0
        //Debug.LogError(this.gameObject.name + " " + dmg);
        if (this.hp.value <= 0)//caso a vida seja 0
        {
            onDie.Invoke();//invoca evento de morte
            DieSt();//chama estado de morto
            GainXP(target);//recebe experiancia
        }
        else
        {//caso contrario tira vida do dano recebido
            takeDmg.Invoke(dmg);
        }
    }

    //cura de HP-->serve para regeneraçao ou para curar
    public void Heal(float value)
    {
        if (hp.value < GetMaxHp())
        {
            this.hp.value = Mathf.Min(hp.value + value, GetMaxHp());//pega o maior valo entre o max de vida atual - dmg e 0
        }
    }

    //curar toda a vida ao upar de nivel
    private float GetMaxHp()
    {
        return GetComponent<CalcAtb>().GetHp();//retorna o maximo de hp
    }

    //devolve a porcentagem de vida do jogador e alvos
    public float GetPercentge()
    {
        return GetFraction() * 100;
    }

    //pega valor fracionario do HP
    public float GetFraction()
    {
        return hp.value / GetComponent<CalcAtb>().GetHp();
    }

    //confirma morte do personagem
    public void DieSt()
    {
        this.GetComponent<ActorScheduler>().CancelAction();//cancela acoes
        this.gameObject.GetComponent<Animator>().SetTrigger("dead");//inicia a animaçao de morto
        this.GetComponent<CapsuleCollider>().enabled = false;//retira colider
        this.GetComponent<Rigidbody>().useGravity = false;
        died = true;//confirma morto
        this.gameObject.GetComponent<Animator>().SetBool("n", died);//para animador
    }

    //recebe experiencia
    private void GainXP(GameObject target)
    {
        XP xp = target.GetComponent<XP>();//referencia de xp

        if (xp == null) return;//se nao da experiancia

        xp.GainXP(GetComponent<CalcAtb>().GetStats(Stats.XP));//pega quantia de experiancia
    }

    //return actual hp
    public void UpdateHp()
    {
        hp.value = GetComponent<CalcAtb>().GetHp();
    }

    //salva hp
    public object CaptureState()
    {
        return hp.value;
    }

    //restalta hp do objeto
    public void RestoreState(object state)
    {
        hp.value = (float)state;//receve o valor
        if (hp.value <= 0)//checa se nao tem vida
        {
            DieSt();//mata
            this.gameObject.GetComponent<Animator>().SetBool("d", Died());
        }
        else
        {
            this.gameObject.GetComponent<Health>().died = false;//tira flag morto
            this.gameObject.GetComponent<Animator>().SetTrigger("d");//reseta trigger
            this.gameObject.GetComponent<Animator>().SetBool("n", died);//tira flag do animator
            this.GetComponent<CapsuleCollider>().enabled = true;//volta capsule
            this.gameObject.GetComponent<Animator>().ResetTrigger("dead");//reseta trigger de animacao
        }
    }
}
