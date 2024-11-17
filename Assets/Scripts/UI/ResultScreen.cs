using System;
using TMPro;
using UnityEngine;

public class ResultScreen : MonoBehaviour, IScreen
{
    public event Action OnMainMenu;
    public event Action OnRestart;

    [SerializeField]
    private TextMeshProUGUI resultText;

    [SerializeField]
    private TextMeshProUGUI totalMovesText;

    public void ShowWinResult(string message, int totalMoves)
    {
        resultText.text = message;
        totalMovesText.text = $"Total Moves: {totalMoves}";
        totalMovesText.gameObject.SetActive(true);
        Show();
    }

    public void ShowLoseResult(string message)
    {
        resultText.text = message;
        totalMovesText.gameObject.SetActive(false);
        Show();
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Hide();
    }

    public void Restart()
    {
        OnRestart?.Invoke();
        Hide();
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
