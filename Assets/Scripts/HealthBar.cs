using UnityEngine;
using UnityEngine.UI;

//Brackeys Health Bar tutorial
public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health; 
    }
}
