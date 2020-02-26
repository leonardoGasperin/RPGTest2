/*####Administra itens pegaveis*/
using UnityEngine;
using UnityEngine.UI;



public class PickupItens : MonoBehaviour, IRayCastable
{
    [SerializeField] WeaponConfig pickableWeapon = null;//item pegavel
    public GameObject itemPrefab = null;
    public string itemName = null;
    public Image icon = null;
    public InventoryControll inventory = null;
    [SerializeField] float HpRest = 20;
    [SerializeField] int amount = 0;
    [SerializeField] float respawnT = 5;//tempo de respawn
    public bool isRespawnable = false;//ativaodr do respawn

    int amt = 0;

    public ItemType type;
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
            PickUp();
        }
    }

    internal void AddItem(int recived = 1)
    {
        amt += recived;
    }
    internal void RemoveItem(int used = 1)
    {
        amt -= used;
    }

    public int InventoryAmount()
    {
        return amt;
    }

    public int GetAmount()
    {
        return amount;
    }

    //pega item
    private void PickUp()
    {
        inventory.AddItem(this);
        if (isRespawnable)
        {
            ShowAndHide showHide = GetComponentInParent<ShowAndHide>();//esconde o objeto do scenario
            showHide.Initiate(gameObject, respawnT);//volta depois de tanto tempo
        }
        else
            gameObject.SetActive(false);
        canGet = false;
    }

    public void Use()
    {
        if (pickableWeapon != null)
            GameObject.Find("Player").GetComponent<Combat>().EquipWeapon(pickableWeapon);//equipa a arma
        else
            GameObject.Find("Player").GetComponent<Health>().Heal(HpRest);//cura o HP
        AfterUse();
    }

    public virtual void AfterUse()
    {
        if (isStack)
        {
            RemoveItem();

            if (amt <= 0)
            {
                inventory.selectedSlot.item = null;
                amt = 0;
            }
        }
        else
            inventory.selectedSlot.item = null;
    }

    public void Dropped(GameObject item)
    {
        if (!item.activeSelf)
            Destroy(item);

        item.SetActive(true);
        item.gameObject.GetComponent<PickupItens>().enabled = true;

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
        return CursorType.combat;
    }
}
