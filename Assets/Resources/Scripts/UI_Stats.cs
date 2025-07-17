using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    #region Variables
    [SerializeField] AI _ai;
    [SerializeField] TextMeshProUGUI _size;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _sex;
    [SerializeField] TextMeshProUGUI _state;
    [SerializeField] TextMeshProUGUI _job;
    [SerializeField] TextMeshProUGUI _age;
    [SerializeField] TextMeshProUGUI _destination;
    #endregion
    #region Initialization
    void Start()
    {
        _ai = GetComponentInParent<AI>();
        _size = transform.Find("Size").GetComponent<TextMeshProUGUI>();
        _name = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _sex = transform.Find("Sex").GetComponent<TextMeshProUGUI>();
        _state = transform.Find("State").GetComponent<TextMeshProUGUI>();
        _job = transform.Find("Job").GetComponent<TextMeshProUGUI>();
        _age = transform.Find("Age").GetComponent<TextMeshProUGUI>();
        _destination = transform.Find("Destination").GetComponent<TextMeshProUGUI>();
        _ai._state = State.Thinking;
        Invoke("StaticStats", 1);
    }
    #endregion
    #region Dynamic / Static Data
    void StaticStats()
    {
        _name.text = "Name : " + _ai._name.ToString();
        _sex.text = "Sex : " + _ai._sex.ToString();
        _size.text = "Size : " + _ai._size.ToString();
        _job.text = "Job : " + _ai._job.ToString();
        _age.text = "Age : " + _ai._age.ToString();
        _ai._state = State.Idle;
    }
    void Update() => DynamicStats();
    void DynamicStats()
    {
        _destination.text = "Dest : " + _ai._follow.name;
        _state.text = "State : " + _ai._state.ToString();
    } 
    #endregion
}
