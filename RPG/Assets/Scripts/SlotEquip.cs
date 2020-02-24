using UnityEngine;

public class SlotEquip : SlotManager
{
    public GameObject buttonUnequip = null;
    public ItemType type;

    public override void SetActiveSlot(bool isAct = true)
    {
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    public override void OnClick()
    {
        if (isSelected)
        {
            EquipamentControll.instance.selectedSlot = null;
        }
        else
        {
            EquipamentControll.instance.selectedSlot = this;
        }
    }
}

/* Start is called before the first frame update
    void Start()
    {
        SetUpSlot();
    }

    private void OnDisable()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        SetUpSlot();
        isSelected = InventoryControll.instance.selectedSlot == this;
        if(!isSelected)
            opt.SetActive(false);
        background.color = isSelected ? onSelected : unSelected;
    }

    public void SetUpSlot()
    {
        if (item != null)
        {
            SetActiveSlot(true);
            //itemIcon.sprite = item.icon.sprite;
            nameItem.text = item.name;

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

    public void SetActiveSlot(bool isAct = true)
    {
        amountInd.SetActive(isAct);
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }*/
