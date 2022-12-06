using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTank;
    [SerializeField]
    private float _smooth;

    private Vector3 _offset;

    void Start()
    {
        _offset = _playerTank.position - transform.position;
    }

    void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_playerTank != null)
        {
            Vector3 targetPosition = _playerTank.position - _offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, _smooth * Time.deltaTime);
        }
    }
}
