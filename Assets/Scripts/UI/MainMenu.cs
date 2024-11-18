using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour, IScreen
{
    [SerializeField]
    private TMP_InputField nameInput;

    [SerializeField]
    private TextMeshProUGUI nameWarning;

    [SerializeField]
    private Slider difficultySlider;

    public UnityEvent OnGameStart;

    private void Start()
    {
        nameInput.text = GameSettings.GetPlayerName();
        difficultySlider.value = GameSettings.GetDifficultyLevel();
    }

    public void NameInputed(string name)
    {
        if(!string.IsNullOrEmpty(name))
        {
            GameSettings.SetPlayerName(name);
            nameWarning.gameObject.SetActive(false);
        }
    }

    public void DifficultyChanged(float difficultyLevel)
    {
        GameSettings.SetDifficultyLevel(difficultyLevel);
    }

    public void TryGameStart()
    {
        if(!string.IsNullOrEmpty(nameInput.text))
        {
            OnGameStart?.Invoke();
        }
        else
        {
            nameWarning.gameObject.SetActive(true);
        }
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
