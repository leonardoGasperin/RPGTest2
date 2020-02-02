/*####Arquivo de administraçao de armas*/
using UnityEngine;
using UnityEngine.Events;

/*namespace RPG.Combat
{
    //criar pasta Combat caso nao tenha
}*/

public class Weapon : MonoBehaviour
{
    [SerializeField] UnityEvent onHit = null;

    public void OnHit()
    {
        print("-->>" + gameObject.name);
        onHit.Invoke();
    }
    
}
