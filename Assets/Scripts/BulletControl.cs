using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _targetTag;
    [SerializeField]
    private float _lifeTime;

    public float Speed { get; set; }

    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * Speed;
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            other.GetComponent<IHealth>().Damage(_damage);
            Destroy(gameObject);
        }
    }
}
