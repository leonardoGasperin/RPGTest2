using UnityEngine;
using UnityEngine.UI;

public class NpcQuest : Npc
{
    [SerializeField] GameObject pl;
    public Quest quest;
    bool isRecivied = false;

    Text expR;
    Text moneyR;

    public override void Update()
    {
        /*if (!quest.isActive)
            Debug.LogError("note active quest" + name);*/

        if (dialoge.questAccept && !quest.task.endQuest && !isRecivied)
        { AcceptdQuest(); Debug.LogError("note active quest" + name); }
        else if (quest.isActive && quest.task.endQuest)
            OverQuest();
        
        base.Update();
    }

    public override void PressToInteract(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInt)
        {
            dialoge.name = thisName;
            if(!quest.isActive && !quest.task.endQuest)
            {
                dialoge.dialoge = sentences;
                dialoge.NPCName(true);
            }
            else if(quest.isActive && !quest.task.endQuest)
            {
                dialoge.dialoge = null;
                dialoge.dialoge = midQuestSentence;
                dialoge.NPCName(false);
            }
            else if(quest.isActive && quest.task.endQuest)
                dialoge.NPCName(true);
            if(quest.isComplet)
                dialoge.NPCName(false);
        }
    }

    public void AcceptdQuest()
    {
        pl.GetComponent<PlayerUIController>().questUI.AddQuest(quest);
        isRecivied = true;
        quest.isActive = true;
    }

    public void OverQuest()
    {
        dialoge.dialoge = endQuestSentence;
        if (dialoge.questEnd)
        {
            quest.Complet();
            Reward();
            dialoge.questReciving = false;
        }
        else
        {
            dialoge.questReciving = true;
        }
    }
    private void Reward()
    {
        pl.GetComponent<PlayerUIController>().inventoryUI.GetComponent<InventoryControll>().money += quest.moneyReward;
        pl.GetComponent<XP>().GainXP(quest.expReward);
        if (quest.task.type == TaskType.Gathering)
        { 
            pl.GetComponent<PlayerUIController>().inventoryUI.GetComponent<InventoryControll>().haveQuestItem = false;
            pl.GetComponent<PlayerUIController>().inventoryUI.GetComponent<InventoryControll>().clearTask = true;
            //if(quest.task.currentAmt == 0)
                //pl.GetComponent<PlayerUIController>().QuestUI.quest = null;
            return;
        }
        //pl.GetComponent<PlayerUIController>().QuestUI.quest = null;
    }
}
