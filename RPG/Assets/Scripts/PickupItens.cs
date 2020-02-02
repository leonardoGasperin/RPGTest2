/*####Administra itens pegaveis*/
using UnityEngine;

public class PickupItens : MonoBehaviour, IRayCastable
{
    [SerializeField] WeaponConfig pickableWeapon = null;//item pegavel
    [SerializeField] float HpRest = 20;
    [SerializeField] float respawnT = 5;//tempo de respawn
    [SerializeField] bool isRespawnable = false;//ativaodr do respawn

    bool canGet = false;
    
    private void Update()
    {
        if (GetInRange(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform) && canGet)
        {//se foi clicado pega item
            PickUp(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    //pega item
    private void PickUp(GameObject other)
    {
        //se for alguma arma
        if (pickableWeapon != null)
        {
            other.GetComponent<Combat>().EquipWeapon(pickableWeapon);//equipa a arma
            ShowAndHide showHide = GetComponentInParent<ShowAndHide>();//esconde o objeto do scenario
            showHide.Initiate(gameObject, respawnT);//volta depois de tanto tempo
        }
        if (HpRest > 0)//se o item tiver HpRest maior que 0
        {
            other.GetComponent<Health>().Heal(HpRest);//cura o HP
            ShowAndHide showHide = GetComponentInParent<ShowAndHide>();//esconde o objeto do scenario
            showHide.Initiate(gameObject, respawnT);//volta depois de tanto tempo
        }
        canGet = false;
    }
    //retorna distancia da arma
    private bool GetInRange(Transform targetT)
    {
        return Vector3.Distance(targetT.position, transform.position) <= 0.5f;
    }

    //fazer ser interagivel
    public bool HandleRaycast(PlayerController caller)
    {
        if (Input.GetMouseButtonDown(0))
        {
            caller.gameObject.GetComponent<Move>().MoveTo(transform.position, 1, false);
            canGet = true;
        }
        return true;
    }
    public CursorType GetCursorType()
    {
        return CursorType.Pickup;
    }
}
