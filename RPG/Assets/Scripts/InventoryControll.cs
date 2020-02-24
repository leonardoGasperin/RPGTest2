using System.Collections.Generic;
using UnityEngine;

public class InventoryControll : MonoBehaviour
{
    [SerializeField] GameObject pl = null;
    [SerializeField] List<SlotInventory> invSlot;
    [SerializeField] SlotInventory slotPrefab = null;
    [SerializeField] Transform invGrid = null;
    
    public static InventoryControll instance;
    public SlotInventory selectedSlot;

    private void Start()
    {
        instance = this;
        slotPrefab.SetUpSlot();
        slotPrefab.transform.SetParent(invGrid, false);
        invSlot.Add(slotPrefab.GetComponent<SlotInventory>());
        for (int i = 1; i < 20; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(invGrid, false);
            invSlot.Add(tempSlot.GetComponent<SlotInventory>());
        }
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
        if (selectedSlot != null && selectedSlot.item != null)
        {
            selectedSlot.opt.SetActive(true);
            if (selectedSlot.item.isWeapon)
                selectedSlot.buttonEquip.SetActive(true);
            else
                selectedSlot.buttonUse.SetActive(true);

            selectedSlot.buttonDrop.SetActive(true);
        }
    }

    public void AddItem(PickupItens item)
    {
        bool found = false;
        SlotInventory empty = NextEmptySlot();

        if (item.isStack)
        {
            foreach (SlotInventory slot in invSlot)
            {
                if (slot.item != null && slot.item.name == item.gameObject.name)
                {
                    slot.item.AddItem(slot.item.GetAmount());
                    found = true;
                }
            }

            if (!found && empty != null)
            {
                item.AddItem(item.GetAmount());
                empty.item = item;
            }
        }
        else if (empty != null)
        {
            empty.item = item;
        }
    }

    private SlotInventory NextEmptySlot()
    {
        SlotInventory slotReturn = null;
        foreach (SlotInventory slot in invSlot)
        {
            if (slot.item == null)
            {
                slotReturn = slot;
                break;
            }
        }
        return slotReturn;
    }

    private void SetOptionsButtons()
    {
        selectedSlot.buttonUse.SetActive(false);
        selectedSlot.buttonEquip.SetActive(false);
        selectedSlot.buttonDrop.SetActive(false);
        selectedSlot.opt.SetActive(false);

    }

    public void UseItem()
    {
        selectedSlot.item.Use();
        selectedSlot.SetUpSlot();
        SetOptionsButtons();
    }

    public void EquipItem()
    {
        {
            EquipamentControll.instance.Equip(selectedSlot.item.type, selectedSlot.item);
            selectedSlot.item.Use();
            selectedSlot.SetUpSlot();
            SetOptionsButtons();
        }

    }

    public void DropItem()
    {
        ShowAndHide tr = FindObjectOfType(typeof(ShowAndHide)) as ShowAndHide;
        selectedSlot.item.gameObject.SetActive(true);
        Instantiate(selectedSlot.item, pl.transform.position + new Vector3(1, 0, 1), Quaternion.identity, tr.gameObject.transform);
        selectedSlot.item.AfterUse();
        SetOptionsButtons();
    }

}
