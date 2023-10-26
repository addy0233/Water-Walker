using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Healthbar1 : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public bool textOn = false;
    public TextMeshProUGUI text;

    public PlayerHealth1 playerHealth;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        text.text = playerHealth.currentHealth.ToString();
    }

    public void SetHealth(float health)
    {
        slider.value = health;

        text.text = playerHealth.currentHealth.ToString();
    }
}
