using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bench : MonoBehaviour
{
    #region Variables
    [SerializeField] int _ageCheck;
    [SerializeField] int _restTime;
    [SerializeField] float[] _defaultStoppingDis;
    [SerializeField] Transform[] _desPosition;
    [SerializeField] Transform[] _seats;
    [SerializeField] List<Seat> _seatCheck = new List<Seat>();
    #endregion
    #region Initialization
    void Start()
    {
        _seats = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
        _desPosition = new Transform[_seats.Length];
        _defaultStoppingDis = new float[_seats.Length];
        foreach (Transform seat in _seats) _seatCheck.Add(new Seat(seat, false));
    }
    #endregion
    #region RestCheck
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            AI aI = other.GetComponent<AI>();
            if (aI._age >= _ageCheck)
            {
                aI._state = State.Thinking;
                for (int i = 0; i < _seats.Length; i++)
                {
                    if (!_seatCheck[i]._seatCheck)
                    {
                        StartCoroutine(RestTimer(aI, i));
                        break;
                    }
                }
            }
        }
    }
    IEnumerator RestTimer(AI aI, int index)
    {
        aI._resting = UnityEngine.Random.value > 0.5f;
        aI.Stop();
        yield return new WaitForSeconds(2f);
        aI._state = State.Moving;
        if (aI._resting)
        {
            _desPosition[index] = aI._follow;
            aI._follow = _seatCheck[index]._seatP;
            _defaultStoppingDis[index] = aI._ai_Agent.stoppingDistance;
            aI._ai_Agent.stoppingDistance = .5f;
            _seatCheck[index]._seatCheck = true;
            yield return new WaitForSeconds(_restTime);
            _seatCheck[index]._seatCheck = false;
            aI._follow = _desPosition[index];
            _desPosition[index] = null;
            aI._resting = false;
            aI._ai_Agent.stoppingDistance = _defaultStoppingDis[index];
            _defaultStoppingDis[index] = 0;
            aI._state = State.Moving;
        }
    }
    #endregion
}
