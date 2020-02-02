/*#### mantem objetos que nao podem ser deletados duranto carregamentos*/
using UnityEngine;

/*namespace RPG.Manager
{
    //criar pasta Manager caso nao tenha
}*/

public class PersistObjtSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistObjetPrefabs;//referencia do prefab

    static bool hasSpawned = false;//checagem

    private void Awake()
    {
        if (hasSpawned) return;//se esta carregado sai

        SpawnPersistentObj();//carrega objeto
        hasSpawned = true;//checa
    }

    //carrega objeto
    private void SpawnPersistentObj()
    {
        GameObject persistObj = Instantiate(persistObjetPrefabs);//instancia objeto
        DontDestroyOnLoad(persistObj);//nao destroi no carregamento
    }
}
