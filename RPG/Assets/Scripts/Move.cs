/*#### Açao de movimento, faz moviento do personagem caso ele clique em algum lugar andavel, ou alcansavel(inimigos,npc, etc)
 #### recebe uma açao (vinda de um botao de movimento ou click do mouse)
 #### e faz o personagem se mover para local deseado*/
using System;
using UnityEngine;
using UnityEngine.AI;

/*namespace RPG.Actor
{
    //criar pasta Actor caso nao tenha
}*/

public class Move : MonoBehaviour, ISaveable, IAction//inplementa Interface de açoes
{
    [SerializeField]float maxSpd = 5;//velocidade maxima

    Rigidbody rb;
    NavMeshAgent nav;//referencia do nav mesh agente para o click
    Animator animator;//referencia do animator
    bool isKeyboard = false;
    
    private void Awake()
    {
        //inicializa a variaveis
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GetComponent<Health>().Died())//checa se o personagem esta morto
            nav.enabled = false;//se sim tira o navmesh
        
        CharactAnim(isKeyboard);//manda fazer as animaçoes de movimentos
    }

    //controlador de animaçao
    private void CharactAnim(bool isKb)
    {
        if (!isKb)
        {
            Vector3 vel = nav.velocity;//vetor de vlocidade para acesso da velocidade do nav mesh agent
            Vector3 localVel = transform.InverseTransformDirection(vel);//transforma a velocidade do navmesh (world value) para local
            float spd = localVel.z;//velocidade do valor foward do personagem
            animator.SetFloat("speed", spd);//manda a informaçao para o animator
        }
        else
        {
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                animator.SetFloat("speed", maxSpd);
            else
                animator.SetFloat("speed", 0);
        }
    }

    //inicia a açao de movimentacao recebendo um vetor contendo o ponto de destino
    public void StartActing(Vector3 dest, float spdFrac, bool isKb, bool jump)
    {
        GetComponent<ActorScheduler>().StartAction(this);//inicia a açao cancelando a anterior caso seja uma açao difetente
        
        if (jump)
        {
            Jumping();
        }

        if (!isKb)
        {
            MoveTo(dest, spdFrac, isKb);//manda mover para o ponto
        }
        else
            MoveKey(dest, isKb);
            
    }

    ///TODO metodos para movimentaçao de teclado, pulo e joypad
    ///TODO Jump(); JoyPadMove()

    public void Jumping()
    {
        rb.AddForce(Vector3.up * 80);
    }

    //manda movimentar para o ponto de destino
    public void MoveTo(Vector3 _dest, float _spdFrac, bool kb)
    {
        if(nav.enabled)
        {
            isKeyboard = kb;
            transform.LookAt(_dest);//aponta o foward do personagem para o ponto de destino
            nav.destination = _dest;//informa o destino para o nav mesh
            nav.speed = maxSpd * Mathf.Clamp01(_spdFrac);//fraciona velocidade
            nav.isStopped = false;
        }
    }

    //manda se movimentar na direcao do eixo escolido A/D esquerda direita ou W/S frente traz
    private void MoveKey(Vector3 _dest, bool kb)
    {
        if (nav.enabled)
            Cancel();
        isKeyboard = kb;

        Vector3 locomotion;
        
        locomotion = this.transform.forward * _dest.x * maxSpd * Time.deltaTime;
        transform.position += locomotion;
        transform.Rotate(new Vector3(0, _dest.z, 0) * maxSpd);
    }
    
    //volta movimento
    public void Resume()
    {
        nav.isStopped = false;
    }

    //cancela açao de movimento
    public void Cancel()
    {
        nav.isStopped = true;
    }

    public void NavEnable(bool j)
    {
        if (j)
            nav.enabled = false;
        else
            GetComponent<NavMeshAgent>().enabled = true;
    }

    //captura estado para salvar
    public object CaptureState()
    {
        return new SerializableVector3(transform.position);//posiçao atual
    }

    //restoura posiçao do salvado
    public void RestoreState(object state)
    {
        SerializableVector3 pos = (SerializableVector3)state;//cria um tipo serializado de vetor que recebe o estado correspondente
        nav.enabled = false;//desativa navmeshagent
        transform.position = pos.ToVector();//posiçao atualiza para pos des serializado
        nav.enabled = true;////ativa navmeshagent
    }
}
