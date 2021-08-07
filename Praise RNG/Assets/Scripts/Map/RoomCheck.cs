using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCheck : MonoBehaviour
{
    public GameObject[] _spawnPoints;
    private Vector3[] _targets;
    private float _distance_ = 2f;

    public RoomSettings _roomSetting;
    public RoomSetup _roomTemplates;

    public float distanceFromNewRoom;

    public bool _firstRoom = false;

    public bool _isRoomOrHallway;
    public RoomTemplates _rt;

    private void Awake()
    {
        if (gameObject.GetComponent<AddRoom>() != null)
        {
            _rt = IsRoomOrHallway(_rt);
            GetComponent<AddRoom>().AddRoomToCollection(_rt);
        }
    }
    private void Start()
    {
        if (_firstRoom && _isRoomOrHallway)
        {
            //checkDoor();
            if (_roomTemplates._allRays == null)
            {
                _roomTemplates._allRays = new List<Ray>();
            }
        }
    }

    public void calculatingTargets()
    {
        _targets = new Vector3[_spawnPoints.Length];
        for (int i = 0; i < _spawnPoints.Length; ++i)
        {
            _targets[i] = _spawnPoints[i].transform.position - transform.position;
        }
    }

    //private void LateUpdate()
    //{
    //    calculatingTargets();
    //    for (int i = 0; i < _targets.Length; ++i)
    //    {
    //        Debug.DrawRay(transform.position + Vector3.up * 2, _targets[i], Color.blue);
    //    }
    //}

    public bool checkDoor()
    {
        calculatingTargets();
        bool state;
        for (int i = 0; i < _targets.Length; ++i)
        {
            //Debug.DrawRay(transform.position + Vector3.up * 2, _targets[i] * _distance_, Color.blue, 99999999999);

            Ray _ray_ = new Ray(transform.position + Vector3.up * 2, _targets[i] * _distance_);
            //_roomTemplates._allRays.Add(_ray_);

            if (Physics.Raycast(_ray_, _distance_))
            {
                state = true;
                return state;
            }
            else
            {
                if (!_isRoomOrHallway && _roomTemplates != null)
                {
                    foreach (Ray _r_ in _roomTemplates._allRays)
                    {
                        Debug.DrawRay(_r_.origin, _r_.direction * 100, Color.green, 9999999);
                        //Debug.Log(_r_);
                        //Debug.Log("Loop Entered");
                        if ((_ray_.direction + _ray_.origin) != (_r_.direction + _r_.origin))
                        {
                            //Debug.Log($"Ray found: {_r_}");

                            int side = (_r_.direction.x > 1 || _r_.direction.z > 1) ? -1 : 1;

                            Vector3 _interceptionPoint_ = FindInterceptionPointBetweenRays(_ray_, _r_, side);

                            //Debug.Log($"Point Found: {_interceptionPoint_}");
                            if (_interceptionPoint_ != Vector3.zero)
                            {
                                if (Mathf.Abs(_interceptionPoint_.x) > 1 && Mathf.Abs(_interceptionPoint_.z) > 1)
                                {
                                    Debug.Log($"{_interceptionPoint_} : {_spawnPoints[i].gameObject.transform.position}");
                                    Vector3 _pointRoomA_ = new Vector3(_interceptionPoint_.x, 0, _interceptionPoint_.z / 2);
                                    Vector3 _pointRoomB_ = new Vector3(_interceptionPoint_.x / 2, 0, _interceptionPoint_.z);

                                    if (_pointRoomA_.x == _spawnPoints[i].transform.position.x || _pointRoomA_.z == _spawnPoints[i].transform.position.z)
                                    {
                                        Instantiate(GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomSetup>()._closedRoom, _pointRoomA_, default(Quaternion));
                                        _spawnPoints[i].GetComponent<RoomSettings>().m_hasRoomBeenSpawned = true;
                                        _roomTemplates._allRays.Remove(_r_);
                                        Debug.Log("Spawned A");
                                        return false;
                                        break;
                                    }
                                    if (_pointRoomB_.x == _spawnPoints[i].transform.position.x || _pointRoomB_.z == _spawnPoints[i].transform.position.z)
                                    {
                                        Instantiate(GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomSetup>()._closedRoom, _pointRoomB_, default(Quaternion));
                                        _spawnPoints[i].GetComponent<RoomSettings>().m_hasRoomBeenSpawned = true;
                                        _roomTemplates._allRays.Remove(_r_);
                                        Debug.Log("Spawned B");
                                        return false;
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        for (int i = 0; i < _spawnPoints.Length; ++i)
        {
            RoomSettings _rs_ = _spawnPoints[i].GetComponent<RoomSettings>();
            //_rs_.m_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            _rs_.Spawn();
        }
        return false;
    }

    Vector3 FindInterceptionPointBetweenRays(Ray currentRay, Ray nextRay, int positive = 1, float tolerance = 0.0001f)
    {
        //return currentRay.origin - currentRay.direction + nextRay.origin - nextRay.direction;
        // equations of the form x = c (two vertical lines)
        currentRay.direction *= _distance_;
        nextRay.direction *= _distance_;
        if (Math.Abs(currentRay.origin.x - currentRay.GetPoint(10 * positive).x) < tolerance && Math.Abs(nextRay.origin.x - nextRay.GetPoint(10 * positive).x) < tolerance && Math.Abs(currentRay.origin.x - nextRay.origin.x) < tolerance)
        {
            Debug.LogWarning("Both lines overlap vertically, ambiguous intersection points.");
        }

        //equations of the form y=c (two horizontal lines)
        if (Math.Abs(currentRay.origin.z - currentRay.GetPoint(10 * positive).z) < tolerance && Math.Abs(nextRay.origin.z - nextRay.GetPoint(10 * positive).z) < tolerance && Math.Abs(currentRay.origin.z - nextRay.origin.z) < tolerance)
        {
            Debug.LogWarning("Both lines overlap horizontally, ambiguous intersection points.");
        }

        //equations of the form x=c (two vertical lines)
        if (Math.Abs(currentRay.origin.x - currentRay.GetPoint(10 * positive).x) < tolerance && Math.Abs(nextRay.origin.x - nextRay.GetPoint(10 * positive).x) < tolerance)
        {
            return default(Vector3);
        }

        //equations of the form y=c (two horizontal lines)
        if (Math.Abs(currentRay.origin.z - currentRay.GetPoint(10 * positive).z) < tolerance && Math.Abs(nextRay.origin.z - nextRay.GetPoint(10 * positive).z) < tolerance)
        {
            return default(Vector3);
        }

        float _x_, _z_;

        if (Math.Abs(currentRay.origin.x - currentRay.GetPoint(10 * positive).x) < tolerance)
        {
            float m2 = (nextRay.GetPoint(10 * positive).z - nextRay.origin.z) / (nextRay.GetPoint(10 * positive).x - nextRay.origin.x);
            float c2 = -m2 * nextRay.origin.x + nextRay.origin.z;

            _x_ = currentRay.origin.x;
            _z_ = c2 + m2 * currentRay.origin.x;
            return new Vector3(_x_, 0, _z_);
        }
        else if (Math.Abs(nextRay.origin.x - nextRay.GetPoint(10 * positive).x) < tolerance)
        {
            float m1 = (currentRay.GetPoint(10 * positive).z - currentRay.origin.z) / (currentRay.GetPoint(10 * positive).x - currentRay.origin.x);
            float c1 = -m1 * currentRay.origin.x + currentRay.origin.z;

            _x_ = nextRay.origin.x;
            _z_ = c1 + m1 * nextRay.origin.x;
            return new Vector3(_x_, 0, _z_);
        }
        else
        {
            float m1 = (currentRay.GetPoint(10 * positive).z - currentRay.origin.z) / (currentRay.GetPoint(10 * positive).x - currentRay.origin.x);
            float c1 = -m1 * currentRay.origin.x + currentRay.origin.z;


            float m2 = (nextRay.GetPoint(10 * positive).z - nextRay.origin.z) / (nextRay.GetPoint(10 * positive).x - nextRay.origin.x);
            float c2 = -m2 * nextRay.origin.x + nextRay.origin.z;

            _x_ = (c1 - c2) / (m2 - m1);
            _z_ = c2 + m2 * _x_;

            if (!(Math.Abs(-m1 * _x_ + _z_ - c1) < tolerance
                && Math.Abs(-m2 * _x_ + _z_ - c2) < tolerance))
            {
                return default(Vector3);
            }
            return new Vector3(_x_, 0, _z_);
        }
    }

    public RoomTemplates IsRoomOrHallway(RoomTemplates _rt)
    {
        if (_isRoomOrHallway)
        {
            _rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<HallwaySetup>();
        }
        else
        {
            _rt = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomSetup>();
        }
        return _rt;
    }
}
