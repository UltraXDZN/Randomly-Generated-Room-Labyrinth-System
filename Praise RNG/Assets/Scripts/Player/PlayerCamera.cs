using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Main Settings")]
    public Transform _playerCharacter;
    public float _distanceFromPlayer;
    public float _playerHeight;
    public float _cameraHeight;
    public bool _inverted;

    [Header("Speed Values")]
    public float _moveSpeed;
    public float _rotSpeed;
    public float _scrollSpeed;

    private int m_invertedValue = 1;
    private Vector3 m_distance;
    private Quaternion m_offset;
    private float m_cameraRot;

    private void Start()
    {
        m_distance = transform.position - _playerCharacter.position;
        transform.position = transform.forward - m_distance + _playerCharacter.position - Vector3.up * -_distanceFromPlayer;
    }
    private void Update()
    {
        //transform.LookAt(_playerCharacter);
        //Vector3 _playerLocation_ = new Vector3(_playerCharacter.position.x - _distanceFromPlayer, _playerCharacter.position.y + _distanceFromPlayer, _playerCharacter.position.z - _distanceFromPlayer);
        //transform.position = Vector3.MoveTowards(transform.position, _playerLocation_, Time.deltaTime * _moveSpeed);

        //Inversion
        m_invertedValue = _inverted ? -1 : 1;

        //Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            m_cameraRot -= _rotSpeed * Time.deltaTime;
            //transform.RotateAround(_playerCharacter.position, Vector3.up, _rotSpeed * Time.deltaTime);
            //transform.position = transform.forward - m_distance + _playerCharacter.position - Vector3.up * -_distanceFromPlayer;

        }
        else if (Input.GetKey(KeyCode.E))
        {
            m_cameraRot += _rotSpeed * Time.deltaTime;
            //transform.RotateAround(_playerCharacter.position, Vector3.up, -_rotSpeed * Time.deltaTime);
            //transform.position = transform.forward - m_distance + _playerCharacter.position - Vector3.up * -_distanceFromPlayer;
        }
        m_offset = Quaternion.AngleAxis(m_cameraRot, Vector3.up);
        //transform.position = transform.forward - m_distance + _playerCharacter.position - m_offset + Vector3.up * -_distanceFromPlayer;
        transform.position = _playerCharacter.position + _playerCharacter.up * _cameraHeight - m_offset * Vector3.forward * _distanceFromPlayer;
        transform.LookAt(_playerCharacter.position + Vector3.up * _playerHeight);


        //Zooming
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += m_invertedValue * Time.deltaTime * _scrollSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= m_invertedValue * Time.deltaTime * _scrollSpeed;
        }
    }
}
