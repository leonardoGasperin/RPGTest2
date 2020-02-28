/*####Este scrip é o gerador basico de slot, tudo que um slot tem ou precisa tera aqui
      configuração, ativação e click*/
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public PickupItens item;//referencia do item contido no slot
    [SerializeField]protected Image itemIcon = null;//icone do item
    [SerializeField] protected Text nameItem = null;//nome do item
    [SerializeField] protected Image background = null;
    public GameObject opt = null;//GameObject que contem os botões do slot nao vazio
    [SerializeField] protected Color onSelected;
    [SerializeField] protected Color unSelected;
    protected bool isSelected = false;//checador de slot selecionado

    // Start is called before the first frame update
    protected void Start()
    {
        SetUpSlot();
    }

    protected void OnDisable()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        SetUpSlot();//sempre atualiza os slot quando UI aberta
        isSelected = InventoryControll.instance.selectedSlot == this || EquipamentControll.instance.selectedSlot == this;//checa se o slot esta selecionado
        opt.SetActive(isSelected);//ativa botões do slot selecionado

        background.color = isSelected ? onSelected : unSelected;//check para cor do slot
    }

    //configura slot
    public virtual void SetUpSlot()
    {
        //caso tenha um item no slot
        if (item != null)
        {
            //ativa slot
            SetActiveSlot(true);
            //itemIcon.sprite = item.icon.sprite;//atribui icone do item
            nameItem.text = item.itemName;//atribui nome do item
        }
        else
        {//caso contrario desativa slot
            SetActiveSlot(false);
        }
    }

    //ativa slot
    public virtual void SetActiveSlot(bool isAct = true)
    {
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    //quando clicado
    public virtual void OnClick()
    {
        
    }
}
