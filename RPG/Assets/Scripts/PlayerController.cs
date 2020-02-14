/*#### Este script excluso para o Player é o controlador/comando do jogador,
 #### controla todas as possibilidades como personagem*/
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/*namespace RPG.Controll
{
    //criar pasta Controll caso nao tenha
}*/

public class PlayerController : MonoBehaviour
{
    private Move move;//referencia da classe Move

    //estrutura para o mapeamento do cursor
    [System.Serializable]
    struct CursorMapping
    {
        //referencias
        public CursorType type;
        public Vector2 hostpos;
        public Texture2D Texture;
    }

    [SerializeField] CursorMapping[] cursorMappings = null;//variavel de mapeamento

    void Awake()
    {
        //inicializando variaveis
        move = GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractUI()) return;//se o cursor esta em uma UI
        
        CameraControll();//checa se esta pedindo algum controle da camera 
        
        if (!GetComponent<Health>().Died())//se o jogador estiver vivo
        {
            if (Jump(Input.GetKeyDown(KeyCode.Space)))//se pular
                move.StartActing(transform.position, 0, false, Jump(Input.GetKeyDown(KeyCode.Space)));
            if (KeyMove())//se tiver movendo pelo teclado
            {
                SeePlayer(true);//manda a camera seguir personagem
                return;
            }
            if (SelectTarget()) return;
            if (InteractComponent() && !CursorMove()) return;//ve que tipo de componente interagivel
            if (CursorMove())
                return;
            SeePlayer(false);//manda a camera parar de seguir personagem
        }
        SetCursor(CursorType.none);//caso nao seja nenhuma das opçoes
    }

    //manda a camera seguir ou nao o jogador
    private void SeePlayer(bool active)
    {
        GameObject.FindGameObjectWithTag("CamControll").GetComponent<CameraController>().LookAtPlayer(active, Input.GetMouseButton(1), transform);
    }

    private bool Jump(bool isJump)
    {
        if(isJump)//se apertou space bar
        {
            move.NavEnable(isJump);//desativa navmesh
            if(IsGround())//se tiver no chão
            {
                return true;
            }
            return false;
        }
        return false;
    }

    //checa se esta no chao
    private bool IsGround()
    {
        return Physics.Raycast(transform.position, -Vector3.up,  0.25f);
    }

    //controle basicos da camera
    private void CameraControll()
    {
        //checa zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            GameObject.FindGameObjectWithTag("CamControll").GetComponent<CameraController>().Zoom(Input.GetAxis("Mouse ScrollWheel"));
        if (Input.GetMouseButton(1))//checa orbitação
        {
            GameObject.FindGameObjectWithTag("CamControll").GetComponent<CameraController>().Orbitation(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    //seleciona alvo
    private bool SelectTarget()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        { 
            GetTarget(); 
            return true; 
        }
        return false;
    }

    //recebe alvo
    /// <summary>
    /// faz uma lista de alvos proximo e seleicoan o primeiro
    /// </summary>
    private void GetTarget()
    {
        GameObject[] tg;
        tg = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject _tg in tg)
        {
            if (Vector3.Distance(this.gameObject.transform.position, _tg.transform.position) < 25 && !_tg.GetComponent<Health>().Died())
                this.gameObject.GetComponent<Combat>().selectTg = _tg.GetComponent<Health>();
        }

    }

    //interage com certo componente
    private bool InteractComponent()
    {
        RaycastHit[] hits = RaycastAllSort();//gera lista de RaycastHit de onde o mouse esta com a distancia e origem do objeto
        foreach (RaycastHit hit in hits)//para cada elemento da lista
        {
            //referencia da lista dos objetos de interaçao
            IRayCastable[] rayCastables = hit.transform.GetComponents<IRayCastable>();
            foreach (IRayCastable castable in rayCastables)
            {//para cada interagivel
                if (castable.HandleRaycast(this))
                {//se tiver na lista e for o menor distante retorna o tipo de cursor
                    SetCursor(castable.GetCursorType());//muda o cursor
                    return true;
                }
            }
        }
        return false;
    }

    //movimento por cursor
    private bool CursorMove()
    {
        //RaycastHit hit;//hit que recebera a informação do click
        Vector3 hitT;
        bool hasHit = RaycastNav(out hitT);
        if (hasHit)//(Physics.Raycast(GetRay(), out hit))//pede um click valido no jogo
        {
            if (Input.GetMouseButton(0))//caso click ou esteja clicado
            {
                move.NavEnable(false);
                GameObject.FindGameObjectWithTag("CamControll").GetComponent<CameraController>().LookAtPlayer(true, Input.GetMouseButton(1), this.transform);
                move.StartActing(hitT, 1, false, Jump(Input.GetKeyDown(KeyCode.Space)));//inicia a açao andar rm move passando o ponto de hit como referencia
            }
            if (Vector3.Distance(this.transform.position, hitT) <= 0.5f)
            {
                move.Cancel();
                return false;
            }
            SetCursor(CursorType.basic);//muda cursor

            return true;//retorna verdadeiro para linha 28
        }
        return false;//caso nao seja um click valido retorne falso para a linha 28
    }

    //movimento por teclado
    private bool KeyMove()
    {
        Vector3 axis = Vector3.zero;
        axis.z = Input.GetAxis("Horizontal");//A/D
        axis.x = Input.GetAxis("Vertical");//W/S

        if (axis.x != 0 || axis.z != 0)
        {
            move.StartActing(axis, 1, true, Jump(Input.GetKeyDown(KeyCode.Space)));
            return true;
        }
        return false;
    }

    //calcula o caminho do navmash
    private bool RaycastNav(out Vector3 target)
    {
        target = new Vector3();
        RaycastHit hit;
        bool hasHit = Physics.Raycast(GetRay(), out hit);

        if (!hasHit)
            return hasHit;

        NavMeshHit navhit;
        bool castHit = NavMesh.SamplePosition(hit.point, out navhit, 0.5f, NavMesh.AllAreas);

        if (!castHit)
            return castHit;

        target = navhit.position;

        NavMeshPath navPath = new NavMeshPath();
        bool path = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, navPath);
        if (!path) return false;
        if (navPath.status != NavMeshPathStatus.PathComplete) return false;

        return castHit;
    }

    /// <summary>
    /// Pega todos os hits. 
    /// Separa por distancia.
    /// Contruir Array.
    /// separa os hits.
    /// retorna hit correto.
    /// </summary>
    RaycastHit[] RaycastAllSort()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetRay());//gera lista de RaycastHit de onde foi clicado
        float[] distances = new float[hits.Length];//gera lista de distancias

        for (int i = 0; i < hits.Length; i++)
        {//para cada hits
            distances[i] = hits[i].distance;//a distancia entre o click e o objeto
        }
        
        Array.Sort(distances, hits);//ajunta as listas
        return hits;//retorna a lista
    }

    //pega o ponto onde foi clicado
    private static Ray GetRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);//retorna o ponto final do vetor da "tela" da camera principal com a direçao do mouse no momento do click
    }

    //alterador de cursor
    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);//referencia do cursor
        Cursor.SetCursor(mapping.Texture, mapping.hostpos, CursorMode.Auto);//altera cursor
    }

    //recebe o cursor desejado
    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping mapping in cursorMappings)
        {//para cada tipo de curso
            if (mapping.type == type)
                return mapping;//retorna o cursor caso haja na lista
        }

        return cursorMappings[0];//retorna cursor padrao
    }

    //interaçao com UI
    private bool InteractUI()
    {
        if (EventSystem.current == null) return false;
        //se o cursor estiver encima de alguma UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            SetCursor(CursorType.UI);//muda o cursor
            return true;
        }

        return false;
    }
}
