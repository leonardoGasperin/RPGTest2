/*#### gera uma indentidade para objetos o qual deseja ser salvo usando UIDD*/
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*namespace RPG.Saving
{
    // criar pasta Saving caso nao tenha feito ainda
}*/

[ExecuteAlways]//excecura a todo momento inclusive em runtime
public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string uniqueIdentifier = "";//espaco para identificador
    static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();//dicionario de identificadores usados

    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;//retorna o identificador do objeto
    }

    //captura dados para salvar
    public object CaptureState()
    {
        Dictionary<string, object> state = new Dictionary<string, object>();//cria um dicionario para recolher dados
        foreach (ISaveable saveable in GetComponents<ISaveable>())//para cada tipo ISaveable na lista de todos os ISaveable
        {
            state[saveable.GetType().ToString()] = saveable.CaptureState();//no index correspondente coloque as informacoes capturadas
        }
        return state;//retorna dicionario
    }

    //pega dados para restaurar
    public void RestoreState(object state)
    {
        Dictionary<string, object> stateDict = (Dictionary<string, object>)state;//cria um dicionario com os dados recebidos
        foreach (ISaveable saveable in GetComponents<ISaveable>())//para cada tipo ISaveable
        {
            string typeString = saveable.GetType().ToString();//procura os dados tipo saveable
            if (stateDict.ContainsKey(typeString))//checa se ele exite
            {
                saveable.RestoreState(stateDict[typeString]);//restoura todos os dados do tipo
            }
        }
    }
    //somente no editor do unity
#if UNITY_EDITOR
    private void Update()
    {
        if (Application.IsPlaying(gameObject)) return;//se estiver rodando sai
        if (string.IsNullOrEmpty(gameObject.scene.path)) return;//se estiver vaziu sai

        SerializedObject serializedObject = new SerializedObject(this);//cria um objeto serializado serializando este objeto
        SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");//e encontre e tribua um identificador unico

        if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))//se for vaziu ou se ja existe
        {
            property.stringValue = System.Guid.NewGuid().ToString();//cria outro
            serializedObject.ApplyModifiedProperties();//e modifica as propiedades dele
        }

        globalLookup[property.stringValue] = this;//e adiciona na lista
    }
#endif

    //checa se o identificador é unico
    private bool IsUnique(string candidate)
    {
        if (!globalLookup.ContainsKey(candidate)) return true;//se ele nao contem no dicionario

        if (globalLookup[candidate] == this) return true;//se ele é igual a ele

        if (globalLookup[candidate] == null)// se for vaziu
        {
            globalLookup.Remove(candidate);//remove
            return true;
        }

        if (globalLookup[candidate].GetUniqueIdentifier() != candidate)//se ja existir no dicionario
        {
            globalLookup.Remove(candidate);//remove
            return true;
        }

        //caso contrario retorna falso
        return false;
    }
}