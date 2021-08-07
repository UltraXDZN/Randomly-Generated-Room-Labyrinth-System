using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //public List<GameObject> _roomPrefabs;
    //public Dictionary<int, Vector3> locationsToSpawn = new Dictionary<int, Vector3>();
    public int curRoom = 0;
    public float incrementForSpawn = 20;

    private RoomTemplates m_templates;
    private int m_rand;
    private bool m_hasRoomBeenSpawned = false;

    public int dir;

    private void Start()
    {
        //locationsToSpawn.Add(1, Vector3.back);
        //locationsToSpawn.Add(2, Vector3.forward);
        //locationsToSpawn.Add(3, Vector3.left);
        //locationsToSpawn.Add(4, Vector3.right);
        //m_templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //Invoke("Spawn", 0.1f);
    }

    // Update is called once per frame
    //void Spawn()
    //{
    //    if (!m_hasRoomBeenSpawned)
    //    {
    //        switch (dir)
    //        {
    //            case 1:
    //                m_rand = Random.Range(0, m_templates._rightRooms.Count);
    //                Instantiate(m_templates._bottomRooms[m_rand], transform.position, m_templates._rightRooms[m_rand].transform.rotation);
    //                break;
    //            case 2:
    //                m_rand = Random.Range(0, m_templates._upperRooms.Count);
    //                Instantiate(m_templates._upperRooms[m_rand], transform.position, m_templates._upperRooms[m_rand].transform.rotation);
    //                break;
    //            case 3:
    //                m_rand = Random.Range(0, m_templates._leftRooms.Count);
    //                Instantiate(m_templates._leftRooms[m_rand], transform.position, m_templates._leftRooms[m_rand].transform.rotation);
    //                break;
    //            case 4:
    //                m_rand = Random.Range(0, m_templates._bottomRooms.Count);
    //                Instantiate(m_templates._bottomRooms[m_rand], transform.position, m_templates._bottomRooms[m_rand].transform.rotation);
    //                break;
    //        }
    //        return;
    //    }
    //    m_hasRoomBeenSpawned = true;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "SpawnPoint" && other.GetComponent<RoomSettings>().m_hasRoomBeenSpawned)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    /* each room type calls directions from dictionary based on the room shape */
}
