using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public PickupItens item;
    [SerializeField]protected Image itemIcon = null;
    [SerializeField] protected Text nameItem = null;
    [SerializeField] protected Image background = null;
    public GameObject opt = null;
    [SerializeField] protected Color onSelected;
    [SerializeField] protected Color unSelected;
    protected bool isSelected = false;

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
        SetUpSlot();
        isSelected = InventoryControll.instance.selectedSlot == this || EquipamentControll.instance.selectedSlot == this;
        if (!isSelected)
            opt.SetActive(false);
        background.color = isSelected ? onSelected : unSelected;
    }

    public virtual void SetUpSlot()
    {
        if (item != null)
        {
            SetActiveSlot(true);
            //itemIcon.sprite = item.icon.sprite;
            nameItem.text = item.name;
        }
        else
        {
            SetActiveSlot(false);
        }
    }

    public virtual void SetActiveSlot(bool isAct = true)
    {
        nameItem.gameObject.SetActive(isAct);
        itemIcon.gameObject.SetActive(isAct);
    }

    public virtual void OnClick()
    {
        
    }
}
