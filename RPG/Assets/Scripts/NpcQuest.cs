using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcQuest : Npc
{
    [SerializeField] GameObject pl;
    public Quest quest;

    Text expR;
    Text moneyR;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Update()
    {
        if(!quest.isActive)
            quest.isActive = dialoge.questAccept;

        if (quest.isActive && !quest.task.endQuest)
            AcceptdQuest();
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
        pl.GetComponent<PlayerUIController>().QuestUI.quest = quest;
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
        pl.GetComponent<PlayerUIController>().QuestUI.quest = null;
    }
}
