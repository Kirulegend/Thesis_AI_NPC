using System.Linq;
using UnityEngine;

public class CustomStreetLight : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject[] _light;
    [SerializeField] TimeManager _time;
    #endregion
    #region Initialization
    void Awake()
    {
        _light = transform.Cast<Transform>().SelectMany(child => child.Cast<Transform>()).Select(t => t.gameObject).ToArray();
        _time = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        _time.OnDayStarted.AddListener(TurnOffLights);
        _time.OnNightStarted.AddListener(TurnOnLights);
    }
    #endregion
    #region SubscribeEventMethods
    public void TurnOnLights()
    {
        foreach (var light in _light)
        {
            if (light != null) light.SetActive(true);
        }
    }
    public void TurnOffLights()
    {
        foreach (var light in _light)
        {
            if (light != null) light.SetActive(false);
        }
    }
    #endregion
}
