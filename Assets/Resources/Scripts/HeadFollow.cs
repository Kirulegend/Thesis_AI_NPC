using TMPro;
using UnityEngine;

public class Head : MonoBehaviour
{
    #region Variables
    [SerializeField] public AI _body;
    [SerializeField] Vector3 _vel = Vector3.zero;
    [SerializeField, Range(0.01f, .1f)] float _speed;
    #endregion
    #region Updates
    void LateUpdate() => transform.position = Vector3.SmoothDamp(transform.position, 
                                              new Vector3(_body.transform.position.x, _body.transform.position.y + 1.25f, _body.transform.position.z), 
                                              ref _vel, 
                                              _speed);
    void Update() => SetState();
    void SetState() => GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + _body._state);
    #endregion
}
