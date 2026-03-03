using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light2D _sunLight;
    [SerializeField] private Gradient _sunColorGradient;
    [SerializeField ]private float _time;
    private TimeOfDay _currentTimeOfDay;

    private void Awake()
    {
        _currentTimeOfDay = TimeOfDay.Morning;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= 10f)
        {
            _currentTimeOfDay = TimeOfDay.Afternoon;
            _sunColorGradient.Evaluate(_time);
            Debug.Log("Afternoon");
        }
        if (_time >= 15f)
        {
            _currentTimeOfDay = TimeOfDay.Evening;
            _sunColorGradient.Evaluate(40f);
            Debug.Log("Evening");
        }
        if (_time >= 20f)
        {
            _currentTimeOfDay = TimeOfDay.Night;
            _sunColorGradient.Evaluate(_time);
            Debug.Log("Night");
        }
    
    }

}


public enum TimeOfDay
{
    Morning,
    Afternoon,
    Evening,
    Night
}
