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

    public void Save(string svF)
    {
        GetComponent<SavingSystem>().Save(svF);//chama o salvamento dos dados
    }

    public void Load(string svF, bool isHall)
    {
        if (svF == "OwnSave" && !isHall)//checa se ao esta na tela de inicio
            StartCoroutine(GetComponent<SavingSystem>().LoadLastScene(svF));
        else if (isHall)//se estiver na tela de entrada
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

