using UnityEngine;

public interface ITimeListener 
{
    /// <summary>
    /// Zaman dilimi değiştiğinde çağrılır
    /// Ayıca ek olarak gün tamamlandığında da Çarılır.
    /// </summary>
    /// <param name="newTime">Yeni zaman dilimi (Sabah, Öğle, Akşam, Gece)</param>

    public void OnTimeOfDayChanged(TimeOfDay newTime);
    public void OnDayCompleted(int dayNumber);
}
