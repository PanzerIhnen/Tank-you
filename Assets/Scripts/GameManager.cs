using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask _enemiesLayer;
    [SerializeField]
    private MainCanvasControl _canvasControl;
    [SerializeField]
    private int _pointsToWin;

    public EnemyControl SelectedEnemy { get; set; }
    public bool Paused { get; set; }

    private Ray _ray;
    private RaycastHit _rayHit;
    private bool _rightMouseClic;
    private int _currentPoints;

    private void Start()
    {
        UpdatePoints();
    }

    void Update()
    {
        PlayerInput();
        SelectEnemy();
    }

    public void EnemyDestroyed()
    {
        _currentPoints++;
        UpdatePoints();

        if (_currentPoints >= _pointsToWin)
        {
            Paused = true;
            _canvasControl.ShowWin();
        }
    }

    private void UpdatePoints()
    {
        _canvasControl.SetPoints(_currentPoints, _pointsToWin);
    }

    private void SelectEnemy()
    {
        if (_rightMouseClic)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * 500, Color.red);
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _rayHit, Mathf.Infinity, _enemiesLayer))
            {
                EnemyControl tempEnemy = _rayHit.collider.GetComponent<EnemyControl>();
                if (tempEnemy != SelectedEnemy && SelectedEnemy != null)
                {
                    SelectedEnemy.Unselect();
                }
                _canvasControl.ShowEnemy();
                SelectedEnemy = tempEnemy;
                SelectedEnemy.Select();
            }
            else if (SelectedEnemy != null)
            {
                _canvasControl.HideEnemy();
                SelectedEnemy.Unselect();
                SelectedEnemy = null;
            }
        }
    }

    private void PlayerInput()
    {
        _rightMouseClic = Input.GetMouseButtonDown(1);
    }
}
