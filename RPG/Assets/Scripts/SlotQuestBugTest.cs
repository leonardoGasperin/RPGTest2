using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SlotQuestBugTest : MonoBehaviour
{
    [SerializeField] Text title = null;
    [SerializeField] Text objectives = null;
    [SerializeField] Text currencies = null;
    [SerializeField] TesteQuestUIBug qSlot = null;

    // Update is called once per frame
    void Update()
    {
        ShowOnUI();
    }

    void ShowOnUI()
    {
        foreach(QuestSlot slot in qSlot.testSlotList)
        {
            if(slot.quest.title != null)
            {
                title.text = slot.quest.title;
                if (slot.quest.task.type == TaskType.Kill)
                    objectives.text = "Kill " + slot.quest.task.objName;
                if (slot.quest.task.type == TaskType.Gathering)
                    objectives.text = "Gathering " + slot.quest.task.objName;
                currencies.text = slot.quest.task.currentAmt + "/" + slot.quest.task.requiredAmt;
            }
        }
    }
}
