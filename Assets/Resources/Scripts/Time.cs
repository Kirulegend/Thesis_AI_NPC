using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoBehaviour
{
    #region  Variables
    [SerializeField, Range(0f, 24f)] float timeOfDay = 6f;
    [SerializeField, Min(0.01f)] float timeSpeed = .1f;
    [SerializeField] Light directionalLight;
    [SerializeField] public UnityEvent OnNightStarted;
    [SerializeField] public UnityEvent OnDayStarted;
    [SerializeField] bool IsDay => timeOfDay >= 5f && timeOfDay < 17f;
    [SerializeField] bool _wasDay = true;
    #endregion
    #region TheDeo
    void Start() 
    {
        _wasDay = IsDay;
        OnDayStarted?.Invoke();
        directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
    }   
    void Update()
    {
        timeOfDay += Time.deltaTime * timeSpeed;
        if (timeOfDay >= 24f) timeOfDay -= 24f;
        if (directionalLight) directionalLight.transform.rotation = Quaternion.Euler((timeOfDay / 24f) * 360f - 90f, 170f, 0f);
        if (_wasDay && !IsDay)
        {
            OnNightStarted?.Invoke();
            _wasDay = false;
        }
        else if (!_wasDay && IsDay)
        {
            OnDayStarted?.Invoke();
            _wasDay = true;
        }
    }
    public string GetFormattedTime()
    {
        int hour = Mathf.FloorToInt(timeOfDay);
        int minute = Mathf.FloorToInt((timeOfDay - hour) * 60f);
        return $"{hour:00}:{minute:00}";
    }
    #endregion
}