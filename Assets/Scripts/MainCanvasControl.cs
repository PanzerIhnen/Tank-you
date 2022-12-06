using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasControl : MonoBehaviour
{
    [SerializeField]
    private Image _heathFill;
    [SerializeField]
    private Image _rechargeFill;
    [SerializeField]
    private GameObject _enemyPanel;
    [SerializeField]
    private Image _enemyHealthFill;


    public void SetHealthPercentage(float percentage)
    {
        _heathFill.fillAmount = percentage;
    }

    public void SetRechargePercentage(float percentage)
    {
        _rechargeFill.fillAmount = percentage;
    }

    public void SetEnemyHealthPercentage(float percentage)
    {
        _enemyHealthFill.fillAmount = percentage;
    }

    public void ShowEnemy()
    {
        _enemyPanel.SetActive(true);
    }

    public void HideEnemy()
    {
        _enemyPanel.SetActive(false);
    }
}
