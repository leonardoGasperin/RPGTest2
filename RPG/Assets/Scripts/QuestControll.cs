using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestControll : MonoBehaviour
{
    [SerializeField] QuestSlot slotPrefab;
    [SerializeField] List<QuestSlot> questSlot;
    [SerializeField] Transform invGrid = null;
    [SerializeField] TesteQuestUIBug testSlot = null;

    //int indexList;

    public GameObject backGround = null;
    public static QuestControll instance;
    public QuestSlot selectedSlot;

    private void Start()
    {
        instance = this;
        //pega o slot prefab do prefab do inventario e o adiciona na primeira possição da lista
        slotPrefab.SetUpSlot();
        //para cada possição restante adiciona um novo slot prefab na grid
        for (int i = 0; i < 8; i++)
        {
            GameObject tempSlot = Instantiate(slotPrefab.gameObject);
            tempSlot.transform.SetParent(invGrid, false);
            questSlot.Add(tempSlot.GetComponent<QuestSlot>());
        }
        testSlot.GenerateList(questSlot);
        //e desativa a UI
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //selectedSlot = null;
    }

    private void OnDisable()
    {
        selectedSlot = null;
    }

    private void Update()
    {
        TryTest();
        //se selecionado um slot e nele contem item
        if (selectedSlot != null && selectedSlot.quest != null)
            selectedSlot.buttonAbandon.SetActive(true);
    }

    //adiciona um item ao slot
    public void AddQuest(Quest _quest)
    {
        QuestSlot empty = NextEmptySlot();//proximo slot vazio
        if (empty != null)
        {//o adiciona no proximo slot vaziu
            empty.quest = _quest;
            empty.quest.task = _quest.task;
        }
    }

    void TryTest()
    {
        foreach (QuestSlot slot in questSlot)
        {
            if (questSlot[questSlot.IndexOf(slot) + 8].quest != null)
            {
                slot.quest = questSlot[questSlot.IndexOf(slot) + 8].quest;
                slot.quest.task = questSlot[questSlot.IndexOf(slot) + 8].quest.task;
            }
            else
            {
                if (slot.quest != null)
                {
                    questSlot[questSlot.IndexOf(slot) + 8].quest = slot.quest;
                    questSlot[questSlot.IndexOf(slot) + 8].quest.task = slot.quest.task;
                }
            }
        }
    }

    //procura o primeiro slot vazio da lista
    private QuestSlot NextEmptySlot()
    {
        QuestSlot slotReturn = null;
        backGround.SetActive(true);
        //checa cada slot do inventario
        foreach (QuestSlot slot in questSlot)
        {
            //e caso nao tenha item
            if (slot.quest == null)
            {
                //a variavel de retorno recebe o slot
                slotReturn = slot;
                break;
            }
        }
        return slotReturn;
    }

    public void QuestChecker(string checker)
    {
        foreach (QuestSlot slot in questSlot)
        {
            if (slot != null && slot.quest != null && slot.quest.task.objName == checker)
            {
                if(slot.quest.task.type == TaskType.Gathering)
                {
                    //slot.InvQuestItem();
                }
                if (slot.quest.task.type == TaskType.Kill)
                { 
                    slot.AddKillAmount();
                    break;
                }
            }
        }
    }

    private void SetOffOptButtons()
    {
        selectedSlot.buttonAbandon.SetActive(false);
    }

    public void Abandom()
    {
        selectedSlot.quest.isActive = false;
        selectedSlot.quest = null;
        SetOffOptButtons();
    }
}
