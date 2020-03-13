/*####a partir de SlotManager.cs cria e gerencia os slot do inventario*/
using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : SlotManager
{

    [SerializeField] GameObject amountInd = null;//referencia do indicador de quantia
    [SerializeField] Text amount = null;//quantia
    
    //referencias dos botões do slot
    public GameObject buttonEquip = null;
    public GameObject buttonUse = null;
    public GameObject buttonDrop = null;
    public GameObject buttonSell = null;

    //configuração do botão
    public override void SetUpSlot()
    {
        //se tiver um item
        if (item != null)
        {
            //ativa slot
            SetActiveSlot(true);
            //itemIcon.sprite = item.icon.sprite;//atribui icone do item
            nameItem.text = item.itemName;//atribui nome do item

            //se o item for stackable
            if (item.isStack)
            {
                amount.text = (item.InventoryAmount() + 1).ToString();//motra a quantia no indicador
            }
            else
            {//caso contrario desativa indicador
                amountInd.SetActive(false);
            }
        }
        else
        {//caso contrario desativa slot
            SetActiveSlot(false);
        }
    }

    //ativa slot
    public override void SetActiveSlot(bool isAct = true)
    {
        amountInd.SetActive(isAct);
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    //ao clicar
    public override void OnClick()
    {
        //se selecionado ativa as funçoes do slot
        if (isSelected)
        {
            InventoryControll.instance.selectedSlot = null;
        }
        else
        {
            InventoryControll.instance.selectedSlot = this;
        }
    }
}
