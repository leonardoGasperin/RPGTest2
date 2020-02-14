/*#### Faz aparecer a UI do dano*/
using UnityEngine;

public class UICombatSpawner : MonoBehaviour
{
    [SerializeField] DamageTxt dmgTxt = null;

    public void Spawn(float dmgTaken)
    {
        DamageTxt txt = Instantiate(dmgTxt, transform);
        txt.SetValue(dmgTaken);
    }
}
