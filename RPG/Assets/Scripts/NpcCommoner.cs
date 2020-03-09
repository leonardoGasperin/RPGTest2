using UnityEngine;

public class NpcCommoner : Npc
{
    public override void PressToInteract(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInt)
        {
            isInt = true;
            dialoge.name = thisName;
            dialoge.dialoge = sentences;
            dialoge.NPCName(false);
            isInt = dialoge.dialogueEnded;
        }
    }
}
