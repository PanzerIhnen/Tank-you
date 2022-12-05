using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private EnemyCanvasControl _canvasControl;

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
        Destroy(gameObject);
    }
}
