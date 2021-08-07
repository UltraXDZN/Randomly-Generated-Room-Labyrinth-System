using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.parent != null);
        //Debug.Log(this.transform.parent.name);
        //Debug.Log(other.transform.parent.name == this.transform.parent.name);
        //if (other.CompareTag("WallRoom"))
        //{
        //    Destroy(other.transform.parent.gameObject);
        //}
        //else
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log($"Player has entered {transform.parent.name} room");
        }
        else if (other.transform.parent != null)
        {
            if (other.transform.parent.tag.Contains("Room") && other.transform.GetComponent<RoomSettings>() == null && !other.transform.parent.name.Contains("Wall"))
            {
                Debug.Log(other.transform.parent);
                Debug.Log(this.transform.parent.name);
                Vector3 _pos_ = other.transform.parent.position;
                Quaternion _rot_ = other.transform.parent.rotation;
                Destroy(other.transform.parent.gameObject);
                //Instantiate(GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>()._closedRoom, _pos_, _rot_);
            }
        }
    }
}
