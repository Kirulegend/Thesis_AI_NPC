using TMPro;
using UnityEngine;

public class Cam : MonoBehaviour
{
    #region Variables
    [SerializeField] Spawn _spawn;
    [SerializeField] Transform _currentAI;
    [SerializeField] int _index = 0;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _time;
    [SerializeField] TimeManager _timeM; 
     #endregion
    #region Initialization
    void Start()
    {
        _timeM = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        GameObject.Find("UI").GetComponent<Canvas>().enabled = true;    
        _name = GameObject.Find("UI").transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _time = GameObject.Find("UI").transform.Find("Time").GetComponent<TextMeshProUGUI>();
        _spawn = GameObject.Find("Spawn Points").GetComponent<Spawn>();
        _currentAI = _spawn._aIs[_index].transform.Find("Camera_Target").transform;
        Invoke("CamChange", 1);
    }
    #endregion
    #region Update
    void Update()
    {
        if(_timeM) _time.text = _timeM.GetFormattedTime();
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _index--;
            _index = Mathf.Clamp(_index, 0, _spawn._aIs.Count - 1);
            _currentAI = _spawn._aIs[_index].transform.Find("Camera_Target").transform;
            CamChange();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _index++;
            _index = Mathf.Clamp(_index, 0, _spawn._aIs.Count - 1);
            _currentAI = _spawn._aIs[_index].transform.Find("Camera_Target").transform;
            CamChange();
        }
    }
    void CamChange()
    {
        _name.text = _spawn._aIs[_index].GetComponent<AI>()._name.ToString();
        transform.SetParent(_currentAI.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    #endregion
}
