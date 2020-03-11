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
        ///TODO
        ///change from if to switch
        if (type == TaskType.Kill)
        {
            if (pl.GetComponent<Combat>().GetTarget() == null) return;
            if (pl.GetComponent<Combat>().GetTarget().gameObject.name == quest.task.objName)
                pl.GetComponent<Combat>().isQuestMob = true;
        }
    }

    public void AddAmount(string _name)
    {
        quest.task.EnemyKilled(_name);
    }

}
