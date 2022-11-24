using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    [Header("Health Properties")]
    public int value;

    [Header("Display Properties")]
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
        ResetHealthBar();
    }

    public void ResetHealthBar()
    {
        healthBar.value = 100;
        value = (int)healthBar.value;
    }

    public void TakeDamage(int dmg)
    {
        healthBar.value -= dmg;
        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }

        value = (int)healthBar.value;
    }

    public void HealDamage(int dmg)
    {
        healthBar.value += dmg;
        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }

        value = (int)healthBar.value;
    }
}
