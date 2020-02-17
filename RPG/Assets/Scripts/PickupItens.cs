/*####Administra itens pegaveis*/
using System;
using UnityEngine;
using UnityEngine.UI;

public class PickupItens : MonoBehaviour, IRayCastable
{
    [SerializeField] WeaponConfig pickableWeapon = null;//item pegavel
    public Image icon = null;
    [SerializeField] InventoryControll inventory = null;
    [SerializeField] float HpRest = 20;
    [SerializeField] int amount = 0;
    [SerializeField] float respawnT = 5;//tempo de respawn
    [SerializeField] bool isRespawnable = false;//ativaodr do respawn

    public bool isStack = false;
    
    bool canGet = false;
    
    public bool isWeapon
    {
        get { return pickableWeapon == null ? false : true; }
    }

    private void Update()
    {
        if (GetInRange(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform) && canGet)
        {//se foi clicado pega item
            PickUp(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    internal void AddItem(int recived = 1)
    {
        amount += recived;
    }
    internal void RemoveItem(int used = 1)
    {
        amount -= used;
        if (amount <= 0)
            Destroy(gameObject);
    }

    public int GetAmount()
    {
        return amount;
    }

    //pega item
    private void PickUp(GameObject other)
    {
        //se for alguma arma
        if (pickableWeapon != null)
        {
            inventory.AddItem(this);
            //other.GetComponent<Combat>().EquipWeapon(pickableWeapon);//equipa a arma
            ShowAndHide showHide = GetComponentInParent<ShowAndHide>();//esconde o objeto do scenario
            showHide.Initiate(gameObject, respawnT);//volta depois de tanto tempo
        }
        if (HpRest > 0)//se o item tiver HpRest maior que 0
        {
            inventory.AddItem(this);
            //other.GetComponent<Health>().Heal(HpRest);//cura o HP
            ShowAndHide showHide = GetComponentInParent<ShowAndHide>();//esconde o objeto do scenario
            showHide.Initiate(gameObject, respawnT);//volta depois de tanto tempo
        }
        canGet = false;
    }

    public void Use()
    {

    }
    public virtual void AfterUse()
    {

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
            return true;
        }
        return false;
    }
    public CursorType GetCursorType()
    {
        return CursorType.Pickup;
    }
}
