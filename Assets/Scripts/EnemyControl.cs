using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    Transform _towerBody;
    [SerializeField]
    private float _distanceToAttack;
    

    [Header("Shooting")]
    [SerializeField]
    private Transform _missilPosition;
    [SerializeField]
    private GameObject _missilPrefab;
    [SerializeField]
    private float _missilForce;
    [SerializeField]
    private float _attackCadence;

    [Header("General")]
    [SerializeField]
    private GameObject _selectedMark;

    private NavMeshAgent _agent;
    private Transform _targetPlayer;
    private float _attackTimer;
    private EnemyHealth _health;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        _health = GetComponent<EnemyHealth>();
    }

    public void Select()
    {
        _selectedMark.SetActive(true);
        _health.VisibleInMainCanvas = true;
    }

    public void Unselect()
    {
        _selectedMark.SetActive(false);
        _health.VisibleInMainCanvas = false;
    }

    private void Update()
    {
        Move();
        TurningTower();
    }

    private void Move()
    {
        if (_targetPlayer != null)
        {
            float distance = Vector3.Distance(transform.position, _targetPlayer.position);

            if (distance > _distanceToAttack)
            {
                _attackTimer = 0;
                _agent.SetDestination(_targetPlayer.position);
            }
            else
            {
                FireMissil();
            }
        }
    }

    private void FireMissil()
    {
        if (_attackTimer <= 0)
        {
            _attackTimer = _attackCadence;
            GameObject newMissil = Instantiate(_missilPrefab, _missilPosition.position, _missilPosition.rotation);
            newMissil.GetComponent<Rigidbody>().AddForce(newMissil.transform.forward * _missilForce, ForceMode.Impulse);
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }

    private void TurningTower()
    {
        if (_targetPlayer != null)
        {
            Vector3 towerToPlayer = _targetPlayer.position - transform.position;
            towerToPlayer.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(towerToPlayer);
            _towerBody.rotation = newRotation;
        }
    }
}
