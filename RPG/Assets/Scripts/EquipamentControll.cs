/*####Gerente do menu de equipamentos*/
using System.Collections.Generic;
using UnityEngine;

public class EquipamentControll : MonoBehaviour
{
    //referencias dos slots e lista
    [SerializeField] SlotEquip helmSlot = null;
    [SerializeField] SlotEquip chestSlot = null;
    [SerializeField] SlotEquip pantsSlot = null;
    [SerializeField] SlotEquip glovesSlot = null;
    [SerializeField] SlotEquip bootsSlot = null;
    [SerializeField] SlotEquip mainHSlot = null;
    [SerializeField] SlotEquip offHSlot = null;
    [SerializeField] List<SlotEquip> slot = new List<SlotEquip>();
    
    public SlotEquip selectedSlot = null;//slot selecionado

    public static EquipamentControll instance;//instancia para comunicação con inventario

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetUpList();//gera lista de slots
        this.gameObject.SetActive(false);//desativa slots
    }

    private void Update()
    {
        //ativa e desativa botões
        SetOptionsButtons(selectedSlot != null && selectedSlot.item != null);
    }

    //adiciona slots a lista
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

    //joga item equipado no slot correto
    public void Equip(ItemType type, PickupItens item)
    {
        //para cada slot
        foreach(SlotEquip equip in slot)
        {
            //se for do mesmo tipo do que o recebido
            if (equip.type == type)
            {
                equip.item = item;//item do slot recebe item
                break;
            }
        }
    }

    //desequipa e devolve item para inventorio
    public void Unequip()
    {
        WeaponConfig wDefault = GameObject.Find("Player").GetComponent<Combat>().weaponD;//referencia do desarme
        InventoryControll.instance.AddItem(selectedSlot.item);//joga item para inventario
        GameObject.Find("Player").GetComponent<Combat>().EquipWeapon(wDefault);//desequipa arma e coloca nao armado
        selectedSlot.item = null;//esvazia slot
        SetOptionsButtons();//desativa botoes
    }

    //ativa e desativa botoes
    private void SetOptionsButtons(bool active = false)
    {
        if (selectedSlot == null) return;
        if (selectedSlot.item == null) active = false;
        selectedSlot.buttonUnequip.SetActive(active);
        selectedSlot.opt.SetActive(active);
    }
}
