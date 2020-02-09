/*#### sistema de salvamento de data do jogo, gerencia e serializa o salvamento das datas recebida pela interface ISaveable.cs*/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

/*namespace RPG.Saving
{
    // criar pasta Saving caso nao tenha feito ainda
}*/

public class SavingSystem : MonoBehaviour
{
    //restaura a ultima cena salva no savefile
    public IEnumerator LoadLastScene(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);//cria um dicionario contendo as informacoes do saveFile
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (state.ContainsKey("lastSceneBuildIndex"))//confere se tem a key
        {
            buildIndex = (int)state["lastSceneBuildIndex"];//recebe o valor do index da cena ativa
        }
        yield return SceneManager.LoadSceneAsync(buildIndex);
        RestoreState(state);
    }

    //faz recolimento dos dados a partir do nome do arquivo
    public void Save(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);//faz um dicionario contendo as informacoes do arquivo recebido
        CaptureState(state);//escreve as informacoes novas
        SaveFile(saveFile, state);//salva o arquivo
    }

    //restalra os dados
    public void Load(string saveFile)
    {
        RestoreState(LoadFile(saveFile));
    }

    //deleta arquivo do save
    public void Delete(string saveFile)
    {
        File.Delete(GetPathFromSaveFile(saveFile));
    }

    //abre arquivo
    private Dictionary<string, object> LoadFile(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);//pega o caminho do arquivo
        if (!File.Exists(path))//se o arquivo nao existe
        {
            Debug.LogError("FAIL Load on " + saveFile);
            return new Dictionary<string, object>();//retorna um novo dicionario vaziu
        }//se nao
        using (FileStream stream = File.Open(path, FileMode.Open))//usando file stream abra o arquivo recebido
        {
            //Debug.LogWarning("Load on " + saveFile);
            BinaryFormatter formatter = new BinaryFormatter();//formatador binario para des serializar os dados do data save
            return (Dictionary<string, object>)formatter.Deserialize(stream);//e casta como dicionario a des-serializaçao do arquivo para retorno
        }//fecha arquivo
    }

    //salva arquivo
    private void SaveFile(string saveFile, object state)
    {
        string path = GetPathFromSaveFile(saveFile);//pega o local do arquivo
        print("Saving to " + path);
        using (FileStream stream = File.Open(path, FileMode.Create))//usando file stream cria um arquivo no caminho
        {
            BinaryFormatter formatter = new BinaryFormatter();//formatador binario para serializar dados
            formatter.Serialize(stream, state);//serializa dados recebidos
        }//fecha arquivo
    }

    //pega dados para salvar
    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())//para cada entidade salvavel(characters, etcs)
        {
            state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();//salva cada ISaveable type correspondente a seu identificador
        }

        state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;//salva index da scena
    }

    //restaura estado
    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())//para cada entidade salva em uma lista de SaveableEntity
        {
            string id = saveable.GetUniqueIdentifier();//pega o identificador
            if (state.ContainsKey(id))//checa se existe
            {
                saveable.RestoreState(state[id]);//e restaura os dados da endentidade
            }
        }
    }

    //pega o caminho do arquivo salvavel
    private string GetPathFromSaveFile(string saveFile)
    {
        //feramenta do sistema que joga automatico LowLocal como local e arquivo
        return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }
}