[System.Serializable]
public class Quest
{
    public string title;
    [UnityEngine.TextArea(5, 10)]
    public string description;
    public int expReward;
    public int moneyReward;
    public bool isActive;
    public bool isComplet;
    public QuestTask task;

    public void Complet()
    {
        isComplet = true;
        isActive = false;
    }
}
