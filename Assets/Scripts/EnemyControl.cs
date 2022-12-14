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

    [Header("Sound")]
    [SerializeField]
    private AudioSource _motor;
    [SerializeField]
    private AudioSource _fx;
    [SerializeField]
    private AudioClip _fireMissilSound;

    private NavMeshAgent _agent;
    private Transform _targetPlayer;
    private float _attackTimer;
    private EnemyHealth _health;
    private GameManager _gameManager;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
        PlaySound();
        TurningTower();
    }

    private void PlaySound()
    {
        if (_agent.velocity != Vector3.zero)
        {
            if (!_motor.isPlaying)
            {
                _motor.Play();
            }
        }
        else
        {
            if (_motor.isPlaying)
            {
                _motor.Stop();
            }
        }
    }

    private void Move()
    {
        if (!_gameManager.Paused && !_health.IsDead)
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
        else
        {
            _agent.isStopped = true;
        }
    }

    private void FireMissil()
    {
        if (_attackTimer <= 0)
        {
            _attackTimer = _attackCadence;
            GameObject newMissil = Instantiate(_missilPrefab, _missilPosition.position, _missilPosition.rotation);
            newMissil.GetComponent<Rigidbody>().AddForce(newMissil.transform.forward * _missilForce, ForceMode.Impulse);
            _fx.PlayOneShot(_fireMissilSound);
        }
        else
        {
            _attackTimer -= Time.deltaTime;
        }
    }

    private void TurningTower()
    {
        if (!_gameManager.Paused)
        {
            Vector3 towerToPlayer = _targetPlayer.position - transform.position;
            towerToPlayer.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(towerToPlayer);
            _towerBody.rotation = newRotation;
        }
    }
}
