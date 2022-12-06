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


    public void SetHealthPercentage(float percentage)
    {
        _heathFill.fillAmount = percentage;
    }

    public void SetRechargePercentage(float percentage)
    {
        _rechargeFill.fillAmount = percentage;
    }

}
