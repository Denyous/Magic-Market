using UnityEngine;


[CreateAssetMenu(fileName = "ResourceSO", menuName = "Scriptable Objects/ResourceSO")]
/// <summary>
/// Taş, ahşap, mineral gibi hammer resource itemler.
/// Temel data'yı kullanır, ek specialization gerekmez.
/// </summary>
public class ResourceSO : ItemDataSO
{
    
    [SerializeField]
    [Tooltip("Satış değeri (ileride ekonomi sistemi için)")]
    private int sellValue = 10;

    public int SellValue => sellValue;

    public override bool Validate()
    {
        if (!base.Validate()) return false;

        if (sellValue < 0)
        {
            Debug.LogError($"[ResourceItemSO] {name}: SellValue negatif olamaz!", this);
            return false;
        }

        return true;
    }
}
