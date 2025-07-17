using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    #region Variables
    [SerializeField] LineRenderer _lr;
    [SerializeField] public Transform _aI;
    [SerializeField] NavMeshAgent _nav;
    #endregion
    #region Initialization
    void Start()
    {
        _lr = GetComponent<LineRenderer>();
        _nav = GetComponent<NavMeshAgent>();
    }
    #endregion
    #region Update
    void Update()
    {
        Move(_aI);
        Line();
    }
    void Line()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, transform.position);
        _lr.SetPosition(1, _aI.position);
    }
    void Move(Transform Des) => _nav.SetDestination(Des.position);
    #endregion
}
