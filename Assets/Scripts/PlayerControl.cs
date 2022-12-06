using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
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
    [SerializeField]
    private float _attackCadence;
    [SerializeField]
    private Transform _bulletPosition;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private float _bulletCadence;

    [Header("General")]
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private MainCanvasControl _canvasControl;

    private Rigidbody _rb;
    private float _vertical;
    private float _horizontal;
    private bool _fireMissil;
    private bool _fireBullets;
    private Vector3 _targetVelocity;
    private Vector3 _dampVelocity;
    private Vector3 _targetAngularVelocity;
    private Vector3 _dampAngularVelocity;
    private float _attackTimer;
    private float _bulletTimer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _attackTimer = _attackCadence;
    }

    void Update()
    {
        PlayerInput();
        TargetVelocity();
        FireMissil();
        FireBullets();
    }

    private void FixedUpdate()
    {
        Move();
        TurningTower();
    }

    private void Move()
    {
        _rb.velocity = Vector3.SmoothDamp(_rb.velocity, _targetVelocity, ref _dampVelocity, _smoothAcceleration);
        _rb.angularVelocity = Vector3.SmoothDamp(_rb.angularVelocity, _targetAngularVelocity, ref _dampAngularVelocity, _smoothAcceleration);
    }

    private void PlayerInput()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
        _fireMissil = Input.GetKeyDown(KeyCode.Space);
        _fireBullets = Input.GetMouseButton(0);

    }

    private void TargetVelocity()
    {
        _targetVelocity = transform.forward * _vertical * _speed;
        _targetAngularVelocity = new Vector3(_rb.angularVelocity.x, _horizontal * _rotationSpeed);
    }

    private void FireBullets()
    {
        if (_bulletTimer <= 0)
        {
            if (_fireBullets)
            {
                _bulletTimer = _bulletCadence;
                GameObject newBullet = Instantiate(_bulletPrefab, _bulletPosition.position, _bulletPosition.rotation);
                newBullet.GetComponent<BulletControl>().Speed = _bulletSpeed;
            }
        }
        else
        {
            _bulletTimer -= Time.deltaTime;
        }
    }

    private void FireMissil()
    {
        if (_attackTimer >= _attackCadence)
        {
            if (_fireMissil)
            {
                _attackTimer = 0;
                GameObject newMissil = Instantiate(_missilPrefab, _missilPosition.position, _missilPosition.rotation);
                newMissil.GetComponent<Rigidbody>().AddForce(newMissil.transform.forward * _missilForce, ForceMode.Impulse);
            }
        }
        else
        {
            _attackTimer += Time.deltaTime;
            _attackTimer = Mathf.Clamp(_attackTimer, 0, _attackCadence);
        }

        _canvasControl.SetRechargePercentage(_attackTimer / _attackCadence);
    }

    private void TurningTower()
    {
        if (_gameManager.SelectedEnemy != null)
        {
            Vector3 towerToEnemy = _gameManager.SelectedEnemy.transform.position - transform.position;
            towerToEnemy.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(towerToEnemy);
            _towerBody.rotation = newRotation;
        }
        else
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
}
