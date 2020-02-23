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
    */
}
