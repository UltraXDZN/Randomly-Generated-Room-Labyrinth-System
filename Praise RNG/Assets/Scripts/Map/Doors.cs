using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public bool hasTriggeredNewRoom = false;
    Floor floor;

    //private void Start()
    //{
    //    floor = GetComponentInParent<RoomSettings>().gameObject.GetComponentInChildren<Floor>();
    //}
    //private void Update()
    //{
    //    Vector3 direction = transform.TransformDirection(Vector3.forward * 20);
    //    Debug.DrawRay(new Vector3(0, 0, transform.position.z + transform.localScale.z + 1.5f), direction, Color.blue, 300);
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggeredNewRoom)
        {
            //GameObject _roomToSpawn_ = GetComponentInParent<RoomSettings>().gameObject.GetComponentInParent<MapGenerator>()._roomPrefabs[0];
            GameObject _parentObject_ = GetComponentInParent<RoomSettings>().gameObject.GetComponentInParent<MapGenerator>().gameObject;
            RoomSettings _room_ = GetComponentInParent<RoomSettings>();
            MapGenerator _map_ = GetComponentInParent<RoomSettings>().gameObject.GetComponentInParent<MapGenerator>();
            Vector3 _direction_ = new Vector3();
            Vector3 _roomRotationVector_ = new Vector3();
            Debug.Log(tag);


            if (tag == "Left")
            {
                _direction_ = Vector3.left;
                _roomRotationVector_ = Vector3.down;
            }
            else if (tag == "Right")
            {
                _direction_ = Vector3.right;
                _roomRotationVector_ = Vector3.up;
            }
            else if (tag == "Up")
            {
                _direction_ = Vector3.forward;
            }
            else if (tag == "Down")
            {
                _direction_ = Vector3.back;
                _roomRotationVector_ = Vector3.down * 2;
            }

            Ray _rayOfRooms_ = new Ray(new Vector3(_direction_.x * (_map_.incrementForSpawn) * _map_.curRoom , 0, _direction_.z * (_map_.incrementForSpawn) * _map_.curRoom), _direction_);
            RaycastHit hit;

            Debug.DrawRay(_rayOfRooms_.origin, _rayOfRooms_.direction, Color.blue, 300);

            if (Physics.Raycast(_rayOfRooms_, out hit))
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.GetComponentInParent<RoomSettings>().gameObject.name);
                }
            }
            else
            {
                Quaternion _roomRotation_ = Quaternion.Euler(_roomRotationVector_);
                _direction_ *= _map_.incrementForSpawn;
                Debug.Log(_direction_);


                Vector3 _positionToSpawn_ = _room_.transform.position + _direction_;
                //var _newRoom_ = Instantiate(_roomToSpawn_, _positionToSpawn_, _roomRotation_, _parentObject_.transform);
                //_newRoom_.name = $"Room Instance: {_map_.curRoom}";
                hasTriggeredNewRoom = true;
                _map_.curRoom++;
                //floor.changeDoors(false);
            }
        }
    }
}
