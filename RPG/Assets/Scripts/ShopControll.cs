/*####Este script tem a função de gerenciar o comportamento do Inventario do jogado*/
using System.Collections.Generic;
using UnityEngine;

public class ShopControll : MonoBehaviour
{
    [SerializeField] List<SlotShop> invSlot;//lista de slot
    [SerializeField] SlotShop slotPrefab = null;//prefab do slot
    [SerializeField] Transform invGrid = null;//referencia da grip do panel

    public static ShopControll instance;//instancia da classe para uso em locais co relativo como Equipamento
    public SlotShop selectedShop;

    //preparação e criação do inventario
    private void Start()
    {
        instance = this;
        //pega o slot prefab do prefab do inventario e o adiciona na primeira possição da lista
        slotPrefab.SetUpSlot();
        slotPrefab.transform.SetParent(invGrid, false);
        invSlot.Add(slotPrefab.GetComponent<SlotShop>());
        //para cada possição restante adiciona um novo slot prefab na grid
        for (int i = 1; i < 20; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(invGrid, false);
            invSlot.Add(tempSlot.GetComponent<SlotShop>());
        }
        //e desativa a UI
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        selectedShop = null;
    }

    private void OnDisable()
    {
        selectedShop = null;
    }

    private void Update()
    {
        //se selecionado um slot e nele contem item
        if (selectedShop != null && selectedShop.item != null)
        {
            //ativa os botoes de função
            selectedShop.buy.SetActive(true);
        }
    }

    //adiciona um item ao slot
    public void AddItem(PickupItens item)
    {
        SlotShop empty = NextEmptySlot();//proximo slot vazio
        if (empty != null)
        {//o adiciona no proximo slot vaziu
            empty.item = item;
        }
    }

    //procura o primeiro slot vazio da lista
    private SlotShop NextEmptySlot()
    {
        SlotShop slotReturn = null;

        //checa cada slot do inventario
        foreach (SlotShop slot in invSlot)
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

    //desativa os botoes do slot
    private void SetOffOptButtons()
    {
        selectedShop.buy.SetActive(false);
    }

    public void Buy()
    {
        if(InventoryControll.instance.money >= selectedShop.item.ItemPrice())
        {
            InventoryControll.instance.AddItem(selectedShop.item);
            InventoryControll.instance.money -= selectedShop.item.ItemPrice();
            SetOffOptButtons();
            selectedShop = null;
        }
    }
}
