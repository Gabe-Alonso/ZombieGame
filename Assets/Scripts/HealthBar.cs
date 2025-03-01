using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    public TextMeshProUGUI _healthNums = null;



    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        if (_healthNums)
        {
            _healthNums.text = currentValue.ToString() + " / " + maxValue.ToString();
        }    
    } 


}
