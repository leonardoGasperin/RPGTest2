/*#### scripts para portal de mudança de cena*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

/*namespace RPG.Manager
{
    //criar pasta Manager caso nao tenha
}*/

public class Portal : MonoBehaviour
{
    //identifica portais
    enum DestinationIdentifier
    {
        A, B
    }
    [SerializeField]int chsSceneIndx;//index da scena
    [SerializeField]Transform spawnPoint;//ponto de spanw
    [SerializeField]DestinationIdentifier dest;//referencia do destino
    [SerializeField] float fadeTimer;//tempo do fade
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Transition());//trancisao de scena
        }
    }

    //co rotina de transiçao de scena
    private IEnumerator Transition()
    {
        //se o index da cena escolido for menor que 0 -> erro
        if (chsSceneIndx < 0) { Debug.LogError("Scene index invalid."); yield break; }

        DontDestroyOnLoad(gameObject);//nao destoi no carregamento

        const string svF = "Save";//referencia do arquivo de auto save
        SavingSystem svS = FindObjectOfType<SavingSystem>();//referencia a script de salvar
        Fader fade = FindObjectOfType<Fader>();//referencia do script para fade
        DisableCtrl();//tira controles do personagem
        yield return fade.FadeOut(fadeTimer);//faz fade
        
        svS.Save(svF);//salva jogo
        yield return SceneManager.LoadSceneAsync(chsSceneIndx);//carrega cenario
        DisableCtrl();//tira controles do personagem
        svS.Load(svF);//carrega save

        Portal otherPortal = GetOtherPortal();//referencia do portal de entrada
        UpdatePlayer(otherPortal);//update as informacoes do jogador
        svS.Save(svF);//salva

        yield return new WaitForSeconds(0.5f);
        yield return fade.FadeIn(fadeTimer);//cria fade

        EnableCtrl();//reativa os controles do personagem
        Destroy(gameObject);//deleta objeto
    }

    //desativa o controle do personagem
    void DisableCtrl()
    {
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        pl.GetComponent<ActorScheduler>().CancelAction();//cancela acao do objeto
        pl.GetComponent<PlayerController>().enabled = false;//desativa script de controle
        print("DisableCtrl");
    }

    //co rotina de ativacao de controle
    void EnableCtrl()
    {
        GameObject pl = GameObject.FindGameObjectWithTag("Player");
        pl.GetComponent<PlayerController>().enabled = true;//ativa controle
        print("EnableCtrl");
    }

    //controle dos dados do jogador
    private void UpdatePlayer(Portal otherP)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");//referencia do jogador
        player.GetComponent<NavMeshAgent>().enabled = false;//desativa navmeshagente
        player.GetComponent<NavMeshAgent>().Warp(otherP.spawnPoint.position);//teletransporta jogador para o ponto de spawn
        player.transform.rotation = otherP.spawnPoint.rotation;//e rotacao
        player.GetComponent<NavMeshAgent>().enabled = true;//volta nav mesh agent
    }

    //referencia do portal de entrada/saida
    private Portal GetOtherPortal()
    {
        foreach(Portal p in FindObjectsOfType<Portal>())//para cada portal
        {
            if (p == this) continue;//checa se eh o mesmo portal
            if (p.dest != dest) continue;//checa se eh portal diferente
            return p;
        }

        return null;
    }
}
