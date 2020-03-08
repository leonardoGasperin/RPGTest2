using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] GameObject npcInt = null;
    [SerializeField] GameObject shop = null;
    [SerializeField] PickupItens[] itens = null;

    int i = 0;
    bool isTrig = false;
    bool isInt = false;

    void Update()
    {
        if(i != itens.Length)
        {
            shop.GetComponent<ShopControll>().AddItem(itens[i]);
            i++;
        }
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
            npcInt.SetActive(false);
        }
    }

    private void PressToInteract(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInt)
        {
            transform.LookAt(other.transform);
            isInt = true;
            shop.SetActive(isInt);
            npcInt.SetActive(!isInt);
            other.GetComponent<PlayerUIController>().shopping = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isInt)
        {
            isInt = false;
            shop.SetActive(isInt);
            npcInt.SetActive(!isInt);
            other.GetComponent<PlayerUIController>().shopping = false;
        }
    }
}
