using UnityEngine;

public class ChestUI : MonoBehaviour
{
    [SerializeField] private Sprite chestOpenedSprite;
    [SerializeField] private Sprite chestClosedSprite;

    private void OnEnable()
    {
        Chest.OnChestOpened += UpdateUI;
    }

    private void OnDisable()
    {
        Chest.OnChestOpened -= UpdateUI;
    }
    public void UpdateUI(Chest chest)
    {
        if(chest.UniqueID != GeneralIDHelper.GenerateUniqueID(gameObject)) return;

        if (chest.IsOpened)
        {
            GetComponent<SpriteRenderer>().sprite = chestOpenedSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = chestClosedSprite;
        }

        
    }

}
