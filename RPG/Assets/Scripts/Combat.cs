/*#### açao de combate, faz com que o personagem ataque um inimigo selecionado e vice e versa*/
using GameDevTV.Utils;
using System.Collections.Generic;
using UnityEngine;

/*namespace RPG.Combat
{
    //criar pasta Combat caso nao tenha
}*/

public class Combat : MonoBehaviour, IAction, ISaveable, IAddModifier
{
    [SerializeField] float recuT = 0;//tempo de recuo do ataque
    [SerializeField] Transform rightHandT = null;//referencia da mao direita
    [SerializeField] Transform leftHandT = null;//referencia da mao esquerda
    [SerializeField] public WeaponConfig weaponD = null;//arma padrao


    Health target;//transform do alvo
    Move mov;//referencia da açao move
    float timeSiceAtk = Mathf.Infinity;//delay do ataque
    WeaponConfig weaponConfig;//arma atual
    LazyValue<Weapon> WeaponC;//arma atual

    private void Awake()
    {
        //inicializa variaveis
        mov = GetComponent<Move>();
        weaponConfig = weaponD;
        WeaponC = new LazyValue<Weapon>(SetupDefault);
    }

    // Start is called before the first frame update
    void Start()
    {
        WeaponC.ForceInit();
    }

    //equipa arma padrao
    private Weapon SetupDefault()
    {
        return AttachWeapon(weaponD);
    }

    // Update is called once per frame
    void Update()
    {
        timeSiceAtk++;//a cada frame almenta em 1 o valor do delay
        if (target == null) return;//caso nao tenha target sai
        else
        {//caso contrario
            if(CanAtack(target.gameObject) && (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Alpha1)))
                mov.MoveTo(target.transform.position, 1f, false);//move até o alvo
            
            if (GetInRange(target.transform))
            {//quando chega na distancia da arma
                mov.Cancel();//cancela movimento
                AttackBehaviour();//inicia a animaçao de ataque
            }
        }
    }

    public Health selectTg
    {
        set { target = value; }
    }

    //retorna distancia da arma
    private bool GetInRange(Transform targetT)
    {
        return Vector3.Distance(transform.position, targetT.position) < weaponConfig.GetRange();
    }

    //equipa uma arma
    public void EquipWeapon(WeaponConfig w)
    {
        weaponConfig = w; 
        WeaponC.value = AttachWeapon(w);
    }

    //coloca arma no personagem
    private Weapon AttachWeapon(WeaponConfig w)
    {
        Animator anim = GetComponent<Animator>();//troca a animacao
        return w.Spawn(rightHandT, leftHandT, anim);
    }

    //retorna um alvo
    public Health GetTarget()
    {
        return target;
    }

    //animaçao de ataque
    private void AttackBehaviour()
    {
        if (target.GetComponent<Health>().Died())
        {//checa se o alvo esta morto
            Cancel();//cancela ataque
        }
        if (target != null && timeSiceAtk > recuT)
        {//se o tempo de delay suprimiu o tempo de recuo
            timeSiceAtk = 0;//zera delay
            GetComponent<Animator>().SetTrigger("attack");//inicia animaçao de ataque
        }
    }

    //checa se pode atacar
    public bool CanAtack(GameObject target)
    {
        if (target == null) return false;//caso nao tenha alvo
        
        Health test = target.GetComponent<Health>();//referencia do hp do alvo

        return test != null && !test.Died();//retorna se tem alvo e se esta vivo
    }

    //manda iniciar açao de ataque recebendo um alvo valido
    public void Attack(Transform t)
    {
        GetComponent<ActorScheduler>().StartAction(this);//manda iniciar açao
        target = t.GetComponent<Health>();//target recebe transform do alvo
    }

    //cancela açao
    public void Cancel()
    {
        GetComponent<Animator>().ResetTrigger("attack");//desativa trigger do animator
        target = null;//zera target
        mov.Cancel();
    }

    //adiciona modificadores
    public IEnumerable<float> GetAddMods(Stats stat)
    {
        //se for modificador de dano
        if (stat == Stats.Str)
            yield return weaponConfig.GetDamage();
    }

    //adiciona modificador de porcentagem
    IEnumerable<float> IAddModifier.GetPercentMods(Stats stat)
    {
        //se for modificador de dano
        if (stat == Stats.Str)
            yield return weaponConfig.GetPercentBonus();
    }

    //evento de animacao
    void Hit()
    {
        if (target == null) return;//se nao tiver hit valido sai
        float dmg = GetComponent<CalcStats>().GetStats(Stats.Str);

        if (WeaponC.value != null)
            WeaponC.value.OnHit();

        //checa se é arma de long range ou nao
        if (weaponConfig.HasProjectile())
        {
            weaponConfig.LauchProjectile(rightHandT, leftHandT, target, gameObject, dmg);//retira vida
        }
        else
            target.GetComponent<Health>().Damge(gameObject, dmg);//retira vida
            
    }

    void Shoot()
    {
        Hit();
    }

    //salva estados
    public object CaptureState()
    {
        return weaponConfig.name;
    }

    //restoura estado
    public void RestoreState(object state)
    {
        string wN = (string)state;
        WeaponConfig w = Resources.Load<WeaponConfig>(wN);
        EquipWeapon(w);
    }
}
