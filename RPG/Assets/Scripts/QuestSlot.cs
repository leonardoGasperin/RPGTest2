using UnityEngine;

public class QuestSlot : MonoBehaviour
{
    GameObject pl;
    bool end = false;

    public Quest quest;

    private void Start()
    {
        pl = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (quest == null) return;
        if (!quest.isActive) return;
        if (!quest.task.IsReached())
        {
            TaskGoal(quest.task.type);
            return;
        }
        quest.task.endQuest = true;
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

    public void AddAmount()
    {
        quest.task.EnemyKilled();
    }

    public void InvQuestItem(SlotInventory slotItem)
    {
        quest.task.GatheringSlot(slotItem.item.InventoryAmount() + 1);
    }
}
