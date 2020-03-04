/*#### administra o alvo*/
using UnityEngine;

/*namespace RPG.Combat
{
    //criar pasta Combat caso nao tenha
}*/

[RequireComponent(typeof(Health))]
public class CombatTarget : MonoBehaviour, IRayCastable
{
    CursorType type;

    //checa se eh atacavel e inicia ataque
    public bool HandleRaycast(PlayerController callingControl)
    {
        if (!callingControl.GetComponent<Combat>().CanAtack(gameObject)) return false;//se nao poder se atacado
        if (this.tag == "Enemy")
        {
            type = CursorType.combat;
            if (Input.GetMouseButton(0)/* && type == CursorType.combat*/)//se foi clicado
            {
                Debug.LogError(callingControl.GetComponent<Combat>().gameObject.name);
                callingControl.GetComponent<Combat>().Attack(gameObject.transform);//inicia o ataque ao target na classe Combat passando t como referencia
            }
            return true;//caso tudo certo com a referencia retorna verdadeiro a linha 27 }
        }
        if (this.tag == "Npc")
        {
            type = CursorType.Npc;
            if (Input.GetMouseButton(0))
            callingControl.GetComponent<Move>().MoveTo(this.transform.position - new Vector3(1, 1, 1), 1, false);

            return true;
        }

        return true;
    }

    public CursorType GetCursorType()
    {
        return type;
    }
}
