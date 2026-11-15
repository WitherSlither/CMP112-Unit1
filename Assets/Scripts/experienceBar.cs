using UnityEngine;
using UnityEngine.UI;

public class experienceBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxExp(float XP)
    {
        slider.maxValue = XP;
    }

    public void setExp(float XP)
    {
        slider.value = XP;
    }
   
}
