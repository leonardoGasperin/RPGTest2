using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteQuestUIBug : MonoBehaviour
{
    public List<QuestSlot> testSlotList;
    [SerializeField] GameObject slotPrefab;

    public void GenerateList(List<QuestSlot> slots)
    {
        testSlotList = slots;
        for (int i = 0; i < 8; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab);
            tempSlot.transform.SetParent(this.gameObject.transform, false);
            testSlotList.Add(tempSlot.GetComponent<QuestSlot>());
        }
    }
}
