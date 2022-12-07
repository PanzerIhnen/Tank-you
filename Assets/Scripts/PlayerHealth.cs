using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private MainCanvasControl _canvasControl;
    [SerializeField]
    private ParticleSystem _explosionSystem;
    [SerializeField]
    private GameObject _shape;

    [Header("Sound")]
    [SerializeField]
    private AudioSource _fx;
    [SerializeField]
    private AudioClip _explosionSound;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _canvasControl.SetHealthPercentage(1);
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _canvasControl.SetHealthPercentage(_currentHealth / _maxHealth);

        if (_currentHealth == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        _shape.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        _explosionSystem.Play();
        _fx.PlayOneShot(_explosionSound);
        Destroy(gameObject, 1.5f);
    }
}
