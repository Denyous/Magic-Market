using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Chest : MonoBehaviour, IInteractable, ITimeListener
{
    public bool IsOpened { get; private set; }

    public string UniqueID { get; private set; }

    public List<GameObject> items = new List<GameObject>();

    public static event Action<Chest> OnChestOpened;
    void Start()
    {
        UniqueID = GeneralIDHelper.GenerateUniqueID(gameObject);
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
        SetOpened(true);

    }

    private void SetOpened(bool opened)
    {
        IsOpened = opened;

        if (opened)
        {
            Debug.Log("Chest Opened");
            GiveRandomItem();
            UpdateUI();
        }

    }

    private void GiveRandomItem()
    {
        int randomIndex = UnityEngine.Random.Range(0, items.Count);
        GameObject itemChosen = items[randomIndex];
        Instantiate(itemChosen, transform.position + Vector3.up, Quaternion.identity);
    }

    private void CloseChest()
    {
        IsOpened = false;
        UpdateUI();
    }
    private void UpdateUI()
    {
        Debug.Log("Updating Chest UI");
        OnChestOpened?.Invoke(this);

    }

    public void OnDayCompleted(int dayNumber)
    {
       CloseChest();
    }
    
  


    public void OnTimeOfDayChanged(TimeOfDay newTime)
    {
    }
}





