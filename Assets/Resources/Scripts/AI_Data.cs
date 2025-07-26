using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class AI_Data : MonoBehaviour
{
    #region Variables
    [SerializeField] public Dictionary<Name, bool> _nameCheck;
    [SerializeField] public List<JobTransform> _jobTransform;
    [SerializeField] public List<N_G> _nameGender;
    [SerializeField] public List<P_A> _jobAge;
    public static AI_Data _instance;
    #endregion
    #region Initialization
    void Awake()
    {
        _instance = this;
        _nameCheck = Enum.GetValues(typeof(Name)).Cast<Name>().ToDictionary(name => name, name => false);
        _nameGender = new List<N_G>
        {
            new N_G { _name = Name.Kiran, _sex = Sex.Bisexual },
            new N_G { _name = Name.Bharath, _sex = Sex.Trans },
            new N_G { _name = Name.Harsha, _sex = Sex.Lesbiean },
            new N_G { _name = Name.Deepak, _sex = Sex.Lesbiean },
            new N_G { _name = Name.Gagan, _sex = Sex.Male },
            new N_G { _name = Name.Krishna, _sex = Sex.Asexual },
            new N_G { _name = Name.Sudheer, _sex = Sex.Male },
            new N_G { _name = Name.Tanush, _sex = Sex.Asexual },
            new N_G { _name = Name.Sahithi, _sex = Sex.Female },
            new N_G { _name = Name.Nikitha, _sex = Sex.Female },
            new N_G { _name = Name.Sashi, _sex = Sex.Male },
            new N_G { _name = Name.Aashrith, _sex = Sex.Male },
            new N_G { _name = Name.Shantosh, _sex = Sex.Bisexual},
            new N_G { _name = Name.Chandu, _sex = Sex.Male},
            new N_G { _name = Name.Omkar, _sex = Sex.Bisexual},
            new N_G { _name = Name.Magar, _sex = Sex.Male},
            new N_G { _name = Name.Rajamouli, _sex = Sex.Male},
            new N_G { _name = Name.Sanjay, _sex = Sex.Male},
            new N_G { _name = Name.Ram, _sex = Sex.Male},
            new N_G { _name = Name.Ambar, _sex = Sex.Male}
        };
        //(Sex)Enum.GetValues(typeof(Sex)).GetValue(UnityEngine.Random.Range(0, Enum.GetValues(typeof(Sex)).Length))
        _jobAge = new List<P_A>
        {
            new P_A { _job = Job.Student, _min = 5, _max = 22 },
            new P_A { _job = Job.Doctor, _min = 20, _max = 50 },
            new P_A { _job = Job.Artist, _min = 20, _max = 60 },
            new P_A { _job = Job.Gamer, _min = 10, _max = 30 },
            new P_A { _job = Job.Programmer, _min = 20, _max = 50 },
            new P_A { _job = Job.Athlete, _min = 25, _max = 50 },
            new P_A { _job = Job.Old_Patrol, _min = 40, _max = 75 }
        };
    }
    #endregion
}
#region Class Datatypes
[Serializable]
public class JobTransform
{
    public Job _job;
    public Transform[] _targetTransform;
}

public class N_G
{
    public Name _name;
    public Sex _sex;
}

public class P_A
{
    public Job _job;
    public int _min;
    public int _max;      
}
public class Seat
{
    public Transform _seatP;
    public bool _seatCheck;
    public Seat(Transform seatP, bool seatCheck)
    {
        _seatP = seatP;
        _seatCheck = seatCheck;
    }
}
#endregion