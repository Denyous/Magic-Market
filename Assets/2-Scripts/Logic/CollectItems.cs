using System;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    public delegate void OnItemCollected(ItemDataSO itemData);
    public static event OnItemCollected onItemCollected;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {

            Debug.Log($"Object has been detected");
            Destroy(collision.gameObject);
            
            if (onItemCollected != null)
            {
                onItemCollected.Invoke(collision.gameObject.GetComponent<Item>()._itemData);
            }
        }
    }
}
