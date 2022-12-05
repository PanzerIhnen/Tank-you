using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilControl : MonoBehaviour
{
    [SerializeField]
    private float _damage;
    void Start()
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().Damage(_damage);
            Destroy(gameObject);
        }
    }
}
