/*#### Menu de opções do jogo contendo todas as funções basicas de um menu
  #### salvar/carregar/autosave/voltar ao inicio/sair*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    //referencia dos botoes e objetos
    [SerializeField] Button opc = null;
    [SerializeField] GameObject menuUI = null;
    [SerializeField] Button save = null;
    [SerializeField] Button load = null;
    [SerializeField] Button autoSave = null;
    [SerializeField] Button entrace = null;
    [SerializeField] Button exit = null;

    //referencia do save/load file
    GameObject saver;
    // Start is called before the first frame update
    void Start()
    {
        //inicializando variavel
        saver = GameObject.Find("PersistObjects(Clone)");
    }

    //abre e fecha por click no botao
    public void OpenClose()
    {
        menuUI.SetActive(!menuUI.activeSelf);
    }

    public void Save()
    {
        saver.GetComponentInChildren<SavingWrapper>().Save("OwnSave");
    }

    public void Load()
    {
        saver.GetComponentInChildren<SavingWrapper>().Load("OwnSave", false);
    }

    public void AutoSave()
    {
        saver.GetComponentInChildren<SavingWrapper>().Load("Save", true);
    }

    //retorna para tela de inicio
    public void Entrace()
    {
        StartCoroutine(Transition());
    }

    public void Exit()
    {
        Application.Quit();
    }

    //transfere para tela de inicio
    private IEnumerator Transition()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);//carrega cenario
    }
}
