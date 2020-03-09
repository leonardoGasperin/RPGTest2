using UnityEngine;

public class Npc : MonoBehaviour
{
    public NpcDialogeSystem dialoge = null;
    public GameObject npcInt = null;
    public string thisName;
    public bool isInt = false;
    [TextArea(5, 10)]
    public string[] sentences;
    [TextArea(5, 10)]
    public string[] endQuestSentence;

    bool isTrig = false;

    public virtual void Update()
    {
        if (!isTrig) return;
        PressToInteract(GameObject.Find("Player"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrig = true;
            npcInt.SetActive(!isInt);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isTrig = false;
            dialoge.OutOfRange();
            npcInt.SetActive(false);
        }
    }

    public virtual void PressToInteract(GameObject other) { }
}
