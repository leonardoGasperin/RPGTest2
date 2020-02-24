using System.Collections.Generic;
using UnityEngine;

public class EquipamentControll : MonoBehaviour
{
    [SerializeField] SlotEquip helmSlot = null;
    [SerializeField] SlotEquip chestSlot = null;
    [SerializeField] SlotEquip pantsSlot = null;
    [SerializeField] SlotEquip glovesSlot = null;
    [SerializeField] SlotEquip bootsSlot = null;
    [SerializeField] SlotEquip mainHSlot = null;
    [SerializeField] SlotEquip offHSlot = null;
    [SerializeField] List<SlotEquip> slot = new List<SlotEquip>();
    
    public SlotEquip selectedSlot = null;

    public static EquipamentControll instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetUpList();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (selectedSlot != null && selectedSlot.item != null)
            SetOptionsButtons(true);
    }


    void SetUpList()
    {
        slot.Add(helmSlot);
        slot.Add(chestSlot);
        slot.Add(pantsSlot);
        slot.Add(glovesSlot);
        slot.Add(bootsSlot);
        slot.Add(mainHSlot);
        slot.Add(offHSlot);
    }

    public void Equip(ItemType type, PickupItens item)
    {
        foreach(SlotEquip equip in slot)
        {
            if (equip.type == type)
            {
                equip.item = item;
                break;
            }
        }
    }

    public void Unequip()
    {
        WeaponConfig wDefault = GameObject.Find("Player").GetComponent<Combat>().weaponD;
        InventoryControll.instance.AddItem(selectedSlot.item);
        GameObject.Find("Player").GetComponent<Combat>().EquipWeapon(wDefault);
        selectedSlot.item = null;
        SetOptionsButtons();
    }

    private void SetOptionsButtons(bool active = false)
    {
        selectedSlot.buttonUnequip.SetActive(active);
        selectedSlot.opt.SetActive(active);
    }
}
