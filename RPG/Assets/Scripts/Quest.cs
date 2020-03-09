﻿/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/

[System.Serializable]
public class Quest
{
    public string title;
    [UnityEngine.TextArea(5, 10)]
    public string description;
    public int expReward;
    public int moneyReward;
    public bool isActive;
    public QuestTask task;

    public void Complet()
    {
        isActive = false;
    }
}
