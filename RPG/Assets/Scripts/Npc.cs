using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] GameObject npcInt = null;
    [SerializeField] GameObject shop = null;
    [SerializeField] PickupItens[] itens = null;
    
    bool isTrig = false;
    bool isInt = false;

    private void Start()
    {
        if (itens != null)
        {
            foreach (PickupItens seller in itens)
            {
                shop.GetComponent<InventoryControll>().AddItem(seller);
            }
        }
    }

    void Update()
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
            npcInt.SetActive(false);
        }
    }

    private void PressToInteract(GameObject other)
    {
        other.transform.LookAt(this.transform);
        if (Input.GetKeyDown(KeyCode.E) && !isInt)
        {
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
