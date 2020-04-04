using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestControll : MonoBehaviour
{
    [SerializeField] QuestSlot slotPrefab;
    [SerializeField] List<QuestSlot> questSlot;
    [SerializeField] Transform invGrid = null;
    [SerializeField] TesteQuestUIBug testSlot = null;
    [SerializeField] Text title = null;
    [SerializeField] Text contents = null;
    [SerializeField] Text rewards = null;

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
        selectedSlot = null;
    }

    private void OnDisable()
    {
        selectedSlot = null;
    }

    private void Update()
    {
        //se selecionado um slot e nele contem item
        if (selectedSlot != null && selectedSlot.quest != null)
        {
            selectedSlot.buttonAbandon.SetActive(true);

            title.text = selectedSlot.quest.title;
            if (selectedSlot.quest.task.type == TaskType.Kill)
                contents.text = "Kill " + selectedSlot.quest.task.requiredAmt + " " + selectedSlot.quest.task.objName;
            else
                contents.text = "Get " + selectedSlot.quest.task.requiredAmt + " " + selectedSlot.quest.task.objName;
            rewards.text = "EXP:" + selectedSlot.quest.expReward + "    Gold:" + selectedSlot.quest.moneyReward;
            
            UpdateQuest();
            return;

        }
        title.text = null;
        contents.text = null;
        rewards.text = null;
        UpdateQuest();
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

    void UpdateQuest()
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

    public void QuestChecker(string checker, SlotInventory itemSlot)
    {
        foreach (QuestSlot slot in questSlot)
        {
            if (slot != null && slot.quest != null && slot.quest.task.objName == checker)
            {
                if(slot.quest.task.type == TaskType.Gathering && itemSlot != null)
                {
                    slot.InvQuestItem(itemSlot);
                }
                if (slot.quest.task.type == TaskType.Kill)
                { 
                    slot.AddKillAmount();
                    break;
                }
            }
        }
    }

    public int CheckQuestAmount(string chk)
    {
        foreach(QuestSlot slot in questSlot)
        {
            if (slot.quest.task.objName == chk)
                return slot.quest.task.requiredAmt;
        }
        return 0;
    }

    public string QuestCheckForInv()
    {
        foreach(QuestSlot slot in questSlot)
        {
            if (slot.quest.task.type == TaskType.Gathering)
                return slot.quest.task.objName;
        }
        return null;
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
