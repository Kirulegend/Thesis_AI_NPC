using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stall : MonoBehaviour
{
    //[SerializeField] public Transform[] _item;
    [SerializeField] public Transform[] _seats;
    Dictionary<Transform, bool> _standPoints = new();
    [SerializeField] public List<SSeat> _seatCheck = new List<SSeat>();
    #region Initialization
    void Start()
    {
        Transform Temp = transform.Find("Coffees");
        _standPoints = GetComponentsInChildren<Transform>(true).Where(t => t != transform && t.name == "Coffee Shop").ToDictionary(t => t, t => false);
        //_item = Temp.GetComponentsInChildren<Transform>().Where(t => t != Temp).ToArray();
        foreach (Transform seat in _seats) _seatCheck.Add(new SSeat(seat));
    }
    #endregion
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            AI aI = other.GetComponent<AI>();
            if (!aI._eating)
            {
                for (int i = 0; i < _seats.Length; i++)
                {
                    if (_seatCheck[i]._name == aI._name && _seatCheck[i]._count == 1)
                    {
                        _standPoints[aI._follow] = false;
                        StartCoroutine(Timer(aI, i));
                        break;
                    }
                    else
                    {
                        if (!_seatCheck[i]._seatCheck && _seatCheck[i]._count == 0)
                        {
                            _seatCheck[i]._destnation = aI._follow;
                            _seatCheck[i]._name = aI._name;
                            _seatCheck[i]._seatCheck = true;
                            _seatCheck[i]._defaultStoppingDis = aI._ai_Agent.stoppingDistance;
                            _seatCheck[i]._count++;
                            aI._ai_Agent.stoppingDistance = .25f;
                            aI._follow = _standPoints.First(p => !p.Value).Key;
                            _standPoints[aI._follow] = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            Debug.Log(other.gameObject.name);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            foreach (var seat in _seatCheck)
            {
                print($"Name: {seat._name}, Check: {seat._seatCheck}, Destination: {seat._destnation}, Default Distance: {seat._defaultStoppingDis}");
            }
        }
    }
    #region IEnumerators
    IEnumerator Timer(AI aI, int index)
    {
        aI.Stop();
        _seatCheck[index]._count++;
        aI._state = State.Thinking;
        aI._eating = UnityEngine.Random.value > 0.25f;
        yield return new WaitForSeconds(2.5f);
        if (aI._eating)
        {
            aI._state = State.Waiting;
            yield return new WaitForSeconds(5f);
            GameObject tempCoffee = Instantiate(Resources.Load<GameObject>("Prefabs/Coffee"));
            tempCoffee.transform.parent = aI._pickPos;
            tempCoffee.transform.position = aI._pickPos.position;
            Destroy(tempCoffee, 15f);
            aI._state = State.Eating;
            aI._follow = _seatCheck[index]._seatP;
            _seatCheck[index]._seatCheck = true;
            yield return new WaitForSeconds(15f);
        }
        aI._follow = _seatCheck[index]._destnation;
        aI._ai_Agent.stoppingDistance = _seatCheck[index]._defaultStoppingDis;
        _seatCheck[index]._defaultStoppingDis = .25f;
        _seatCheck[index]._seatCheck = false;
        _seatCheck[index]._destnation = null;
        _seatCheck[index]._name = Name.None;
        _seatCheck[index]._count = 0;
        aI._state = State.Moving;
        yield return new WaitForSeconds(10f);
        aI._eating = false;
    }
    #endregion
}
