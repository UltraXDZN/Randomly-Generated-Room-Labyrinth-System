using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomSettings : MonoBehaviour
{
    public RoomTemplates m_templates;
    private int m_rand = 0;
    public bool m_hasRoomBeenSpawned = false;

    public int dir;
    public float waitTime = 4f;

    bool[] neighbouringRooms = { false, false, false, false };

    bool isRoom;

    private void Start()
    {
        //Destroy(gameObject, waitTime);
        isRoom = GetComponentInParent<RoomCheck>()._isRoomOrHallway;
        m_templates = GetComponentInParent<RoomCheck>().IsRoomOrHallway(m_templates);
        Invoke(nameof(Spawn), 1f);
    }

    // Update is called once per frame
    public void Spawn()
    {
        //FindNeighbouringRooms();
        //Debug.Log($"m_rand = {m_rand}");
        //Debug.Log($"m_templates = {m_templates}");
        //Debug.Log(m_templates);
        if (!m_hasRoomBeenSpawned && m_templates != null)
        {
            Vector3 _pos_ = new Vector3(transform.position.x, 0, transform.position.z);
            //_pos_ = transform.TransformPoint(_pos_);
            GameObject spawnedRoom = null;
            switch (dir)
            {
                case 1:
                    m_rand = Random.Range(0, m_templates._rightRooms.Count);
                    if (m_templates._rightRooms[m_rand].name.Contains("Big"))
                        _pos_ += transform.right * GetComponentInParent<RoomCheck>().distanceFromNewRoom;
                    transform.position = _pos_;
                    //Debug.DrawLine(transform.position + Vector3.up * 2, transform.position + Vector3.up * 2 + transform.right * 30, Color.red, 99999999999);
                    spawnedRoom = Instantiate(m_templates._rightRooms[m_rand], _pos_, m_templates._rightRooms[m_rand].transform.rotation);
                    break;
                case 2:
                    m_rand = Random.Range(0, m_templates._upperRooms.Count);
                    if (m_templates._upperRooms[m_rand].name.Contains("Big"))
                        _pos_ += transform.forward * GetComponentInParent<RoomCheck>().distanceFromNewRoom;
                    transform.position = _pos_;
                    //Debug.DrawLine(transform.position + Vector3.up * 2, transform.position + Vector3.up * 2 + transform.forward * 30, Color.red, 99999999999);
                    spawnedRoom = Instantiate(m_templates._upperRooms[m_rand], _pos_, m_templates._upperRooms[m_rand].transform.rotation);
                    break;
                case 3:
                    m_rand = Random.Range(0, m_templates._leftRooms.Count);
                    if (m_templates._leftRooms[m_rand].name.Contains("Big"))
                        _pos_ += -transform.right * GetComponentInParent<RoomCheck>().distanceFromNewRoom;
                    transform.position = _pos_;
                    //Debug.DrawLine(transform.position + Vector3.up * 2, transform.position + Vector3.up * 2 + -transform.right * 30, Color.red, 99999999999);
                    spawnedRoom = Instantiate(m_templates._leftRooms[m_rand], _pos_, m_templates._leftRooms[m_rand].transform.rotation);
                    break;
                case 4:
                    m_rand = Random.Range(0, m_templates._bottomRooms.Count);
                    if (m_templates._bottomRooms[m_rand].name.Contains("Big"))
                        _pos_ += -transform.forward * GetComponentInParent<RoomCheck>().distanceFromNewRoom;
                    transform.position = _pos_;
                    //Debug.DrawLine(transform.position + Vector3.up * 2, transform.position + Vector3.up * 2 + -transform.forward * 30, Color.red, 99999999999);
                    spawnedRoom = Instantiate(m_templates._bottomRooms[m_rand], _pos_, m_templates._bottomRooms[m_rand].transform.rotation);
                    break;
            }
            if (spawnedRoom != null)
            {
                RoomCheck _rc_ = spawnedRoom.GetComponentInChildren<RoomCheck>();
                _rc_._roomTemplates = this.gameObject.GetComponentInParent<RoomCheck>()._roomTemplates;
                if (_rc_.checkDoor())
                {
                    m_templates._rooms.Remove(spawnedRoom);
                    Destroy(spawnedRoom, 0.1f);
                    Spawn();
                }
            }
            
            m_hasRoomBeenSpawned = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (!other.GetComponent<RoomSettings>().m_hasRoomBeenSpawned && !m_hasRoomBeenSpawned)
            {
                if (!other.CompareTag("StartingRoom"))
                {
                    //Instantiate(m_templates._closedRoom, transform.position, Quaternion.identity);
                    Debug.Log(other.name);
                    Destroy(gameObject);
                }
            }
            m_hasRoomBeenSpawned = true;
        }
        if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
        if (other.transform.parent.name.Contains("Wall"))
        {
            Debug.Log("Found WallRoom");
            Destroy(gameObject);
        }
    }

    void FindNeighbouringRooms()
    {
        neighbouringRooms[0] = Physics.Raycast(transform.position, Vector3.forward * 5);
        neighbouringRooms[1] = Physics.Raycast(transform.position, Vector3.back * 5);
        neighbouringRooms[2] = Physics.Raycast(transform.position, Vector3.left * 5);
        neighbouringRooms[3] = Physics.Raycast(transform.position, Vector3.right * 5);

        Debug.DrawRay(transform.position + Vector3.up, Vector3.forward * 5, Color.green, 9999999999999999999);
        Debug.DrawRay(transform.position + Vector3.up, Vector3.back * 5, Color.green, 9999999999999999999);
        Debug.DrawRay(transform.position + Vector3.up, Vector3.left * 5, Color.green, 9999999999999999999);
        Debug.DrawRay(transform.position + Vector3.up, Vector3.right * 5, Color.green, 9999999999999999999);

    }

    int neighbouringRoomsReturn()
    {
        int counter = 0;
        foreach (bool roomStatus in neighbouringRooms)
        {
            if (roomStatus)
                counter++;
        }
        return counter;
    }

    
}
