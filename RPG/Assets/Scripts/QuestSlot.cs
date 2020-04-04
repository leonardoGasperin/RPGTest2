using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    [SerializeField] protected Text tittle;
    [SerializeField] protected Text objectives;
    [SerializeField] protected Text currencies;
    [SerializeField] protected Image background = null;
    public GameObject opt = null;//GameObject que contem os botões do slot nao vazio
    [SerializeField] protected Color onSelected;
    [SerializeField] protected Color unSelected;
    protected bool isSelected = false;//checador de slot selecionado

    public GameObject buttonAbandon = null;

    GameObject pl;
    bool end = false;

    public Quest quest;

    private void Start()
    {
        pl = GameObject.Find("Player");
        quest = null;
    }

    // Update is called once per frame
    void Update()
    {
        SetUpSlot();
        isSelected = QuestControll.instance.selectedSlot == this;
        opt.SetActive(isSelected && quest != null);//ativa botões do slot selecionado
        background.color = isSelected ? onSelected : unSelected;//check para cor do slot
        if (quest == null) return;
        if (!quest.isActive) return;
        if (!quest.task.IsReached())
        {
            TaskGoal(quest.task.type);
            return;
        }
        quest.task.endQuest = true;
    }

    public virtual void SetUpSlot()
    {
        //caso tenha um item no slot
        if (quest != null)
        {
            //ativa slot
            SetActiveSlot(true);
            tittle.text = quest.title;
            objectives.text = quest.description;
            currencies.text =  quest.task.currentAmt + "/" + quest.task.requiredAmt;
        }
        else
        {//caso contrario desativa slot
            SetActiveSlot(false);
        }
    }

    public virtual void SetActiveSlot(bool isAct = true)
    {
        tittle.gameObject.SetActive(isAct);
        objectives.gameObject.SetActive(isAct);
        currencies.gameObject.SetActive(isAct);
    }

    private void TaskGoal(TaskType type)
    {
        switch(type)
        {
            case TaskType.Kill:
                if (pl.GetComponent<Combat>().GetTarget() == null) break;
                if (pl.GetComponent<Combat>().GetTarget().gameObject.name == quest.task.objName)
                    pl.GetComponent<Combat>().isQuestMob = true;
                break;
            case TaskType.Gathering:
                pl.GetComponent<PlayerUIController>().inventoryUI.GetComponent<InventoryControll>().haveQuestItem = true;
                break;
            default:
                Debug.LogError("No Quest something get an error if you are getting a quest");
                break;
        }
    }

    public void AddKillAmount()
    {
        quest.task.EnemyKilled();
    }

    public void InvQuestItem(SlotInventory slotItem)
    {
        quest.task.GatheringSlot(slotItem.item.InventoryAmount() + 1);
    }

    public virtual void OnClick()
    {
        //se selecionado ativa as funçoes do slot
        if (isSelected)
        {
            QuestControll.instance.selectedSlot = null;
        }
        else
        {
            QuestControll.instance.selectedSlot = this;
        }
    }
}
