using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private TextMeshProUGUI _pointsText;
    [SerializeField]
    private GameObject _winPanel;


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

    public void SetPoints(int points, int targetPoints)
    {
        _pointsText.text = $"{points} / {targetPoints}";
    }

    public void ShowWin()
    {
        _winPanel.SetActive(true);
    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
