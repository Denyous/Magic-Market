using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Scriptable Objects/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    [Header("Item Data")]
    public string itemName;
    public Sprite itemIcon;
    

}
