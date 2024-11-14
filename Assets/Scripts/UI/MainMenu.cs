using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField nameInput;

    [SerializeField]
    private Slider difficultySlider;

    [SerializeField]
    private GameSave gameSave;

    private void Start()
    {
        nameInput.text = gameSave.GetPlayerName();
        difficultySlider.value = gameSave.GetDifficultyLevel();
    }
}
