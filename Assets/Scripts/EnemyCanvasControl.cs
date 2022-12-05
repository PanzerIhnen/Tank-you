using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvasControl : MonoBehaviour
{
    [SerializeField]
    private Image _heathFill;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetHealthPercentage(float percentage)
    {
        _heathFill.fillAmount = percentage;
    }

}
