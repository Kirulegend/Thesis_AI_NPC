using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VehicleSpawn : MonoBehaviour
{
    #region Variables
    [SerializeField] public GameObject _car;
    [SerializeField] public GameObject _spawnParent;
    [SerializeField] public Transform[] _spawnPoints;
    [SerializeField] public List<GameObject> _cars;
    [SerializeField] public int _spawnCount;
    [SerializeField] public bool _autoSpawn = true;
    Dictionary<Transform, bool> _spawnCheck;
    public static VehicleSpawn Instance;
    #endregion
    #region Initialization
    void Awake()
    {
        Instance = this;
        if (_autoSpawn)
        {
            _spawnPoints = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
            _spawnParent = GameObject.Find("Vehicle's").gameObject;
            _car = Resources.Load<GameObject>("Prefabs/Car");
            _spawnCheck = _spawnPoints.ToDictionary(sp => sp, sp => false);
            SpawnAI();
        }
    }
    #endregion
    #region Spawn
    void SpawnAI()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            List<Transform> availableSpots = _spawnCheck.Where(pair => pair.Value == false).Select(pair => pair.Key).ToList();
            Transform chosenPoint = availableSpots[UnityEngine.Random.Range(0, availableSpots.Count)];
            GameObject car = Instantiate(_car, chosenPoint.position, Quaternion.identity);
            _cars.Add(car);
            car.transform.parent = _spawnParent.transform;
            car.GetComponent<Car>()._dest = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
            _spawnCheck[chosenPoint] = true;
        }
    }
    #endregion
}
