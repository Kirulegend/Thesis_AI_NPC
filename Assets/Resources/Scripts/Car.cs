using System;
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
    void Start()
    {
        _carAgent = GetComponent<NavMeshAgent>();
        RandomStats();
    }
    void RandomStats()
    {
        
    }
    private void Update()
    {
        _carAgent.SetDestination(_dest.position);
        if (_carAgent.remainingDistance <= _carAgent.stoppingDistance && _dest == _follow)
        {
            _dest = VehicleSpawn.Instance._spawnPoints[UnityEngine.Random.Range(0, VehicleSpawn.Instance._spawnPoints.Length)];
            _follow = _dest;
        }
    }
}
