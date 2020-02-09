/*#### controla a interface do usuarios para continuar salvar carregar e deletar jogo*/
using System.Collections;
using UnityEngine;

/*namespace RPG.Save
{
    //criar pasta Save caso nao tenha
}*/

public class SavingWrapper : MonoBehaviour
{
    public const string defaultSv = "Save";//nome e referencia do arquivo de save principal
    const string hitSv = "OwnSave";//nome e referencia do arquivo de save profile

    //carrega ultima scena salva
    private IEnumerator LoadLastScene()
    {
        yield return GetComponent<SavingSystem>().LoadLastScene(defaultSv);//carrega a scena sincronizada com o cpu
    }

    /*private void Awake()
    {
        //inicializador de variaveis
        StartCoroutine(LoadLastScene());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Save(hitSv);//funçáo para salvar profile
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Load(hitSv, false);//funçáo para carregar profile
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Delete(defaultSv);
        }
    }*/

    public void Save(string svF)
    {
        GetComponent<SavingSystem>().Save(svF);//chama o salvamento dos dados
    }

    public void Load(string svF, bool isHall)
    {
        if (svF == "OwnSave" && !isHall)
            StartCoroutine(GetComponent<SavingSystem>().LoadLastScene(svF));
        else if (isHall)
        {
            StartCoroutine(GetComponent<SavingSystem>().LoadLastScene(svF));
        }
        else
            GetComponent<SavingSystem>().Load(svF);//chama o carregamento dos dados
    }

    public void Delete(string svf)
    {
        GetComponent<SavingSystem>().Delete(svf);//Deleta save padrao
    }
}

