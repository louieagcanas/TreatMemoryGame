using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySliderIndicator : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private TextMeshProUGUI sliderIndicator;

    private void Awake()
    {
        sliderIndicator = GetComponent<TextMeshProUGUI>();
        slider.onValueChanged.AddListener(DifficultyValueChanged);
    }

    private void DifficultyValueChanged(float difficultyValue)
    {
        sliderIndicator.text = $"{difficultyValue}";
    }
}
