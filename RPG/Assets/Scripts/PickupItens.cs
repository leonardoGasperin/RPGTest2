/*####Administra itens pegaveis*/
using UnityEngine;
using UnityEngine.UI;

public class PickupItens : MonoBehaviour, IRayCastable
{
    [SerializeField] WeaponConfig pickableWeapon = null;//item pegavel
    public GameObject itemPrefab = null;//referencia do item para drop
    public string itemName = null;//nome do item
    public Image icon = null;//icone do item
    public InventoryControll inventory = null;//referencia do inventario
    [SerializeField] float HpRest = 20;//variavel para amontante de recureçao ou bonus de atributos(poções)
    [SerializeField] int amount = 0;//amontante de item stackable
    [SerializeField] float respawnT = 5;//tempo de respawn
    public bool isRespawnable = false;//ativaodr do respawn
    public ItemType type;//tipo do item
    public bool isStack = false;//check de item stackable

    int amt = 0;//amontante do indicador
    bool canGet = false;//permitidor para pegar item
    
    //checa se é um equip ou nao
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

    //referencia da quantia do item pego
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
        //adiciona item no inventario
        inventory.AddItem(this);
        //se for respawnable
        if (isRespawnable)
        {
            ShowAndHide showHide = GetComponentInParent<ShowAndHide>();//esconde o objeto do scenario
            showHide.Initiate(gameObject, respawnT);//volta depois de tanto tempo
        }
        else//caso contrario item some
            gameObject.SetActive(false);
        canGet = false;//e nao pode ser mais pego
    }

    //usar item
    public void Use()
    {
        //se for equipe
        if (pickableWeapon != null)
            GameObject.Find("Player").GetComponent<Combat>().EquipWeapon(pickableWeapon);//equipa a arma
        else
            GameObject.Find("Player").GetComponent<Health>().Heal(HpRest);//cura o HP
        AfterUse();
    }

    //finaliza o uso do item
    public virtual void AfterUse()
    {
        //se for stackable
        if (isStack)
        {
            RemoveItem();

            //se nao tiver mais o item
            if (amt <= 0)
            {
                //esvazia o slot
                inventory.selectedSlot.item = null;
                amt = 0;
            }
        }
        else
            inventory.selectedSlot.item = null;//esvazia slot
    }

    public void Dropped(GameObject item)
    {
        //checa se o item nao é o original que esta na cena
        if (!item.activeSelf)
            Destroy(item);//caso sim destroy o original

        item.SetActive(true);//ativa o novo
        item.gameObject.GetComponent<PickupItens>().enabled = true;//e ativa o script

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
