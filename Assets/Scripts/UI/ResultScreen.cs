using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour, IScreen
{
    public event Action OnMainMenu;
    public event Action OnRestart;

    [SerializeField]
    private Image resultsOverlay;

    [SerializeField]
    private Sprite winOverlay;

    [SerializeField]
    private Sprite loseOverlay;

    [SerializeField]
    private TextMeshProUGUI totalMovesText;

    public void ShowWinResult(string message, int totalMoves)
    {
        resultsOverlay.sprite = winOverlay;
        totalMovesText.text = $"Total Moves: {totalMoves}";
        totalMovesText.gameObject.SetActive(true);
        Show();
    }

    public void ShowLoseResult(string message)
    {
        resultsOverlay.sprite = loseOverlay;
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
