using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public enum TimeOfDay
    {
        Sabah,   // 06:00 - 12:00
        Ogle,   // 12:00 - 18:00
        Aksam,   // 18:00 - 22:00
        Gece     // 22:00 - 06:00
    }

    [System.Serializable]
    public class TimeGradient
    {
        public TimeOfDay timeOfDay;
        public Gradient skyGradient;
    }

    [Header("Day-Night Settings ")]

    [SerializeField] private float _dayDurationInSeconds = 480f;
    [SerializeField] private Light2D _sunLight;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private TimeGradient[] _timeGradients;

    private float _currentTimeInSeconds = 0f;
    private TimeOfDay _currentTimeOfDay;
    private int _totalDaysElapsed = 0;


    public delegate void TimeOfDayChangedEvent(TimeOfDay newTime);
    public static event TimeOfDayChangedEvent OnTimeOfDayChanged;

    public delegate void DayCompletedEvent(int dayNumber);
    public static event DayCompletedEvent OnDayCompleted;

    public delegate void TimeTickEvent(float currentHour);
    public static event TimeTickEvent OnTimeTick;


    private List<ITimeListener> timeListeners = new List<ITimeListener>();

    private void Start()
    {
        if (_sunLight == null)
            _sunLight = FindFirstObjectByType<Light2D>();

        if (_mainCamera == null)
            _mainCamera = Camera.main;

        if (_timeGradients == null || _timeGradients.Length == 0)
        {
            InitializeDefaultGradients();
        }

        _currentTimeOfDay = TimeOfDay.Sabah;
         FindAllTimeListeners();

    }

    private void Update()
    {
        _currentTimeInSeconds += Time.deltaTime;

        // Gün tamamlandığında bu bloğa girecek ve günün sayısını artıracak.

        if (_currentTimeInSeconds >= _dayDurationInSeconds)
        {
            _currentTimeInSeconds -= _dayDurationInSeconds;
            _totalDaysElapsed++;
            OnDayCompleted?.Invoke(_totalDaysElapsed);
            NotifyListeners(_totalDaysElapsed);

            Debug.Log($"Gün tamamlandı! Toplam gün sayısı: {_totalDaysElapsed}");
        }

        float currentHour = (_currentTimeInSeconds / _dayDurationInSeconds) * 24f;

        TimeOfDay newTimeOfDay = GetTimeOfDay(currentHour);

        //Günün dilimi değiştiğinde bu bloğa girecek.

        if (newTimeOfDay != _currentTimeOfDay)
        {
            _currentTimeOfDay = newTimeOfDay;
            OnTimeOfDayChanged?.Invoke(_currentTimeOfDay);
            NotifyListeners(_currentTimeOfDay);

            Debug.Log($"Zaman dilimi değişti: {_currentTimeOfDay}");

        }

        UpdateSkyColor(currentHour);
        UpdateLightDirection(currentHour);
        OnTimeTick?.Invoke(currentHour);
    }

    private TimeOfDay GetTimeOfDay(float hour)
    {
        if (hour >= 6f && hour < 12f)
            return TimeOfDay.Sabah;
        else if (hour >= 12f && hour < 18f)
            return TimeOfDay.Ogle;
        else if (hour >= 18f && hour < 22f)
            return TimeOfDay.Aksam;
        else
            return TimeOfDay.Gece;
    }

    private void UpdateSkyColor(float hour)
    {
        Gradient currentGradient = GetGradientForTime(_currentTimeOfDay);

        if (currentGradient == null)
            return;

        float timeRangeStart = 0f;
        float timeRangeEnd = 6f;

        switch (_currentTimeOfDay)
        {
            case TimeOfDay.Sabah:
                timeRangeStart = 6f;
                timeRangeEnd = 12f;
                break;
            case TimeOfDay.Ogle:
                timeRangeStart = 12f;
                timeRangeEnd = 18f;
                break;
            case TimeOfDay.Aksam:
                timeRangeStart = 18f;
                timeRangeEnd = 22f;
                break;
            case TimeOfDay.Gece:
                timeRangeStart = 22f;
                timeRangeEnd = 30f;
                if (hour < 6f)
                    hour += 24f;
                break;
        }

        float t = (hour - timeRangeStart) / (timeRangeEnd - timeRangeStart);
        t = Mathf.Clamp01(t);

        Color skyColor = currentGradient.Evaluate(t);

        if (_mainCamera != null)
        {
            _mainCamera.backgroundColor = skyColor;
        }

        if (_sunLight != null)
        {
            _sunLight.color = skyColor;
        }
    }

    private void UpdateLightDirection(float hour)
    {
        if (_sunLight == null)
            return;

        float rotation = (hour / 24f) * 360f;
        _sunLight.transform.rotation = Quaternion.Euler(rotation, 0f, 0f);

        if (_currentTimeOfDay == TimeOfDay.Gece)
        {
            _sunLight.intensity = 0.2f;
        }
        else if (_currentTimeOfDay == TimeOfDay.Aksam)
        {
            _sunLight.intensity = 0.6f;
        }
        else
        {
            _sunLight.intensity = 1f;
        }
    }

    private Gradient GetGradientForTime(TimeOfDay time)
    {
        foreach (var tg in _timeGradients)
        {
            if (tg.timeOfDay == time && tg.skyGradient != null)
                return tg.skyGradient;
        }
        return null;
    }

    private void InitializeDefaultGradients()
    {
        _timeGradients = new TimeGradient[4];

        _timeGradients[0] = new TimeGradient();
        _timeGradients[0].timeOfDay = TimeOfDay.Sabah;
        _timeGradients[0].skyGradient = new Gradient();

        _timeGradients[1] = new TimeGradient();
        _timeGradients[1].timeOfDay = TimeOfDay.Ogle;
        _timeGradients[1].skyGradient = new Gradient();

        _timeGradients[2] = new TimeGradient();
        _timeGradients[2].timeOfDay = TimeOfDay.Aksam;
        _timeGradients[2].skyGradient = new Gradient();

        _timeGradients[3] = new TimeGradient();
        _timeGradients[3].timeOfDay = TimeOfDay.Gece;
        _timeGradients[3].skyGradient = new Gradient();
    }


    // =================
    // LISTENERLAR
    // =================

    private void FindAllTimeListeners()
    {
        MonoBehaviour[] allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (var mono in allMonoBehaviours)
        {
            if (mono is ITimeListener listener)
            {
                RegisterListener(listener);
            }
        }
    }

    //Zaman dilimi değiştiğinde
    private void NotifyListeners(TimeOfDay newTime)
    {
        foreach (var listener in timeListeners)
        {
            listener.OnTimeOfDayChanged(newTime);
        }

    }
    //Gün tamamlandığında
    private void NotifyListeners(int newDay)
    {
        foreach (var listener in timeListeners)
        {
            listener.OnDayCompleted(newDay);
        }
    }


    public void RegisterListener(ITimeListener listener)
    {
        if (!timeListeners.Contains(listener))
        {
            timeListeners.Add(listener);
        }
    }

    public void UnregisterListener(ITimeListener listener)
    {
        timeListeners.Remove(listener);
    }

    // ============================================
    // PUBLIC GETTER METODLAR
    // ============================================

    public float GetCurrentHourAsFloat()
    {
        return (_currentTimeInSeconds / _dayDurationInSeconds) * 24f;
    }

    public TimeOfDay GetCurrentTimeOfDay()
    {
        return _currentTimeOfDay;
    }

    public string GetTimeOfDayString()
    {
        return _currentTimeOfDay.ToString();
    }

    public int GetTotalDaysElapsed()
    {
        return _totalDaysElapsed;
    }

    public float GetTimeProgress()
    {
        return _currentTimeInSeconds / _dayDurationInSeconds;
    }

    public float GetDayDurationInSeconds()
    {
        return _dayDurationInSeconds;
    }

    public bool IsTimeBetween(float startHour, float endHour)
    {
        float currentHour = GetCurrentHourAsFloat();
        return currentHour >= startHour && currentHour < endHour;
    }
}
