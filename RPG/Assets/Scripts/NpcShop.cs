using UnityEngine;

public class NpcShop : Npc
{
    [SerializeField] GameObject shop = null;
    [SerializeField] PickupItens[] itens = null;

    int i = 0;

    public override void Update()
    {
        if (i >= itens.Length || itens.Length <= 0) 
        {
            if(!isInt && shop.activeSelf)
            {
                shop.SetActive(isInt);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUIController>().shopping = false;
            }
            base.Update(); 
        }
        if (i != itens.Length)
        {
            shop.GetComponent<ShopControll>().AddItem(itens[i]);
            i++;
        }
        base.Update();
    }

    public override void PressToInteract(GameObject other)
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInt)
        {
            transform.LookAt(other.transform);
            isInt = true;
            shop.SetActive(isInt);
            npcInt.SetActive(!isInt);
            other.GetComponent<PlayerUIController>().shopping = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isInt)
        {
            isInt = false;
            shop.SetActive(isInt);
            npcInt.SetActive(!isInt);
            other.GetComponent<PlayerUIController>().shopping = false;
        }
    }
}
