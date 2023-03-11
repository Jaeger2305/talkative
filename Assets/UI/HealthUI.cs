using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] Image fill;
    [SerializeField] private Slider healthSlider;

    public void SetHealth(int health, int difference)
    {
        healthSlider.value = health;
    }
}