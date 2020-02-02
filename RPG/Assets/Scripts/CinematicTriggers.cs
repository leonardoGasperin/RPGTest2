/*trigger para cinematicas*/
using UnityEngine;
using UnityEngine.Playables;

/*namespace RPG.Cinematic
{
    //criar pasta cinematics caso nao tenha
}*/

public class CinematicTriggers : MonoBehaviour
{
    [SerializeField] bool trigged = false;//check in off

    private void OnTriggerEnter(Collider other)
    {
        if (!trigged && other.tag == "Player")
        {
            trigged = true;//ativo
            GetComponent<PlayableDirector>().Play();//play cinematic
        }
    }

}
