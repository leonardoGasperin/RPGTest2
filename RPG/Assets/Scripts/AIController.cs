/*#### controla as açoes dos nao jogadores*/
using GameDevTV.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

/*namespace RPG.Controll
{
    //criar pasta Controll caso nao tenha
}*/

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDist = 5f;//distancia de agrecividade (raio)
    [SerializeField] float waitTime = 3f;//tempo de procura do player
    [SerializeField] float shoutDist = 5f;//distancia para chamar aggro amigo
    [SerializeField] float aggro = 5f;//distancia para aggro
    [SerializeField] float wpTolerance = 1f;//tempo parado no wp
    [Range(0, 1)] [SerializeField] float patrolSpdFrac = 0.2f;//velocidade durante patrulha
    [SerializeField] PatrolControll paths;//caminhos

    public bool aggred = false;

    Transform target;//coordenadas do alvo
    Combat combat;//referencia de Combat.cs
    Move move;//referencia de Move.cs
    float range;//distancia de ataque
    float lostTime = 3;//tempo de espera depois de perder alvo
    int idxRef = 0;//referencia do objeto
    float delay = 0;//atraso de ataque
    float rnd;//randomizacao
    float timeSiceAggro = Mathf.Infinity;//delay
    LazyValue<Vector3> guardPos;//pocisao para voltar

    private void Awake()
    {
        //inicializacao das variaveis
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        combat = GetComponent<Combat>();
        move = GetComponent<Move>();
        range = combat.weaponD.GetDamage();
        guardPos = new LazyValue<Vector3>(GetGuardPos);
    }

    private void Start()
    {
        //iniciando funcoes
        guardPos.ForceInit();
        rnd = Random.Range(3, 15);
    }

    private void Update()
    {
        //se o target nao tiver a tag ou se ele estiver morto sai
        if (target.tag != "Player" || this.GetComponent<Health>().Died()) return;
        if (CanAggro(target) || GetAttacked(aggred))//se consegui atacar o alvo
        {
            AttackBehaviour();//inicia o ataque
        }
        else if (!CanAggro(target))//se nao
        {
            PatrolBehaviour();//volta a patrulhar
        }
        DelayTimers();//joga delay
    }

    private void OnDrawGizmosSelected()
    {
        //referencia do alcance de visao target
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, chaseDist);
    }

    //delay do aggro
    public void Aggrevate()
    {
        AICombat(target);
        timeSiceAggro = 0;
    }

    private Vector3 GetGuardPos()
    {
        return transform.position;//retorna a posiçao do npc
    }

    //criase delay
    private void DelayTimers()
    {
        lostTime += Time.deltaTime;
        delay += Time.deltaTime;
        timeSiceAggro += Time.deltaTime;
    }

    //manda ai atacar
    private void AICombat(Transform t)
    {
        print(this.name + " chasing on -> " + target.gameObject.name);
        combat.Attack(t);
    }

    //checa se consegue atacar
    public bool CanAggro(Transform t)
    {
        //se tiver na distancia e vivo
        return (Vector3.Distance(this.transform.position, t.position) <= chaseDist && !t.gameObject.GetComponent<Health>().Died()) || timeSiceAggro < aggro;
    }

    public bool GetAttacked(bool isAtk = false)
    {
        return isAtk;
    }

    //manda fazer ataque
    private void AttackBehaviour()
    {
        lostTime = 0;//zera tempo de desencontro
        AICombat(target);
        AggrevateParty();
    }

    //gerador de aggro
    private void AggrevateParty()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDist, Vector3.up, shoutDist);//faz uma lista do que tem em volta na distancia do shout

        //para cara item no raio do shout
        foreach (RaycastHit hit in hits)
        {
            AIController aiAggro = hit.collider.GetComponent<AIController>();//se tiver o o component
            if (aiAggro == null) continue;
            aiAggro.Aggrevate();//gera aggro
        }
    }

    //patrulha
    public void PatrolBehaviour()
    {
        //se o tempo de desenccontro for menor que o de espera
        if (lostTime < waitTime)
            GetComponent<ActorScheduler>().CancelAction();
        else
        {
            //se tem caminho
            if (paths != null)
            {
                Vector3 next = GetWP();//vai para o procimo ponto
                if (AtWP())
                {
                    delay = 0;//zera delay
                    rnd = Random.Range(3, 15);//escolha um tempo para ficar parado
                    CycleWP();//continue o ciclo
                }

                next = GetWP();//proximo
                if (delay > rnd)//se delay for maior q o tempo de espera
                    move.StartActing(next, patrolSpdFrac, false, false);//mova para o proximo ponto
            }
            else
            {
                //caso o ponto do objeto for diferente do da patrulha o mova ate o ponto
                if (Vector3.Distance(new Vector3(transform.position.x, 1, transform.position.z), guardPos.value) > 0.5f)
                    move.StartActing(guardPos.value, patrolSpdFrac, false, false);
            }
        }
    }

    //chegou no ponto
    private bool AtWP()
    {
        float distWp = Vector3.Distance(transform.position, GetWP());//distancia entre objeto e ponto
        return distWp < wpTolerance;
    }

    //pega novo ponto
    private Vector3 GetWP()
    {
        return paths.GetWP(idxRef);
    }

    //continua o ciclo
    private void CycleWP()
    {
        idxRef++;
        if (idxRef >= paths.transform.childCount)//se idx ultrapasar o tamanho de paths resete
            idxRef = 0;
    }
}
