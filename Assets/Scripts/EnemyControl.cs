using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _smoothAcceleration;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    Transform _towerBody;

    [Header("Shooting")]
    [SerializeField]
    private Transform _missilPosition;
    [SerializeField]
    private GameObject _missilPrefab;
    [SerializeField]
    private float _missilForce;

    //private Rigidbody _rb;
    //private float _vertical;
    //private float _horizontal;
    private bool _fireMissil;
    //private Vector3 _targetVelocity;
    //private Vector3 _dampVelocity;
    //private Vector3 _targetAngularVelocity;
    //private Vector3 _dampAngularVelocity;
    void Awake()
    {
        //_rb = GetComponent<Rigidbody>();
    }

    //void Update()
    //{
    //    PlayerInput();
    //    TargetVelocity();
    //    FireMissil();
    //}

    //private void FixedUpdate()
    //{
    //    Move();
    //    TurningTower();
    //}

    private void FireMissil()
    {
        if (_fireMissil)
        {
            GameObject newMissil = Instantiate(_missilPrefab, _missilPosition.position, _missilPosition.rotation);
            newMissil.GetComponent<Rigidbody>().AddForce(newMissil.transform.forward * _missilForce, ForceMode.Impulse);
        }
    }

    private void TurningTower()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
        {
            Vector3 towerToMouse = hit.point - transform.position;
            towerToMouse.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(towerToMouse);
            _towerBody.rotation = newRotation;
        }
    }
}
