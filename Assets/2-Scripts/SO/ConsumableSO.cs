using UnityEngine;
using System;


[CreateAssetMenu(fileName = "ConsumableSO", menuName = "Scriptable Objects/ConsumableSO")]
/// <summary>
/// Tüketilecek itemler (yemek, ilaç, vb.)
/// Enerji/stat restore özelliği ve future buff system hook'u içerir.
/// </summary>
public class ConsumableSO : ItemDataSO
{
    [SerializeField]
    [Range(0, 100)]
    [Tooltip("Tüketildiğinde enerji restore miktarı")]
    private int energyRestoreAmount = 20;

    [SerializeField]
    [Tooltip("Buff sistemi hook'u. Buffs detaylı data taşıyabilir")]
    private ConsumableBuffData buffData;

    #region Properties

    public int EnergyRestoreAmount => energyRestoreAmount;
    public ConsumableBuffData BuffData => buffData;

    #endregion

    public override bool Validate()
    {
        if (!base.Validate()) return false;

        if (energyRestoreAmount < 0)
        {
            Debug.LogError($"[ConsumableItemSO] {name}: EnergyRestoreAmount negatif olamaz!", this);
            return false;
        }

        return true;
    }
}

/// <summary>
/// Consumable buff data container. İleride genişletilebilir.
/// </summary>
[Serializable]
public class ConsumableBuffData
{
    [SerializeField]
    [Tooltip("Buff uygulanacak mı?")]
    public bool hasBuffEffect = false;

    [SerializeField]
    [Tooltip("Buff türü tanımlayıcısı")]
    public string buffId = "";

    [SerializeField]
    [Range(0.1f, 60f)]
    [Tooltip("Buff süresi (saniye)")]
    public float buffDuration = 10f;

    [SerializeField]
    [Range(0, 200)]
    [Tooltip("Buff kuvveti")]
    public int buffPower = 10;
}

