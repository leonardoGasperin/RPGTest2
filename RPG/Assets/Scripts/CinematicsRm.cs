/*#### controle do personagem durante cinematics*/
using UnityEngine;
using UnityEngine.Playables;

public class CinematicsRm : MonoBehaviour
{
    GameObject pl;//referencia do jogador

    // Start is called before the first frame update
    void Awake()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        //inplementa as corroutines
        GetComponent<PlayableDirector>().played += DisableCtrl;
        GetComponent<PlayableDirector>().stopped += EnableCtrl;
    }

    private void OnDisable()
    {
        //inplementa as corroutines
        GetComponent<PlayableDirector>().played -= DisableCtrl;
        GetComponent<PlayableDirector>().stopped -= EnableCtrl;
    }

    //co rotina de desativar controle
    void DisableCtrl(PlayableDirector pd)
    {
        print("DisableCtrl");
        pl.GetComponent<ActorScheduler>().CancelAction();//cancela acao do objeto
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;//desativa script de controle
    }

    //co rotina de ativacao de controle
    void EnableCtrl(PlayableDirector pd)
    {
        print("EnableCtrl");
        pl.GetComponent<PlayerController>().enabled = true;//ativa controle
    }
}
