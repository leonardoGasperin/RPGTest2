/*#### Script para funções da UI da tela inicial*/
using System.Collections;
using UnityEngine;

public class EntraceUIController : MonoBehaviour
{
    GameObject saver;//referencia do save/load file

    // Start is called before the first frame update
    void Start()
    {
        //inicialização de variaveis
        saver = GameObject.Find("PersistObjects(Clone)");
    }

    ///cada função faz exatamente o que o nome indica
  
    public void NewGame()
    {
        StartCoroutine(Transition());
    }

    public void AutoSave()
    {
        saver.GetComponentInChildren<SavingWrapper>().Load("Save", true);
    }

    public void Load()
    {
        saver.GetComponentInChildren<SavingWrapper>().Load("OwnSave", true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    //carrega novo jogo
    private IEnumerator Transition()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);//carrega cenario
    }
}
