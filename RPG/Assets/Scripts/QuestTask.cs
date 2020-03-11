using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTask
{
    public TaskType type;

    public int requiredAmt;
    public int currentAmt;
    public string objName;
    public bool endQuest = false;

    public bool IsReached()
    {
        return (currentAmt >= requiredAmt);
    }

    public void EnemyKilled(string _name)
    {
        if (_name != objName || endQuest) return;
        currentAmt++;

    }
}
