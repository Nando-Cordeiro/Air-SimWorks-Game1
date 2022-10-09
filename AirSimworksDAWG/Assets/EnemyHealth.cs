using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    float health;

    [SerializeField] Slider slider;
    [SerializeField] GameObject healthBarUI;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void Damage(float damageAmmount)
    {
        health -= damageAmmount;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        slider.value = health / maxHealth;
    }
}
