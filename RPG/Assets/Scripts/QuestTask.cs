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

    public void EnemyKilled()
    {
        if (endQuest) return;
        currentAmt++;
    }

    public void GatheringSlot(int amt)
    {
        if (endQuest) return;
        currentAmt = amt;
    }
}
