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
        quest.isActive = dialoge.questAccept;

        if (quest.isActive && !quest.task.endQuest)
            AcceptdQuest();


        base.Update();
    }

    public override void PressToInteract(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInt)
        {
            //isInt = true;
            dialoge.name = thisName;
            if (quest.isActive && quest.task.endQuest)
            {
                OverQuest();
                dialoge.NPCName(true);
            }
            else
            {
                dialoge.dialoge = sentences;
                dialoge.NPCName(true);
            }
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
            dialoge.questReciving = false;
            quest.Complet();
        }
        else
        {
            Reward();
            dialoge.questReciving = true;
        }
    }
    private void Reward()
    {
        pl.GetComponent<PlayerUIController>().inventoryUI.GetComponent<InventoryControll>().money += quest.moneyReward;
        pl.GetComponent<XP>().GainXP(quest.expReward);
        quest = null;
    }
}
