using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    #region Variables
    [SerializeField] GameObject _aiNPC;
    [SerializeField] GameObject _spawnParent;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] public List<GameObject> _aIs;
    [SerializeField] int _spawnCount;
    [SerializeField] bool _autoSpawn = false;
    Dictionary<Transform, bool> _spawnCheck;
    #endregion
    #region Initialization
    void Awake()
    {
        if (_autoSpawn) 
        {
            _spawnPoints = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
            _spawnParent = GameObject.Find("AI's").gameObject;
            _aiNPC = Resources.Load<GameObject>("Prefabs/AI");
            _spawnCheck = _spawnPoints.ToDictionary(sp => sp, sp => false);
            _spawnCount = Enum.GetValues(typeof(Name)).Length - 1;
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
            GameObject npc = Instantiate(_aiNPC, chosenPoint.position, Quaternion.identity);
            _aIs.Add(npc);
            npc.transform.parent = _spawnParent.transform;
            _spawnCheck[chosenPoint] = true;
            npc.GetComponent<AI>()._head = Instantiate(Resources.Load<GameObject>("Prefabs/Head"));
            npc.GetComponent<AI>()._head.transform.position = npc.transform.position;
            npc.GetComponent<AI>()._head.transform.parent = _spawnParent.transform;
            npc.GetComponent<AI>()._head.GetComponent<Head>()._body = npc.GetComponent<AI>();
        }
    }
    #endregion
}
