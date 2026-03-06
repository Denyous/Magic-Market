using UnityEngine;

public class Item : MonoBehaviour,ITimeListener
{
    [SerializeField] public ItemDataSO _itemData;
    
    void Start()
    {
        InitializeItem(_itemData);


    }
    public void InitializeItem(ItemDataSO itemData)
    {
        _itemData = itemData;
        
    }

    public void OnTimeOfDayChanged(DayNightCycle.TimeOfDay newTime)
    {
        Debug.Log($"[Item] {name} zaman dilimi değişti: {newTime}");
        // Zaman dilimine göre item görünümünü değiştirmek isterseniz, burada yapabilirsiniz.
        // Örneğin, gece olduğunda item'in rengini biraz karartabilirsiniz:
    }

    public void OnDayCompleted(int dayNumber)
    {
        Debug.Log(" Item Day Completed ");
    }
}
