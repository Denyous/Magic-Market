using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public ItemDataSO _itemData;
    
    void Start()
    {
        InitializeItem(_itemData);


    }
    public void InitializeItem(ItemDataSO itemData)
    {
        _itemData = itemData;
        gameObject.GetComponent<SpriteRenderer>().sprite = _itemData.itemIcon;
    }


}
