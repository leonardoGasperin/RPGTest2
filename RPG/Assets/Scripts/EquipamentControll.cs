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

    public EquipamentControll instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetUpList();
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

    public void EquipUnequip(ItemType type, PickupItens item)
    {
        foreach(SlotEquip equip in slot)
        {
            if (equip.type == type)
            {
                /*if(!equip.isActiveAndEnabled)
                {
                    Debug.LogError("fdsfs");
                    equip.enabled = true;
                    equip.item = item;
                    equip.enabled = false;
                    break;
                }*/
                equip.item = item;
                break;
            }
        }
    }
}
