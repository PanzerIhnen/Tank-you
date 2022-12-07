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

    [Header("Enemies")]
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private Transform[] _enemySpawn;
    [SerializeField]
    private float _spawnTime;

    public EnemyControl SelectedEnemy { get; set; }
    public bool Paused { get; set; }

    private Ray _ray;
    private RaycastHit _rayHit;
    private bool _rightMouseClic;
    private int _currentPoints;

    private void Start()
    {
        UpdatePoints();
        InvokeRepeating("SpawnEnemy", 1, _spawnTime);
    }

    void Update()
    {
        PlayerInput();
        SelectEnemy();
    }

    private void SpawnEnemy()
    {
        if (!Paused)
        {
            int n = Random.Range(0, _enemySpawn.Length);
            Instantiate(_enemyPrefab, _enemySpawn[n].position, _enemySpawn[n].rotation);
        }
        else
        {
            CancelInvoke("SpawnEnemy");
        }
    }

    public void EnemyDestroyed()
    {
        _currentPoints++;
        UpdatePoints();

        if (_currentPoints >= _pointsToWin)
        {
            Paused = true;
            Invoke("Win", 1.5f);
        }
    }

    private void Win()
    {
        _canvasControl.ShowWin();
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
