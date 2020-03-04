using UnityEngine;
using UnityEngine.UI;

public class SlotShop : SlotManager
{
    [SerializeField] GameObject pricetInd = null;//referencia do indicador de quantia
    [SerializeField] Text price = null;//quantia

    public GameObject buy = null;

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
                price.text = item.ItemPrice().ToString();//motra a quantia no indicador
            }
            else
            {//caso contrario desativa indicador
                pricetInd.SetActive(false);
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
        pricetInd.SetActive(isAct);
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    //ao clicar
    public override void OnClick()
    {
        //se selecionado ativa as funçoes do slot
        if (isSelected)
        {
            InventoryControll.instance.selectedShop = null;
        }
        else
        {
            InventoryControll.instance.selectedShop = this;
        }
    }
}
