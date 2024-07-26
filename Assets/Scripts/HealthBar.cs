using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;
    public SimulateDeath death;
    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthSlider.maxValue = 100;
        _healthSlider.value = 100;
    }
    public void Damage(float damage)
    {
        _healthSlider.value-=damage;
        if (_healthSlider.value <=0 )
        death.Exit();
    }
}