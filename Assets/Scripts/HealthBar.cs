using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;
    bool isdead = false;
    public SimulateDeath death;
    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthSlider.maxValue = 100;
        _healthSlider.value = 100;
    }
    private void Update()
    {
        if (isdead)
        {
            Target[] asteroids = FindObjectsOfType<Target>();
            for(int i = 0; i < asteroids.Length; i++)
            {
                Destroy(asteroids[i].gameObject);
            }
        }
    }
    public void Damage(float damage)
    {
        _healthSlider.value-=damage;
        if (_healthSlider.value <= 0)
        {
            death.Exit();
        }
        if (_healthSlider.value <= 0)
        {
            death.Exit();
            isdead = true;
        }
    }
}