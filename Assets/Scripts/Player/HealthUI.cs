using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    public TextMeshProUGUI HealthView;
    public PlayerHealth PlayerHealth;

    private void Start()
    {
        HealthView.text = PlayerHealth.Health.ToString();
    }
}
