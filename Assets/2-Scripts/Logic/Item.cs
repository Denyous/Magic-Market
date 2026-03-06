using UnityEngine;

public class Item : MonoBehaviour,ITimeListener
{
    [SerializeField] public ItemDataSO _itemData;


    private void OnEnable()
    {
        DayNightCycle.OnDayCompleted += OnDayCompleted;
        DayNightCycle.OnTimeOfDayChanged += OnDayTimeChanged;
    }

    void Start()
    {
        InitializeItem(_itemData);


    }
    public void InitializeItem(ItemDataSO itemData)
    {
        _itemData = itemData;
        
    }

    void ITimeListener.OnTimeOfDayChanged(TimeOfDay newTime)
    {
        Debug.Log($"Interface, Item: {name} - Zaman dilimi değişti: {newTime}");
    }

    void ITimeListener.OnDayCompleted(int dayNumber)
    {
        Debug.Log($"Interface, Item: {name} - Gün değişti: {dayNumber}");
    }

    void OnDayCompleted(int dayNumber)
    {
        Debug.Log($",Evet, Item: {name} - Gün değişti: {dayNumber}");
    }

    void OnDayTimeChanged(TimeOfDay timeOfDay)
    {
        Debug.Log($"Evet, Item: {name} - Zaman dilimi değişti: {timeOfDay}"); 
    }
}
