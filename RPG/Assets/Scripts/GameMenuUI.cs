using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuUI : MonoBehaviour
{
    [SerializeField] Button opc = null;
    [SerializeField] GameObject menuUI = null;
    [SerializeField] Button save = null;
    [SerializeField] Button load = null;
    [SerializeField] Button autoSave = null;
    [SerializeField] Button entrace = null;
    [SerializeField] Button exit = null;

    GameObject saver;
    // Start is called before the first frame update
    void Start()
    {
        saver = GameObject.Find("PersistObjects(Clone)");
    }

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

    public void Entrace()
    {
        StartCoroutine(Transition());
    }

    public void Exit()
    {
        Application.Quit();
    }
    private IEnumerator Transition()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);//carrega cenario
    }
}
