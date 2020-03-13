/*####Este script tem a função de gerenciar o comportamento do Inventario do jogado*/
using System.Collections.Generic;
using UnityEngine;

public class InventoryControll : MonoBehaviour
{
    [SerializeField] GameObject pl = null;//referencia do jogador
    [SerializeField] List<SlotInventory> invSlot;//lista de slot
    [SerializeField] SlotInventory slotPrefab = null;//prefab do slot
    [SerializeField] Transform invGrid = null;//referencia da grip do panel

    public static InventoryControll instance;//instancia da classe para uso em locais co relativo como Equipamento
    public SlotInventory selectedSlot;//slot selecionado
    public int money = 0;
    public bool isShop;
    public bool haveQuestItem = false;
    public bool questItemComplet = false;
    public bool clearTask = false;

    //preparação e criação do inventario
    private void Start()
    {
        instance = this;
        //pega o slot prefab do prefab do inventario e o adiciona na primeira possição da lista
        slotPrefab.SetUpSlot();
        slotPrefab.transform.SetParent(invGrid, false);
        invSlot.Add(slotPrefab.GetComponent<SlotInventory>());
        //para cada possição restante adiciona um novo slot prefab na grid
        for (int i = 1; i < 20; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(invGrid, false);
            invSlot.Add(tempSlot.GetComponent<SlotInventory>());
        }
        //e desativa a UI
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        selectedSlot = null;
    }

    private void OnDisable()
    {
        selectedSlot = null;
    }

    private void Update()
    {
        //se selecionado um slot e nele contem item
        if (selectedSlot != null && selectedSlot.item != null)
        {
            //ativa os botoes de função
            if (pl.GetComponent<PlayerUIController>().shopping)
            {
                selectedSlot.buttonSell.SetActive(true);
            }
            else
            {
                selectedSlot.buttonSell.SetActive(false);
                if (selectedSlot.item.isWeapon)
                    selectedSlot.buttonEquip.SetActive(true);
                else//caso contario
                    selectedSlot.buttonUse.SetActive(true);

                selectedSlot.buttonDrop.SetActive(true);
            }
        }
    }

    //adiciona um item ao slot
    public void AddItem(PickupItens item)
    {
        bool found = false;//procurador de item stackable
        SlotInventory empty = NextEmptySlot();//proximo slot vazio

        //se o item a adicionar vor stackable
        if (item.isStack)
        {
            //checa em todos os slots
            foreach (SlotInventory slot in invSlot)
            {
                //se ha um slot ocupado com o mesmo item
                if (slot.item != null && slot.item.itemName == item.itemName)
                {//caso sim almenta a quantia de item
                    
                    slot.item.AddItem();
                    found = true;//e diz que o item foi encontrado
                }
            }

            //caso o item nao foi encontrado e o slot for vaziu
            if (!found && empty != null)
            {//adiciona o item
                if(pl.GetComponent<PlayerUIController>().shopping)
                item.AddItem(1, true);
                empty.item = item;
            }
        }//caso o item nao for stackable
        else if (empty != null)
        {//o adiciona no proximo slot vaziu
            empty.item = item;
        }
    }

    //procura o primeiro slot vazio da lista
    private SlotInventory NextEmptySlot()
    {
        SlotInventory slotReturn = null;
        //checa cada slot do inventario
        foreach (SlotInventory slot in invSlot)
        {
            //e caso nao tenha item
            if (slot.item == null)
            {
                //a variavel de retorno recebe o slot
                slotReturn = slot;
                break;
            }
        }
        return slotReturn;
    }

    public void CheckForQuest()
    {
        foreach(SlotInventory slot in invSlot)
        {
            if (slot.item != null) 
            {
                if(clearTask)
                {
                    if (slot.item.itemName == pl.GetComponent<PlayerUIController>().QuestUI.quest.task.objName)
                    {
                        slot.item.QuestRemover(pl.GetComponent<PlayerUIController>().QuestUI.quest.task.requiredAmt);
                        if (slot.item.InventoryAmount() + 1 <= 0)
                        {  slot.item = null; }
                        clearTask = false;

                        break;
                    }
                }
                if (slot.item.itemName == pl.GetComponent<PlayerUIController>().QuestUI.quest.task.objName)
                        pl.GetComponent<PlayerUIController>().QuestUI.InvQuestItem(slot);
                
            }
            
        }
    }

    //desativa os botoes do slot
    private void SetOffOptButtons()
    {
        selectedSlot.buttonUse.SetActive(false);
        selectedSlot.buttonEquip.SetActive(false);
        selectedSlot.buttonDrop.SetActive(false);
    }

    //usa um item
    public void UseItem()
    {
        selectedSlot.item.Use();
        selectedSlot.SetUpSlot();
        SetOffOptButtons();
    }

    //equipa um item
    public void EquipItem()
    {
        EquipamentControll.instance.Equip(selectedSlot.item.type, selectedSlot.item);
        selectedSlot.item.Use();
        selectedSlot.SetUpSlot();
        SetOffOptButtons();
        selectedSlot = null;
    }

    public void SellItem()
    {
        int sellPrice = selectedSlot.item.ItemPrice() / 2;
        money += sellPrice;
        selectedSlot.item.AfterUse();
        selectedSlot.SetUpSlot();
        SetOffOptButtons();
        selectedSlot = null;
    }

    //joga item fora
    public void DropItem()
    {
        //referencia do item
        GameObject drop = selectedSlot.item.itemPrefab;

        //checa se o item em questao tem a referencia do inventario, caso nao o adiciona
        if (drop.GetComponent<PickupItens>().inventory == null)
            drop.GetComponent<PickupItens>().inventory = GameObject.Find("UIInventory").GetComponent<InventoryControll>();

        //dropa o item e o posiciona no mapa perto do personagem jogador, e o cria dentro do objeto de controles de itens dropados na cena
        drop.GetComponent<PickupItens>().Dropped(drop);
        ShowAndHide tr = FindObjectOfType(typeof(ShowAndHide)) as ShowAndHide;
        Instantiate(drop, pl.transform.position + new Vector3(1, 0, 1), drop.transform.rotation, tr.gameObject.transform);

        //e apos isso finaliza o uso do item
        selectedSlot.item.AfterUse();
        SetOffOptButtons();
        selectedSlot = null;
    }
}
