using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilControl : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    [SerializeField]
    private string _targetTag;
    void Start()
    {
        Destroy(gameObject, 2);
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
