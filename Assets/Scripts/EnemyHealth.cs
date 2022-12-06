using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private EnemyCanvasControl _canvasControl;

    private bool _visibleInMainCanvas;

    public bool VisibleInMainCanvas
    {
        get { return _visibleInMainCanvas; }
        set 
        {
            if (value)
            {
                _mainCanvasControl.SetEnemyHealthPercentage(_currentHealth / _maxHealth);
            }
            _visibleInMainCanvas = value; 
        }
    }


    private float _currentHealth;
    private MainCanvasControl _mainCanvasControl;
    private GameManager _gameManager;

    private void Awake()
    {
        _mainCanvasControl = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<MainCanvasControl>();
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _canvasControl.SetHealthPercentage(1);
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        float healthPercentage = _currentHealth / _maxHealth;
        _canvasControl.SetHealthPercentage(healthPercentage);

        if (VisibleInMainCanvas)
        {
            _mainCanvasControl.SetEnemyHealthPercentage(healthPercentage);
        }

        if (_currentHealth == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        if (VisibleInMainCanvas)
        {
            _mainCanvasControl.HideEnemy();
        }

        _gameManager.EnemyDestroyed();

        Destroy(gameObject);
    }
}
