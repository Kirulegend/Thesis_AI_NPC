using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stall : MonoBehaviour
{
    [SerializeField] public Transform[] _item;
    [SerializeField] public Transform[] _seats;
    [SerializeField] public Transform[] _standPoints;
    [SerializeField] public List<SSeat> _seatCheck = new List<SSeat>();
    #region Initialization
    void Start()
    {
        Transform Temp = transform.Find("Coffees");
        _item = Temp.GetComponentsInChildren<Transform>().Where(t => t != Temp).ToArray();
        foreach (Transform seat in _seats) _seatCheck.Add(new SSeat(seat, false, transform, Name.None, .5f));
    }
    #endregion
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            Debug.Log(other.name + " entered the stall.");
            AI aI = other.GetComponent<AI>();
            for (int i = 0; i < _seats.Length; i++)
            {
                if (_seatCheck[i]._name == aI._name)
                {
                    StartCoroutine(Timer(aI, i));
                    break;
                }
                else
                {
                    if (!_seatCheck[i]._seatCheck)
                    {
                        _seatCheck[i]._destnation = aI._follow;
                        _seatCheck[i]._name = aI._name;
                        _seatCheck[i]._seatCheck = true;
                        _seatCheck[i]._defaultStoppingDis = aI._ai_Agent.stoppingDistance;
                        aI._follow = _standPoints[UnityEngine.Random.Range(0, _standPoints.Length)];
                        break;
                    }
                }
            }
        }
    }   
    #region IEnumerators
    IEnumerator Timer(AI aI, int index)
    {
        aI._state = State.Thinking;
        aI._eating = UnityEngine.Random.value > 0.25f;
        Debug.Log(aI.gameObject.name + " entered the stall.");
        yield return new WaitForSeconds(1f);
        if (aI._eating)
        {
            aI._state = State.Waiting;
            yield return new WaitForSeconds(2f);
            _item[index].parent = aI._pickPos;
            aI._state = State.Eating;
            aI._follow = _seatCheck[index]._seatP;
            _seatCheck[index]._defaultStoppingDis = aI._ai_Agent.stoppingDistance;
            aI._ai_Agent.stoppingDistance = .5f;
            _seatCheck[index]._seatCheck = true;
            yield return new WaitForSeconds(10f);
        }
        aI._eating = false;
        aI._follow = _seatCheck[index]._destnation;
        aI._ai_Agent.stoppingDistance = _seatCheck[index]._defaultStoppingDis;
        _seatCheck[index]._defaultStoppingDis = .5f;
        _seatCheck[index]._seatCheck = false;
        _seatCheck[index]._destnation = null;
        _seatCheck[index]._name = Name.None;
        aI._state = State.Moving;
    }
    #endregion
}
