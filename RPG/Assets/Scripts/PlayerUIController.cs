/*#### Script semelhante ao PlayerController.cs serve para o controle somente da UI
  #### quis fazer separado para que os comandos de UI sejam separados dos comandos
  #### fisicos e animados.*/
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    //referencias
    [SerializeField] GameObject attributeUI = null;
    [SerializeField] GameObject inventoryUI = null;
    [SerializeField] GameObject menuUI = null;

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
            OpenClose(inventoryUI);
            return true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
            OpenClose(menuUI);
        return false;
    }

    //recebe a refencia do menu selecionado e ativa e desativa o objeto
    private void OpenClose(GameObject win)
    {
        win.SetActive(!win.activeSelf);
    }
}
