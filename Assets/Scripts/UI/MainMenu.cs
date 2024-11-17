using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour, IScreen
{
    [SerializeField]
    private TMP_InputField nameInput;

    [SerializeField]
    private Slider difficultySlider;

    private void Start()
    {
        nameInput.text = GameSettings.GetPlayerName();
        difficultySlider.value = GameSettings.GetDifficultyLevel();
    }

    public void NameInputed(string name)
    {
        GameSettings.SetPlayerName(name);
    }

    public void DifficultyChanged(float difficultyLevel)
    {
        GameSettings.SetDifficultyLevel(difficultyLevel);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
