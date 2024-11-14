using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySliderIndicator : MonoBehaviour
{
    private TextMeshProUGUI sliderIndicator;

    private void Awake()
    {
        sliderIndicator = GetComponent<TextMeshProUGUI>();
    }

    public void DifficultyValueChanged(float difficultyValue)
    {
        sliderIndicator.text = $"{difficultyValue}";
    }
}
