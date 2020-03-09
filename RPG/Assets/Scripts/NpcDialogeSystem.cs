using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogeSystem : MonoBehaviour
{
    public Text nameT;
    public Text senteces;

    public GameObject dialogueGUI;

    public Button accept;
    public Button dismiss;
    public Button complete;

    public float letterDelay = 0.1f;
    public float letterMult = 0.5f;

    public string thisName;
    public string[] dialoge;

    public bool letterIsMultiplied = false;
    public bool dialogueActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;
    public bool questAccept = false;
    public bool questReciving = false;
    public bool questEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        senteces.text = "";
    }

    public void InRange()
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);
        if (dialogueActive == true)
            dialogueGUI.SetActive(false);
    }

    public void NPCName(bool isQuest)
    {
        outOfRange = false;
        dialogueGUI.gameObject.SetActive(true);
        nameT.text = name;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue(isQuest));
            }
        }
        StartDialogue(isQuest);
    }

    private IEnumerator StartDialogue(bool _isQuest)
    {
        if (outOfRange == false)
        {
            int dialogueLength = dialoge.Length;
            int currentDialogueIndex = 0;

            while (currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialoge[currentDialogueIndex++]));

                    if (currentDialogueIndex >= dialogueLength)
                    {
                        //dialogueEnded = true;

                        if (_isQuest)
                        {
                            if (questReciving)
                            {
                                complete.gameObject.SetActive(true);
                                accept.gameObject.SetActive(false);
                                dismiss.gameObject.SetActive(false);
                            }
                            else
                            {
                                complete.gameObject.SetActive(false);
                                accept.gameObject.SetActive(true);
                                dismiss.gameObject.SetActive(true);
                            }
                        }
                    }
                }
                yield return 0;
            }

            while (true)
            {
                if (Input.GetKeyDown(KeyCode.E) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            if(!_isQuest)
                DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            senteces.text = "";

            while (currentCharacterIndex < stringLength)
            {
                senteces.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(KeyCode.E))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMult);

                        //if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                    /*else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }*/
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            senteces.text = "";
        }
    }

    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
        }
    }

    public void Accept()
    {
        questAccept = true;
        accept.gameObject.SetActive(false);
        dismiss.gameObject.SetActive(false);
        DropDialogue();
    }

    public void Dismiss()
    {
        questAccept = false;
        accept.gameObject.SetActive(false);
        dismiss.gameObject.SetActive(false);
        DropDialogue();
    }

    public void Completed()
    {
        questAccept = false;
        questEnd = true;
        complete.gameObject.SetActive(false);
        DropDialogue();
    }
}
