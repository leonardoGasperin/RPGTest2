using UnityEngine;
using UnityEngine.UI;

public class SlotInventory : SlotManager
{

    [SerializeField] GameObject amountInd = null;
    [SerializeField] Text amount = null;
    
    public GameObject buttonEquip = null;
    public GameObject buttonUse = null;
    public GameObject buttonDrop = null;

    public override void SetUpSlot()
    {
        if (item != null)
        {
            SetActiveSlot(true);
            //itemIcon.sprite = item.icon.sprite;
            nameItem.text = item.itemName;

            if (item.isStack)
            {
                amount.text = item.InventoryAmount().ToString();
            }
            else
            {
                amountInd.SetActive(false);
            }
        }
        else
        {
            SetActiveSlot(false);
        }
    }

    public override void SetActiveSlot(bool isAct = true)
    {
        amountInd.SetActive(isAct);
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    public override void OnClick()
    {
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
