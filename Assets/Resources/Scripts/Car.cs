using System;
using System.Collections;
using System.Linq;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    [SerializeField] public NavMeshAgent _carAgent;
    [SerializeField] public Transform _dest;
    [SerializeField] public Transform _follow;
    [SerializeField] public bool _canMove = true;
    void Start()
    {
        _follow = _dest;
        _carAgent = GetComponent<NavMeshAgent>();
        RandomStats();
    }
    void RandomStats()
    {
        
    }
    void Update()
    {
        if(_canMove) _carAgent.SetDestination(_dest.position);
        else _carAgent.isStopped = true; 
        if (_carAgent.remainingDistance <= _carAgent.stoppingDistance && _dest == _follow)
        {
            _dest = VehicleSpawn.Instance._spawnPoints[UnityEngine.Random.Range(0, VehicleSpawn.Instance._spawnPoints.Length)];
            _follow = _dest;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            StartCoroutine(Stop());
            transform.GetComponent<AudioSource>().Play();
        }
    }
    IEnumerator Stop()
    {
        _canMove = false;
        yield return new WaitForSeconds(2f);
        _carAgent.isStopped = false;
        _canMove = true;
    }
}
