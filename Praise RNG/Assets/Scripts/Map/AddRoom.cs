using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates m_templates;

    public void AddRoomToCollection(RoomTemplates _rt)
    {
        m_templates = _rt;
        m_templates._rooms.Add(gameObject);

    }

}
