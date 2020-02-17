using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotManager : MonoBehaviour
{
    public PickupItens item;
    [SerializeField] Image itemIcon = null;
    [SerializeField] Text nameItem = null;
    [SerializeField] GameObject amountInd = null;
    [SerializeField] Text amount = null;
    [SerializeField] Image background = null;

    public GameObject opt = null;
    public GameObject buttonEquip = null;
    public GameObject buttonUse = null;
    public GameObject buttonDrop = null;
    public Color onSelected;
    public Color unSelected;
    bool isSelected = false;

    // Start is called before the first frame update
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
                amount.text = item.GetAmount().ToString();
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
    }

    public void OnClick()
    {
        //isSelected = !isSelected;
        if (isSelected)
        {
            opt.SetActive(false);
            InventoryControll.instance.selectedSlot = null;
        }
        else
        {
            InventoryControll.instance.selectedSlot = this;
        }
    }
}
