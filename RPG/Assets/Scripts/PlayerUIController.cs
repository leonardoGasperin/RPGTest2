using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] GameObject characterC = null;
    [SerializeField] GameObject characterI = null;

    // Update is called once per frame
    void Update()
    {
        if (HitButton()) return;
    }

    private bool HitButton()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            OpenClose(characterC);
            return true;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenClose(characterI);
            return true;
        }
        return false;
    }

    private void OpenClose(GameObject win)
    {
        win.SetActive(!win.activeSelf);
    }
}
