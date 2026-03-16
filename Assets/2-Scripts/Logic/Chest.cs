using UnityEngine;

public class Chest : MonoBehaviour,IInteractable
{
    public bool IsOpened { get; private set; }


    void Start()
    {

    }

    public bool CanInteract()
    {
        return !IsOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        
        OpenChest();

    }

    private void OpenChest()
    {
        IsOpened = true;
        Debug.Log("Chest Opened");
    }

}
