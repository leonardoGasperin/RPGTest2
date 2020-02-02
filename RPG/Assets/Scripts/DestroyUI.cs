/*#### Destroi UI's nao mais necessarias como por exemplo a barra de vida do inimigo*/
using UnityEngine;

public class DestroyUI : MonoBehaviour
{
    [SerializeField] GameObject target = null;//referencia do alvo a ser destruido

    //destroi o objeto
    public void DestroyTxt()
    {
        Destroy(target);
    }
}
