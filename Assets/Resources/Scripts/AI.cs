using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class AI : MonoBehaviour
{
    #region Variables
    [SerializeField] public NavMeshAgent _ai_Agent;
    [SerializeField] public GameObject _head;
    [SerializeField] public Transform _follow;
    [SerializeField] public Size _size;
    [SerializeField] public Name _name;
    [SerializeField] public State _state;
    [SerializeField] public Job _job;
    [SerializeField] public Sex _sex;
    [SerializeField] public int _age;
    [SerializeField] bool _random = false;
    [SerializeField] public bool _resting = false;
    [SerializeField] public bool _canChat = true;
    [SerializeField] bool _isPet = false;
    [SerializeField] Transform _dest = null;
    [SerializeField] public Name[] _alias;
    #endregion
    #region Initialization
    void Start()
    {
        _ai_Agent = GetComponent<NavMeshAgent>();
        RandomStats();
    }
    void RandomStats()
    {
        if (_random)
        {
            _size = (Size)UnityEngine.Random.Range(1, Enum.GetValues(typeof(Size)).Length);
            _ai_Agent.speed -= (float)_size / 5;

            _job = (Job)UnityEngine.Random.Range(1, Enum.GetValues(typeof(Job)).Length);

            if (_job == Job.Old_Patrol || _job == Job.Artist || _job == Job.Gamer || _job == Job.Programmer) _isPet = UnityEngine.Random.value > 0.5f;

            foreach (var ja in AI_Data._instance._jobAge)
            {
                if (ja._job == _job) { _age = UnityEngine.Random.Range(ja._min, ja._max); break; }
            }

            var availableNames = AI_Data._instance._nameCheck.Where(pair => !pair.Value).Select(pair => pair.Key).ToList();
            _name = availableNames[UnityEngine.Random.Range(1, availableNames.Count)];
            AI_Data._instance._nameCheck[_name] = true;

            foreach (var ng in AI_Data._instance._nameGender)
            {
                if (ng._name == _name) { _sex = ng._sex; break; }
            }
        }
        foreach (var jt in AI_Data._instance._jobTransform)
        {
            if (jt._job == _job)
            {
                _follow = jt._targetTransform[UnityEngine.Random.Range(0, jt._targetTransform.Length)];
                _dest = _follow;
                break;
            }
        }
        if (_isPet)
        {
            GameObject Pet = Instantiate(Resources.Load<GameObject>("Prefabs/Pet"), transform.position, Quaternion.identity);
            Pet.GetComponent<Pet>()._aI = transform;
        }
    }
    #endregion
    #region Update
    void Update()
    {
        if (_state != State.Thinking && _state != State.Resting && _state != State.Chatting)
        {
            _ai_Agent.isStopped = false;
            SetState();
            Move(_follow.position);
        }
    }
    void Move(Vector3 Destination) => _ai_Agent.SetDestination(Destination);
    public void Stop() => _ai_Agent.isStopped = true;
    void SetState()
    {
        if (_ai_Agent.velocity.sqrMagnitude > 0.01f) _state = State.Moving;
        else if (_ai_Agent.velocity.sqrMagnitude < 0.01f && _resting && _state == State.Moving) _state = State.Resting;
        else if (_ai_Agent.remainingDistance <= _ai_Agent.stoppingDistance && _state == State.Idle && _dest == _follow)
        {
            foreach (var jt in AI_Data._instance._jobTransform)
            {
                if (jt._job == _job)
                {
                    _follow = jt._targetTransform[UnityEngine.Random.Range(0, jt._targetTransform.Length)];
                    _dest = _follow;
                    break;
                }
            }
        }
        else _state = State.Idle;

    }
    #endregion
    #region IEnumerators
    public IEnumerator Chatting(AI otherAI)
    {
        _state = State.Chatting;
        otherAI._state = State.Chatting;
        _canChat = false;
        otherAI._canChat = false;
        Stop();
        otherAI.Stop();
        yield return new WaitForSeconds(5);
        _state = State.Moving;
        otherAI._state = State.Moving;
        yield return new WaitForSeconds(10);
        _canChat = true;
        otherAI._canChat = true;
    }
    #endregion
}