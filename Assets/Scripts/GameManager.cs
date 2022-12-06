using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask _enemiesLayer;

    public EnemyControl SelectedEnemy { get; set; }

    private Ray _ray;
    private RaycastHit _rayHit;
    private bool _rightMouseClic;

    void Update()
    {
        PlayerInput();
        SelectEnemy();
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

                SelectedEnemy = tempEnemy;
                SelectedEnemy.Select();
            }
            else if (SelectedEnemy != null)
            {
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
