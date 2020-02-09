using System.Collections;
using UnityEngine;

public class EntraceUIController : MonoBehaviour
{
    GameObject saver;

    // Start is called before the first frame update
    void Start()
    {
        saver = GameObject.Find("PersistObjects(Clone)");
    }

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

    private IEnumerator Transition()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);//carrega cenario
    }
}
