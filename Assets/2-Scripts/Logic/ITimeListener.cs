using UnityEngine;

public interface ITimeListener 
{
    /// <summary>
    /// Zaman dilimi değiştiğinde çağrılır
    /// </summary>
    /// <param name="newTime">Yeni zaman dilimi (Sabah, Öğle, Akşam, Gece)</param>

    public void OnTimeOfDayChanged(DayNightCycle.TimeOfDay newTime);
    public void OnDayCompleted(int dayNumber);
}
