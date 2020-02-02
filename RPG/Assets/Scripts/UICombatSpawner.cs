/*#### Faz aparecer a UI do dano*/
using UnityEngine;

public class UICombatSpawner : MonoBehaviour
{
    [SerializeField] DanageTxt dngTxt = null;

    public void Spawn(float dmgTaken)
    {
        DanageTxt txt = Instantiate<DanageTxt>(dngTxt, this.transform);
        txt.SetValue(dmgTaken);
    }
}
