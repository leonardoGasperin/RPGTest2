/*####a partir do Slotmanager.cs cria e gerencia os slot do menu de equipamentos*/
using UnityEngine;

public class SlotEquip : SlotManager
{
    public GameObject buttonUnequip = null;//botão de desequipar item
    public ItemType type;//tipo de item para tipo de slot

    //ativa slot
    public override void SetActiveSlot(bool isAct = true)
    {//motra nome e icone
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    //quando clicado
    public override void OnClick()
    {
        //se selecionado ativa as funçoes do slot
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
