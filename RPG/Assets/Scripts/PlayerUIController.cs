/*#### Script semelhante ao PlayerController.cs serve para o controle somente da UI
  #### quis fazer separado para que os comandos de UI sejam separados dos comandos
  #### fisicos e animados.*/
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    //referencias
    [SerializeField] GameObject attributeUI = null;
    public GameObject inventoryUI = null;
    [SerializeField] GameObject EquipUI = null;
    [SerializeField] GameObject menuUI = null;
    public QuestSlot QuestUI = null;

    public bool shopping = false;

    void Update()
    {
        if (HitButton()) return;
    }
    
    // retorna se foi apertado algum botao reservado para UI dinamica do jogador
    private bool HitButton()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            OpenClose(attributeUI);
            return true;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.GetComponent<InventoryControll>().isShop = shopping;
            OpenClose(inventoryUI);
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            OpenClose(menuUI);
            return true;
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            OpenClose(EquipUI);
            return true;
        }
        return false;
    }

    //recebe a refencia do menu selecionado e ativa e desativa o objeto
    private void OpenClose(GameObject win)
    {
        win.SetActive(!win.activeSelf);
    }
}
